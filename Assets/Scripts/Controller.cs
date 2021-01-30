using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public GameObject targetView;
    public BottomDetector detector;
    public Animator animator;

    [Header("Controls")]
    public float speedMove;
    public float speedJump;
    public float speedRotate;
    public float rotateSensibility;

    [Header("Camera")]
    public float cameraPower;

    private bool fakeDeath;
    private bool canJump;
    private float timedJump;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        fakeDeath = true;
        animator.SetBool("FakeDeath", fakeDeath);
        timedJump = 0f;

        detector.OnTouchedGround += ResetJump;
    }

    // Update is called once per frame
    void Update()
    {
        // Target stay focus on player pos
        Vector3 pos = player.transform.position;
        pos.y -= 0.3f;
        targetView.transform.position = pos;

        Rotate();
        FakeDead();

        if (!fakeDeath)
        {
            Move();
            if(timedJump > 0.5f) Jump();
            else timedJump += Time.deltaTime;
        }


    }

    public void SetPosition(Transform pos)
    {
        player.transform.position = pos.position;
    }


    private void FakeDead()
    {
        if (Input.GetButtonDown("Y"))
        {
            fakeDeath = !fakeDeath;
            animator.SetBool("FakeDeath", fakeDeath);
        }
    }

    private void ResetJump()
    {
        if (canJump) return;

        animator.SetBool("Jumping", false);
        timedJump = 0f;
        canJump = true;
    }


    private void Jump()
    {
        if (Input.GetButtonDown("A") && canJump)
        {
            canJump = false;
            animator.SetBool("Jumping", true);
            StartCoroutine(JumpAction());
        }
    }

    IEnumerator JumpAction()
    {
        yield return new WaitForSeconds(.2f);
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, speedJump, 0), ForceMode.Impulse);
    }

    private void Rotate()
    {

        if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
        {
            targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * (cameraPower * 2), Vector3.up);
            targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * (cameraPower * 2), Vector3.right);
        } else
        {
            targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Joystick_RH") * cameraPower, Vector3.up);
            targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Joystick_RV") * cameraPower, Vector3.right);
        }

        Vector3 angles = targetView.transform.localEulerAngles;
        angles.z = 0;

        if (angles.x > 180 && angles.x < 360)
        {
            angles.x = 360;
        }
        else if (angles.x < 180 && angles.x > 80)
        {
            angles.x = 80;
        }

        // Target rotation
        targetView.transform.localEulerAngles = angles;
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * speedRotate;

        float verticallMove = -Input.GetAxis("Vertical") * Time.deltaTime * speedMove;

        if (horizontalMove != 0f || verticallMove != 0f)
        {
            animator.SetBool("Walking", true);
            // Player rotation with jostick
            if (Mathf.Abs(horizontalMove) >= rotateSensibility)
            {
                Quaternion rotate = Quaternion.identity;
                rotate.eulerAngles = new Vector3(0, player.transform.rotation.eulerAngles.y + horizontalMove, 0);
                player.transform.rotation = rotate;
            }


            // Player rotation with view
            //player.transform.rotation = Quaternion.Euler(270, targetView.transform.rotation.eulerAngles.y + 90, 0);

            player.transform.position += (player.transform.right * verticallMove);
        } else
        {
            animator.SetBool("Walking", false);
        }



        

    }
}

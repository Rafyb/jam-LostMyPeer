using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public GameObject targetView;
    public BottomDetector detector;

    [Header("Controls")]
    public float speedMove;
    public float speedJump;
    public float speedRotate;
    public float rotateSensibility;

    [Header("Camera")]
    public float cameraPower;
    

    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;

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

        Move();

        Jump();

        FakeDead();
    }

    private void ResetJump()
    {
        canJump = true;
    }


    private void FakeDead()
    {
        if (Input.GetButtonDown("Y")) Debug.Log("press");
    }

    private void Jump()
    {
        if (Input.GetButtonDown("A") && canJump)
        {
            Debug.Log("Jump");
            canJump = false;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, speedJump, 0),ForceMode.Impulse);
        }
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
            // Player rotation with jostick
            if(Mathf.Abs(horizontalMove) >= rotateSensibility)
            {
                Quaternion rotate = Quaternion.identity;
                rotate.eulerAngles = new Vector3(270, player.transform.rotation.eulerAngles.y + horizontalMove, 0);
                player.transform.rotation = rotate;
            }


            // Player rotation with view
            //player.transform.rotation = Quaternion.Euler(270, targetView.transform.rotation.eulerAngles.y + 90, 0);

            player.transform.position += (player.transform.right * verticallMove);
        }



        

    }
}

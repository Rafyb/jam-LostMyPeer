using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public GameObject player;
    public Transform playerPos;
    public GameObject targetView;

    public float speed;
    public float rotationPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si le cube tombe
        targetView.transform.position = playerPos.transform.position;

        Rotate();

        Move();
    }

    private void Rotate()
    {
        targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X")*rotationPower,Vector3.up);
        targetView.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationPower, Vector3.right);

        Vector3 angles = targetView.transform.localEulerAngles;
        angles.z = 0;
        if (angles.x > 180 && angles.x < 340)
        {
            angles.x = 340;
        }
        else if (angles.x < 180 && angles.x > 40)
        {
            angles.x = 40;
        }

        targetView.transform.localEulerAngles = angles;

    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float verticallMove = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        player.transform.position = new Vector3(player.transform.position.x + horizontalMove, player.transform.position.y, player.transform.position.z + verticallMove);

        Vector3 rotate = player.transform.localEulerAngles;
        rotate.x = targetView.transform.localEulerAngles.x;
    }
}

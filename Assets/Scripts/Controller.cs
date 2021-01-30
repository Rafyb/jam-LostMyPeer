using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public GameObject player;
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
        targetView.transform.position = player.transform.position;

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

        // Target rotation
        targetView.transform.localEulerAngles = angles;
       


    }

    private void Move()
    {

        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float verticallMove = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (horizontalMove != 0f || verticallMove != 0f)
        {
            // Player rotation
            player.transform.rotation = Quaternion.Euler(0, targetView.transform.rotation.eulerAngles.y, 0);
            player.transform.position += (player.transform.forward * verticallMove) + (player.transform.right * horizontalMove);
        }



        

    }
}

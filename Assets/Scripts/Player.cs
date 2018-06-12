using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed;
    public float turnSpeed;
    public Rigidbody2D playerBody;
    public float axisY;
    private float axisX;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;



    // Use this for initialization
    void Start ()
    {
        GameManager.instance.player = this;
	}

    //Update is called once per frame
    void Update()
    {
        //Check for input from keyboard on the verticle and horizonal axis
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        //Screen Wrapping

        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
            transform.position = newPos;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
            transform.position = newPos;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
            transform.position = newPos;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
            transform.position = newPos;
        }

    }

    void FixedUpdate()
    {
        //if an input key for the vertical axis is pressed add force to make the ship move in the direction its facing
        playerBody.AddRelativeForce(Vector2.up * axisY * moveSpeed);
        //if an input key for the horizontal axis is pressed add torque to the make it turn in the right direction
        playerBody.AddTorque(-turnSpeed * axisX);
    }
}
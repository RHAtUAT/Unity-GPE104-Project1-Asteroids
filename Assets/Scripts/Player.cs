using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Rigidbody2D playerBody;
    Vector3 bulletDir;
    Vector2 newPos;

    private float axisY;
    private float axisX;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;


    // Use this for initialization
    void Start ()
    {
        GameManager.instance.player = this.gameObject;
        bulletDir = GameManager.instance.player.transform.right;
        GameManager.instance.playerLifeDisplay.gameObject.GetComponent<Text>().text = ("Lives: " + GameManager.instance.playerLives.ToString());

    }

    //Update is called once per frame
    void Update()
    {
        //Check for input from keyboard on the verticle and horizonal axis
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        //If player goes off screen make then reappear from the other side of the sreen
        //Screen Wrapping
        newPos = transform.position;
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
        //Controls

        //if an input key for the vertical axis is pressed add force to make the ship move in the direction its facing
        //playerBody.AddRelativeForce(Vector2.up * axisY * GameManager.instance.playerSpeed);
        transform.Translate(0, Time.deltaTime * axisY * GameManager.instance.playerSpeed, 0);

        //if an input key for the horizontal axis is pressed add torque to make that player(ship) turn in the right direction
        //playerBody.AddTorque(-turnSpeed * axisX);
        transform.Rotate(Vector3.forward * axisX * -GameManager.instance.playerRotationSpeed * Time.deltaTime);
 

        //If player presses space, fire a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //When the bullet is spawned, give it the positon and rotation of the player
            GameObject bullet = Instantiate(GameManager.instance.bullet, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * GameManager.instance.bulletSpeed);
            
            //Destroys the bullet after bulletTime seconds
            Destroy(bullet, GameManager.instance.bulletTime);

        }

    }

    //If the player collides with an asteroid
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "EnemyShip")
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        GameManager.instance.playerLives--;
        if(GameManager.instance.playerLives == 0)
        {
            //Player is dead so game should end
            Application.Quit();
        }
        else
        {
            GameManager.instance.DestroyActiveEnemies();
            newPos.x = 0;
            newPos.y = 0;
            playerBody.velocity = newPos;
            transform.SetPositionAndRotation(newPos, Quaternion.Euler(0,0,0) );
            //Display lives left
            GameManager.instance.playerLifeDisplay.gameObject.GetComponent<Text>().text = ("Lives: " + GameManager.instance.playerLives.ToString());
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public int asteroidHealth;
    public Rigidbody2D asteroidBody;
    public Vector2 direction;
    public float speed;
    //Vector2 newPos;

    // Use this for initialization
    void Start ()
    {
        

        //Asteroid targets player
        direction = GameManager.instance.player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //Add a random amount of speed (Did this, didn't like how some asteroids were super slow)
        speed = Random.Range(-GameManager.instance.asteroidSpeed, GameManager.instance.asteroidSpeed);

        //Made the asteroid spin as it moves towards you
        //float rotation = Random.Range(-GameManager.instance.asteroidRotation, GameManager.instance.asteroidRotation);
        //asteroidBody.AddTorque(rotation);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);


        /*
        newPos = transform.position;
        if (transform.position.y > GameManager.instance.screenTop)
        {
            newPos.y = GameManager.instance.screenBottom;
            transform.position = newPos;
        }
        if (transform.position.y < GameManager.instance.screenBottom)
        {
            newPos.y = GameManager.instance.screenTop;
            transform.position = newPos;
        }
        if (transform.position.x < GameManager.instance.screenLeft)
        {
            newPos.x = GameManager.instance.screenRight;
            transform.position = newPos;
        }
        if (transform.position.x > GameManager.instance.screenRight)
        {
            newPos.x = GameManager.instance.screenLeft;
            transform.position = newPos;
        }
        */
    }

    //Destroy asteroid and bullets if they collide
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if(asteroidHealth > 0)
            {
                asteroidHealth -= GameManager.instance.bulletDamage;
            }
            else
            {
                GameManager.instance.activeEnemies.Remove(gameObject);
                Destroy(this.gameObject);
            }
            //Destroy bullet
            Destroy(collision.gameObject);
        }

        else if (GameManager.instance.removingEnemies == false)
        {
            if (collision.gameObject.tag == "DespawnPoint")
            {
                //Remove enemy from activeEnemies list
                GameManager.instance.activeEnemies.Remove(this.gameObject);

                //Destroy the gameObject with this script attached
                Destroy(this.gameObject);
            }
        }
    }
}

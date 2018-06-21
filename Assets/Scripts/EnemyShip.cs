using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public Vector3 direction;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        direction = GameManager.instance.player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Gradual targeting
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, GameManager.instance.enemyShipRotationSpeed * Time.deltaTime);

        transform.Translate(Time.deltaTime * GameManager.instance.enemyShipSpeed, 0, 0);

    }


    //Destroy asteroid and bullets if they collide
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameManager.instance.enemyShipHealth > 0)
            {
                GameManager.instance.enemyShipHealth -= GameManager.instance.bulletDamage;
            }
            else
            {
                GameManager.instance.activeEnemies.Remove(gameObject);
                Destroy(this.gameObject);
            }
            //Destroy bullet
            Destroy(collision.gameObject);
        }
    }
}
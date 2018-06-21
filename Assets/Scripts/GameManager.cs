using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject playerLifeDisplay;

    public GameObject player;
    public float playerSpeed;
    public float playerRotationSpeed;
    public int playerLives;


    // Astroid SpawnPoints
    public GameObject[] asteroidSpawnPoints;

    // Astroid prefab
    public float asteroidSpeed;
    //public float asteroidRotation;

    //public float screenTop;
    //public float screenBottom;
    //public float screenLeft;
    //public float screenRight;

    // List of Astroids
    public List<GameObject> asteroids;

    // Bullet prefab
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletTime;
    public int bulletDamage;

    // Enemy ship
    public float enemyShipSpeed;
    public float enemyShipRotationSpeed;
    public int enemyShipHealth;

    // List of enemies
    public List<GameObject> activeEnemies;
    public bool removingEnemies;
    public int maximumActiveEnemies;



    // Use this for initialization
    void Awake () {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

            if (activeEnemies.Count < maximumActiveEnemies)
            {
                // Determine spawn point
                int id = Random.Range(0, asteroidSpawnPoints.Length);
                GameObject point = asteroidSpawnPoints[id];

                // Determine which asteroid to spawn
                GameObject asteroid = asteroids[Random.Range(0, asteroids.Count)];

                // Instantiate an asteroid
                GameObject asteroidInstance = Instantiate(asteroid, point.transform.position, Quaternion.identity);
                if( gameObject.tag == "Asteroid")
                {
                    Vector2 directionVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    directionVector.Normalize();
                    asteroidInstance.GetComponent<Asteroid>().direction = directionVector;
                }

                // Add to enemies list
                activeEnemies.Add(asteroidInstance);
            }
    }


    public void DestroyActiveEnemies()
    {
        removingEnemies = true;
        //Remove all enemies
        activeEnemies.ForEach(Destroy);
        activeEnemies.Clear();
        removingEnemies = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.enemies.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        GameManager.instance.enemies.Remove(this);
    }
}

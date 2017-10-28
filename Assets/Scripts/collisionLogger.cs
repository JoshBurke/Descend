﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionLogger : MonoBehaviour {

    public BoxCollider2D box;
    public bool enabled = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (enabled)
        {
            Debug.Log("Collided with " + coll.gameObject.ToString());
        }
    }
}

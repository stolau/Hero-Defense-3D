﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {
	
	private float speedProjectile = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(Vector3.forward * Time.deltaTime * speedProjectile);
		//Testing projectile movement
		
	}
}

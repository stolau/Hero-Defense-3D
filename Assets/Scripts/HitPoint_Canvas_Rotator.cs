using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint_Canvas_Rotator : MonoBehaviour {
	
	[SerializeField]
	private GameObject HPbar;

	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.rotation = Quaternion.Euler(60f, 0, 0);
	}
}

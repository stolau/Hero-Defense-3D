using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCD_Handler : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//gameObject.GetComponent<Animator>().speed = 3f;
		//gameObject.Animator["CD"].speed = 3f;
		gameObject.GetComponent<Animator>().SetFloat("CD_Speed", 6f);
	}
	
	// Update is called once per frame
}

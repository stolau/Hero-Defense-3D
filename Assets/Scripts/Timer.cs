using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


	void Update ()
	{
		// This is only used to make rotation of the clock object.
		transform.Rotate(0f, 0f, Time.deltaTime * -5f);
	
	}
}

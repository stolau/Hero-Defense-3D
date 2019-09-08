using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dot_Tick_Timer : MonoBehaviour {
	
private int indexDot;
private float dot_Duration;
[SerializeField]
private GameObject dotManager;



	// Use this for initialization
public void dot_Tick_Time(int indexDot, float dot_Duration)
{
	StartCoroutine(dot_Fade(indexDot, dot_Duration));
}
private IEnumerator dot_Fade(int indexDot, float dot_Duration)
{
	yield return new WaitForSeconds(dot_Duration);
	// In future add floating timer
	dotManager.transform.GetComponent<dotManager>().deActivateOnExpire(indexDot);
}
private IEnumerator dot_Timer(float dot_Duration)
{
	// in future calculates fillings of dot bar
	yield return new WaitForSeconds(2f);
}

}
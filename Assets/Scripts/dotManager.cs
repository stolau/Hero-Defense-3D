using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dotManager : MonoBehaviour {

// 5 separate hit effect plays
[SerializeField]
private Image HitEffect_IMG;
private bool hitEffect_On = false;
private float dot_Duration;

[SerializeField]
private List <GameObject> HitEffects = new List<GameObject> ();
[SerializeField]
private List <GameObject> HitEffects_Image = new List<GameObject> ();
[SerializeField]
private List<bool> index = new List<bool>();


// Do not touchs
private int i;


public void activateFirstInLine(float dot_Duration)
{
	// Dots dots = waves[rounds];
	// Activates first Hit Effect box that is open and checks it as being on use
	for(int i = 0; i < index.Count; i++)
	{
		
		if(!index[i])
		{
			index[i] = true;
			HitEffects[i].SetActive(!HitEffects[i].activeInHierarchy);
			HitEffects_Image[i].transform.GetComponent<dot_Tick_Timer>().dot_Tick_Time(i, dot_Duration);
			i = 10;
			// Calls dot_Tick_timer
		}
	}
		
}
public void deActivateOnExpire(int return_Index)
{
	index[return_Index] = false;
	HitEffects[return_Index].SetActive(!HitEffects[return_Index].activeInHierarchy);
	// Deactivates Hit effect box and checks it back to be used again
}






}

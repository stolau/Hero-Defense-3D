using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dots
{
	[SerializeField]
	private GameObject dotObject;
	[SerializeField]
	private Image dotImage;
	public bool dot_Index;
	[SerializeField]
	private int coolDown_Dot;
}
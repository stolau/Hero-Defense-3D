using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject spellManager;
	[SerializeField]
	private GameObject selectedSpells_Show;
	
	private int i, d, z = -1;
	
	public void newGameBtn(string newGameLevel)
	{
		// Condition checks that all spells are selected
		i = spellManager.GetComponent<SpellManager>().getSlot1();
		Debug.Log(i);
		if(i != -1)
		{
			d = spellManager.GetComponent<SpellManager>().getSlot2();
			Debug.Log(d);
			if(d != -1)
			{
				z = spellManager.GetComponent<SpellManager>().getSlot3();
				Debug.Log(d);
				if(z != -1)
				{
					Debug.Log("What the hell!");
					SceneManager.LoadScene(newGameLevel);
				}
				else
				{
					// Actviates Select all the spells
					selectedSpells_Show.SetActive(!selectedSpells_Show.activeInHierarchy);
					StartCoroutine(notEnoughSpells());
			
				}
			}
			else
			{
				// Actviates Select all the spells
				selectedSpells_Show.SetActive(!selectedSpells_Show.activeInHierarchy);
				StartCoroutine(notEnoughSpells());
			
			}
		}
		else
		{
			// Actviates Select all the spells
			selectedSpells_Show.SetActive(!selectedSpells_Show.activeInHierarchy);
			StartCoroutine(notEnoughSpells());
			
		}
		// Condition to check if there is 3 spells
		// SceneManager.LoadScene(newGameLevel);
	}
	public void customizeButton()
	{
		
	}
	public void exitGameBtn()
	{
		Application.Quit();
	}
	IEnumerator notEnoughSpells()
	{
		yield return new WaitForSeconds(3f);
		selectedSpells_Show.SetActive(!selectedSpells_Show.activeInHierarchy);
	}
	
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
	[SerializeField]
	private GameObject spell_1;
	[SerializeField]
	private GameObject spell_2;
	[SerializeField]
	private GameObject spell_3;	
	
	private GameObject button_1;
	private GameObject button_2;
	private GameObject button_3;
	
	private GameObject spellManager;

	[SerializeField]
	private List<GameObject> spellButtons = new List<GameObject>();
	
	void Start()
	{
		// Momentary random system until, way to selected Specs
		spellManager = GameObject.FindWithTag("SpellManager");
		/*if(spellManager == null)
		// This if statement only made for testing purposes.	
		{
			button_1 = spellButtons[0];
			button_2 = spellButtons[1];
			button_3 = spellButtons[2];
		}
		else
		{ */
		GameObject button_1 = spellButtons[
		spellManager.transform.GetComponent<SpellManager>().getSlot1()];
		
		GameObject button_2 = spellButtons[
		spellManager.transform.GetComponent<SpellManager>().getSlot2()];
		
		GameObject button_3 = spellButtons[
		spellManager.transform.GetComponent<SpellManager>().getSlot3()];
		// }
		
		button_1.SetActive(!button_1.activeInHierarchy);
		button_1.transform.parent = spell_1.transform;
		button_1.transform.localPosition = new Vector3(0f, 0f, 0f);
		button_2.SetActive(!button_2.activeInHierarchy);
		button_2.transform.parent = spell_2.transform;
		button_2.transform.localPosition = new Vector3(0f, 0f, 0f);
		button_3.SetActive(!button_3.activeInHierarchy);
		button_3.transform.parent = spell_3.transform;
		button_3.transform.localPosition = new Vector3(0f, 0f, 0f);
	}
	
}

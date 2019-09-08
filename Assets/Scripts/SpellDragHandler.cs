using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {


	[SerializeField]
	private Image selectedSpell_1;
	[SerializeField]
	private Image selectedSpell_2;
	[SerializeField]
	private Image selectedSpell_3;
	[SerializeField]
	private int spellID;
	[SerializeField]
	private GameObject spellManager;
	private Vector3 initPosition;

	
	void Start()
	{
		spellManager = GameObject.FindWithTag("SpellManager");
		initPosition = transform.position;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		// Here we add condition if image is tracked to selected position
		// Will remove old selection and place is to normal position
		// Add new to the position
		if(Vector3.Distance(Input.mousePosition, GameObject.FindWithTag("CSpell_Slot1").transform.position) < 45){
			// Adds the selected spell to current spells
			spellManager.transform.GetComponent<SpellManager>().editSpells(spellID, 1, gameObject);
			transform.position = GameObject.FindWithTag("CSpell_Slot1").transform.position;
		}
		else if(Vector3.Distance(Input.mousePosition, GameObject.FindWithTag("CSpell_Slot2").transform.position) < 45){
			// Adds the selected spell to current spells
			spellManager.transform.GetComponent<SpellManager>().editSpells(spellID, 2, gameObject);
			transform.position = GameObject.FindWithTag("CSpell_Slot2").transform.position;
		}
		else if(Vector3.Distance(Input.mousePosition, GameObject.FindWithTag("CSpell_Slot3").transform.position) < 45){
			// Adds the selected spell to current spells
			spellManager.transform.GetComponent<SpellManager>().editSpells(spellID, 3, gameObject);
			transform.position = GameObject.FindWithTag("CSpell_Slot3").transform.position;
		}
		// Returns the dragged icon to original position
		else {
			transform.localPosition = Vector3.zero;
		}
	}
	public void backToInitPos()
	{
		transform.position = initPosition;
	}

}
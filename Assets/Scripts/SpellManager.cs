using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellManager : MonoBehaviour{


	[SerializeField]
	private int slot1_public = -1;
	[SerializeField]
	private int slot2_public = -1;
	[SerializeField]
	private int slot3_public = -1;
	private GameObject spellObject_1;
	private GameObject spellObject_2;
	private GameObject spellObject_3;
  
	private int spellID;
	private int slotID;
  
	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public int getSlot1(){
		return(slot1_public);
	}
	public int getSlot2(){
		return(slot2_public);
	}
	public int getSlot3(){
		return(slot3_public);
	}
	public void editSpells(int spellID, int slotID, GameObject spellObject){
    if (slotID == 1){
		
		if(spellID != slot1_public)
		{	
			if(slot1_public != -1)
			{
				spellObject_1.transform.GetComponent<SpellDragHandler>().backToInitPos();
			}
			spellObject_1 = spellObject;
			slot1_public = spellID;
		}
    }
    if (slotID == 2){
		
		if (spellID != slot2_public)
		{
			if(slot2_public != -1)
			{
				spellObject_2.transform.GetComponent<SpellDragHandler>().backToInitPos();
			}
			spellObject_2 = spellObject;
			slot2_public = spellID;
		}
    }
    if (slotID == 3){
		if(spellID != slot3_public)
		{
			if(slot3_public != -1)
			{
				spellObject_3.transform.GetComponent<SpellDragHandler>().backToInitPos();
			}
			slot3_public = spellID;
			spellObject_3 = spellObject;
		}
    }
  }
}

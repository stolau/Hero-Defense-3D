﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCheck : MonoBehaviour {
	
	[SerializeField]
	private GameObject obj_Collider;
	[SerializeField]
	private GameObject sub_Collider;
	
	[SerializeField]
	private Transform playerer;
	[SerializeField]
	private GameObject levelManager;
	
	
	[SerializeField]
	private bool damageAbility;
	[SerializeField]
	private bool movementAbility;
	[SerializeField]
	private int damagePlayer = 20;
	[SerializeField]
	private float slowPlayer = 1f;
	[SerializeField]
	private float slowDuration = 2f;
	[SerializeField]
	private int lifesteal = 1;
	[SerializeField]
	private float duration = 0.1f;
	[SerializeField]
	private bool ticks;
	[SerializeField]
	private int ticksAmount;
	[SerializeField]
	private IEnumerator waitTime;
	[SerializeField]
	private bool explode_OnTrigger;
	[SerializeField]
	private ParticleSystem explode_PS;
	[SerializeField]
	private bool second_Cast;
	[SerializeField]
	private GameObject secondCast_Collider;
	[SerializeField]
	private bool damage_On_Hit;
	[SerializeField]
	private float damage_Every_Second;
	[SerializeField]
	private bool muteAbilities;
	
	// public Transform exit;
	// IENumerators
	private IEnumerator refresh;
	
	// private GameObject[] enemiesHit;
	 [SerializeField]
	private List<GameObject> enemiesHit = new List<GameObject>();
	
	

	void OnTriggerEnter( Collider other )
	{
	if(movementAbility)
	{
		if((other.gameObject == playerer.gameObject) || other.gameObject.tag == "Wall")
		{
			playerer.GetComponent<HeroMovement>().heroMovementOnOff();
			if(explode_OnTrigger)
			{
				explode_PS.Play();
				playerer.gameObject.GetComponent<Animator>().Play("SpecialAbility_Finish");
				activateSubCollider();
			}
			obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
			return;
		}
	}
	if(damageAbility)
	{
		if(other.tag == "opponent")
		{
			if(enemiesHit.IndexOf(other.gameObject) < 0)
			{
				// For Explosive Effects
				if(explode_OnTrigger)
				{
					playerer.GetComponent<Player>().AttackPlayer(damagePlayer, slowPlayer, lifesteal, other.gameObject, ticks, ticksAmount);
					explode_PS.Play();
					activateSubCollider();
					obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
					
					if(slowPlayer != 1f)
					{
						other.gameObject.GetComponent<EnemyMovement>().slowEnemy(slowPlayer, slowDuration);
					}
				}
				else
				{
					enemiesHit.Add(other.gameObject);
					playerer.GetComponent<Player>().AttackPlayer(damagePlayer, slowPlayer, lifesteal, other.gameObject, ticks, ticksAmount);
					if(slowPlayer != 1f)
					{
						other.gameObject.GetComponent<EnemyMovement>().slowEnemy(slowPlayer, slowDuration);
					}
				}
			}
		
		}
	}
		return;
	}
	// Activates second collider, usually called on explosions
	private void activateSubCollider()
	{
		sub_Collider.transform.parent = obj_Collider.transform;
		explode_PS.transform.parent = sub_Collider.transform;
		explode_PS.transform.localPosition = new Vector3(0f, 0.3f, 0f);
		sub_Collider.transform.localPosition = new Vector3(0f, 0f, 0f);
		sub_Collider.SetActive(!sub_Collider.activeInHierarchy);
		sub_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
		sub_Collider.transform.parent = null;
		explode_PS.transform.parent = null;
		explode_PS.Play();
	}
	
	public void enemiesHitList()
	{
		enemiesHit.Clear();
		waitTime = fadeTime(duration);
		refresh = refreshEnemyList();
		StartCoroutine(waitTime);
		if(damage_On_Hit)
		{
			StartCoroutine(refresh);
		}
		// enemiesHit.Clear();
		// Slash_Collider.SetActive(!Slash_Collider.activeInHierarchy);
	}
	private IEnumerator fadeTime(float fader)
	{
		yield return new WaitForSeconds(fader);
		if(second_Cast)
		{
			secondCast_Collider.SetActive(!secondCast_Collider.activeInHierarchy);
			secondCast_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
		}
		// Deactivates mute before expiring
		if(muteAbilities)
		{
			levelManager.GetComponent<Spellbook>().deActivateMute();
		}
		obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
	}
	private IEnumerator refreshEnemyList()
	{
		yield return new WaitForSeconds(damage_Every_Second);
		
		enemiesHit.Clear();
		//obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
		//obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
	}
	
}

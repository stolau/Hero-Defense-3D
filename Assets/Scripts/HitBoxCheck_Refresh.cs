using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCheck_Refresh : MonoBehaviour {
	
	[SerializeField]
	private GameObject obj_Collider;
	[SerializeField]
	private GameObject sub_Collider;
	
	[SerializeField]
	private Transform playerer;
	[SerializeField]
	private GameObject levelManager;

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
	
	private float refresh_Times;
	
	// public Transform exit;
	// IENumerators
	private IEnumerator refresh;
	
	// private GameObject[] enemiesHit;
	 [SerializeField]
	private List<GameObject> enemiesHit = new List<GameObject>();
	
	

	void OnTriggerStay( Collider other )
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
		return;
	}
	// Activates second collider, usually called on explosions
	private void activateSubCollider()
	{
		sub_Collider.transform.parent = obj_Collider.transform;
		explode_PS.transform.parent = sub_Collider.transform;
		sub_Collider.transform.localPosition = new Vector3(0f, 0f, 0f);
		sub_Collider.SetActive(!sub_Collider.activeInHierarchy);
		sub_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
		sub_Collider.transform.parent = null;
		explode_PS.transform.parent = null;
		explode_PS.Play();
	}
	
	public void enemiesHitList()
	{
		refresh_Times = duration / damage_Every_Second;
		enemiesHit.Clear();
		waitTime = fadeTime(duration);
		refresh = refreshEnemyList(refresh_Times);
		StartCoroutine(waitTime);
		if(damage_On_Hit)
		{
			// refresh_Times = duration / damage_Every_Second;
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
		// Deactivates mute before ability expires
		if(muteAbilities)
		{
			levelManager.GetComponent<Spellbook>().deActivateMute();
		}
		obj_Collider.SetActive(!obj_Collider.activeInHierarchy);
	}
	private IEnumerator refreshEnemyList(float refresh_Times)
	{
		for(int i = 0; i <= refresh_Times; i++)
		{
			yield return new WaitForSeconds(damage_Every_Second);
		
			enemiesHit.Clear();
		}

	}
	
}

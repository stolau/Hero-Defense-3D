using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
	
	
	

		private float cdPlayer; // Spell cooldown
		private int recover; // HP player recovers without hitting anybody

		
		// Do not touch 
		private float tsFrostNova = 0f; //Every spell got its basic TimeStamp called as tsSpellName.
		private float tsSpikes = 0f;
		private float tsBlink = 0f;
		private float tsEarthQuake = 0f;
		private float tsSlash = 0f;
		private float tsScream = 0f;
		private float tsFireball = 0f;
		private float tsRoots = 0f;
		private float tsCursedGround = 0f;
		private float tsSpin = 0f;
		private float tsBlizzard = 0f;
		private float tsCharge = 0f;
		
		[SerializeField]
		private Transform playerer; // Must be changed to scan in future
		
		// Particle systems
		[SerializeField]
		private ParticleSystem frostNovaPS;
		[SerializeField]
		private ParticleSystem lifeStealPS;
		[SerializeField]
		private ParticleSystem earthQuakePS;
		[SerializeField]
		private ParticleSystem slashPS;
		[SerializeField]
		private ParticleSystem screamPS;
		[SerializeField]
		private ParticleSystem rootsPS;
		[SerializeField]
		private ParticleSystem cursedGroundPS;
		[SerializeField]
		private ParticleSystem spinPS;
		[SerializeField]
		private ParticleSystem blizzardPS;
		[SerializeField]
		private ParticleSystem chargePS;
		
		// Spell Hit Boxes
		[SerializeField]
		private GameObject Slash_Collider;
		[SerializeField]
		private GameObject FrostNova_Collider;
		[SerializeField]
		private GameObject EarthQuake_Collider;
		[SerializeField]
		private GameObject Scream_Collider;
		[SerializeField]
		private GameObject Blink_Collider;
		[SerializeField]
		private GameObject Fireball_Collider;
		[SerializeField]
		private GameObject Roots_Collider;
		[SerializeField]
		private GameObject CursedGround_Collider;
		[SerializeField]
		private GameObject Spin_Collider;
		[SerializeField]
		private GameObject Blizzard_Collider;
		[SerializeField]
		private GameObject Charge_Collider;
		
		// Mute Spells boolean
		private bool muteAbilities = true;
		
		
		
		// Player model for Animator
		[SerializeField]
		private GameObject playerModel;
		
		
	// function that takes spells to Player.cs and gives orders to make Correct attack Move
	public void frostNova()
	{
		if(!muteAbilities)
		{
			cdPlayer = 1f;
			// Calls Player handler function with Spell variables, uses common attack Script
			// Checks cooldowns, in future might swap this function to Spellbook
			if(tsFrostNova <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
				frostNovaPS.Play();
				FrostNova_Collider.SetActive(!FrostNova_Collider.activeInHierarchy);
				FrostNova_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsFrostNova = timeStamp(cdPlayer, tsFrostNova);
			}
		}
	}
	public void earthQuake()
	{
		if(!muteAbilities)
		{		
			cdPlayer = 1f;
			if(tsEarthQuake <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
				earthQuakePS.Play();
				EarthQuake_Collider.SetActive(!EarthQuake_Collider.activeInHierarchy);
				EarthQuake_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsEarthQuake = timeStamp(cdPlayer, tsEarthQuake);
			}
		}
	}
	public void charge()
	{
		if(!muteAbilities)
		{		
			cdPlayer = 3f;
			if(tsCharge <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
				chargePS.Play();
				Charge_Collider.SetActive(!Charge_Collider.activeInHierarchy);
				Charge_Collider.GetComponent<spellBehavior>().ActivateObject();
				Charge_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsCharge = timeStamp(cdPlayer, tsCharge);
			}
		}	
	}
	public void Slash()
	{
		if(!muteAbilities)
		{
			cdPlayer = 1f;
		
			if(tsSlash <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
				slashPS.Play();
				// Activates collader
				Slash_Collider.SetActive(!Slash_Collider.activeInHierarchy);
				// Gets hitbox
				Slash_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsSlash = timeStamp(cdPlayer, tsSlash);
			}
		}
	}
	public void Scream()
	{
		if(!muteAbilities)
		{
			cdPlayer = 1f;
			if(tsScream <= Time.time)
			{
				// screamPS.Play();
			
				Scream_Collider.SetActive(!Scream_Collider.activeInHierarchy);
				playerModel.GetComponent<Animator>().Play("Attack");
				screamPS.Play();
				Scream_Collider.GetComponent<spellBehavior>().ActivateObject();
				Scream_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsScream = timeStamp(cdPlayer, tsScream);
			}
		}
	}
	public void Fireball()
	{
		if(!muteAbilities)
		{
			cdPlayer = 1f;
			if(tsFireball <= Time.time)
			{
				// screamPS.Play();
				playerModel.GetComponent<Animator>().Play("Attack");
			
				Fireball_Collider.SetActive(!Fireball_Collider.activeInHierarchy);
			
				// screamPS.Play();
				Fireball_Collider.GetComponent<spellBehavior>().ActivateObject();
				Fireball_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsFireball = timeStamp(cdPlayer, tsFireball);
			}
		}
	}
	public void Roots()
	{
		if(!muteAbilities)
		{
			cdPlayer = 5f;
			if(tsRoots <= Time.time)
			{
				// screamPS.Play();
				playerModel.GetComponent<Animator>().Play("Attack");
			
				Roots_Collider.SetActive(!Fireball_Collider.activeInHierarchy);
			
				rootsPS.Play();
				Roots_Collider.GetComponent<spellBehavior>().ActivateObject();
				Roots_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsRoots = timeStamp(cdPlayer, tsRoots);
			}
		}
		
	}
	public void CursedGround()
	{
		if(!muteAbilities)
		{
			cdPlayer = 10f;
			if(tsCursedGround <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
			
				CursedGround_Collider.SetActive(!CursedGround_Collider.activeInHierarchy);
			
				cursedGroundPS.Play();
				CursedGround_Collider.GetComponent<spellBehavior>().ActivateObject();
				CursedGround_Collider.GetComponent<HitBoxCheck>().enemiesHitList();
				tsCursedGround = timeStamp(cdPlayer, tsCursedGround);
			}
		}
	}
	public void Spin()
	{
		if(!muteAbilities)
		{
			cdPlayer = 6f;
		
			if(tsSpin <= Time.time)
			{
				muteAbilities = true;
				playerModel.GetComponent<Animator>().Play("Attack");
			
				Spin_Collider.SetActive(!Spin_Collider.activeInHierarchy);
			
				spinPS.Play();
				//Spin_Collider.GetComponent<spellBehavior>().ActivateObject();
				Spin_Collider.GetComponent<HitBoxCheck_Refresh>().enemiesHitList();
				tsSpin = timeStamp(cdPlayer, tsSpin);
			}
		}
		
	}
	public void Blizzard()
	{
		
		if(!muteAbilities)
		{
			cdPlayer = 6f;
		
			if(tsBlizzard <= Time.time)
			{
				playerModel.GetComponent<Animator>().Play("Attack");
			
				Blizzard_Collider.SetActive(!Blizzard_Collider.activeInHierarchy);
			
				blizzardPS.Play();
				Blizzard_Collider.GetComponent<spellBehavior>().ActivateObject();
				Blizzard_Collider.GetComponent<HitBoxCheck_Refresh>().enemiesHitList();
				tsBlizzard = timeStamp(cdPlayer, tsBlizzard);
			
			}
		}
	}
	
	public void spikes()
	{
		recover = 12;
		cdPlayer = 5f;
		if(tsSpikes <= Time.time)
		{
			playerModel.GetComponent<Animator>().Play("Attack");
			lifeStealPS.Play();
			playerer.GetComponent<Player>().healthRecover(recover);
			tsSpikes = timeStamp(cdPlayer, tsSpikes);
		}
	}
	public void blink()
	{
		cdPlayer = 10f;
		if (tsBlink <= Time.time)
		{
			tsBlink = timeStamp(cdPlayer, tsBlink);
		}
		
	}
	public void deActivateMute()
	{
		muteAbilities = false;
	}
	
	
	private float timeStamp(float cdPlayer, float tsSpell)
	{
		tsSpell = Time.time + cdPlayer;
		return tsSpell;
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
		public string namePlayer;
		
		public int healthPlayer; // Player Health
		public int maxHealthPlayer;
		public Slider healthBar;
		private float timeStampAttackPlayer;
		
		[SerializeField]
		private GameObject lose_Screen;
		
		public static Transform opponent; // Targeted person, in future no targeting only AOE
		
		
		
		
	// Use this for initialization
	void Start () 
	{
		maxHealthPlayer = healthPlayer;
		healthBar.value = calculateHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	// Example class of Spell
	public void AttackPlayer(int damagePlayer, float slowPlayer, int lifesteal, GameObject target, bool ticks, int ticksAmount)
	{
		// GameObject[] enemies;
		// enemies = GameObject.FindGameObjectsWithTag("opponent");	
		//timeStampAttackPlayer = Time.time + cdPlayer;
		target.GetComponent<Enemy>().getHitEnemy(damagePlayer, slowPlayer, ticks, ticksAmount);
		//Checks if Lifesteal applied, calls healthRecover incase
		if(lifesteal > 0)
		{
			healthRecover(lifesteal);
		}
	}
	// This function handles Player healing, both Lifesteal and Recover
	public void healthRecover(int recover)
	{
		healthPlayer = healthPlayer + recover;
		if(healthPlayer > maxHealthPlayer)
		{
			healthPlayer = maxHealthPlayer;
		}
		healthBar.value = calculateHealth();
	}
	// Handles all the movement player does, while using Spells
	public void blinkPlayer()
	{
		// Will figure out later, how to properly do it.
		transform.position = new Vector3(5f, 0, 0);

	}
	// handles incoming damage to player
	public void getHitPlayer(int enemyDamage) 
	{
		healthPlayer = healthPlayer - enemyDamage;
		healthBar.value = calculateHealth();
		if (healthPlayer <= 0)
		{
			lose_Screen.SetActive(!lose_Screen.activeInHierarchy);
			transform.position = new Vector3(0, -100f, 0);
			// SceneManager.LoadScene("menu");
			//Destroy(gameObject);
			// ADD: LevelManage call for Game ending Condition, loss
		}
	}
	
	float calculateHealth()
	{
		//Calculates HPbar HP to HpBarSlider 0-1 float 
		float healthPlayerFloat = healthPlayer;
		float maxHealthPlayerFloat = maxHealthPlayer;
		return (healthPlayerFloat / maxHealthPlayerFloat);
		
	}
}

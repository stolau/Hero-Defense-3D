using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
 {
	private string nameEnemy;
		
	public int healthEnemy;
		
	public int damageEnemy;
		
	public float rangeEnemy;
		
	private float timeStampEnemyAttack;
	private float tsSpecialAttack;
	
	private bool live = true;
		
	private float cooldownEnemyAttack = 5f;
	private int tickDamage;
	private int ticksCounter = 0;
		
	private GameObject lvlManager;
		
	[SerializeField]
	private GameObject enemyModel;
	
	// IEnumerators
	private IEnumerator waitTime;
	private IEnumerator ticksWaitTime;
		
	// For Healthbar, DONT CHANGE
	public Image healthBarEnemyImg;
	private int maxHealthEnemy;
	
	// HitEffect
	[SerializeField]
	private GameObject dotManager;
	private float dotDuration;
	
	// Floating combat text
	[SerializeField]
	private GameObject popupText;
	[SerializeField]
	private Transform enemyCanvas;
	private GameObject instPopupText;
	
	//Special Attack Behavior
	[SerializeField]
	private bool specialAbility;
	[SerializeField]
	private bool castTime;
	[SerializeField]
	private float cast_Duration;
	[SerializeField]
	private GameObject Enemy_SpecialAttack;
	[SerializeField]
	private int Enemy_SA_Damage;
	[SerializeField]
	private float Enemy_SA_CD;

	
	
	// Use this for initialization
	void Start ()
	{
		
	tsSpecialAttack = Random.Range(0f, 5f);
	tsSpecialAttack = tsSpecialAttack + Time.time;
	
	lvlManager = GameObject.FindWithTag("levelManager");
	maxHealthEnemy = healthEnemy;
	healthBarEnemyImg.fillAmount = calculateHealthEnemy();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(specialAbility)
		{
			if(tsSpecialAttack <= Time.time)
			{
				if(castTime)
				{
					gameObject.transform.GetComponent<EnemyMovement>().enemyMovementOnOff();
					StartCoroutine(castTime_IE(cast_Duration));
		
					tsSpecialAttack = Time.time + Enemy_SA_CD;
				}
				else
				{
					Enemy_SpecialAttack.SetActive(!Enemy_SpecialAttack.activeInHierarchy);
					Enemy_SpecialAttack.GetComponent<spellBehavior>().ActivateObject();
					Enemy_SpecialAttack.GetComponent<Enemy_HitBoxCheck>().enemiesHitList();
		
					tsSpecialAttack = Time.time + Enemy_SA_CD;
				}
			}
		}
		
	}
	
	// Every damage enemy takes damage goes through this function, when player HP reach 0, kills opponent
	public void getHitEnemy(int playerDamage, float slowPlayer, bool ticks, int ticksAmount)
	{
		if(ticks)
		{
			dotDuration = ticksAmount * 2f;
			gameObject.transform.GetComponent<dotManager>().activateFirstInLine(dotDuration);
			ticksCounter = 1;
			tickDamage = playerDamage / ticksAmount;
			healthEnemy = healthEnemy - tickDamage;
			StartCoroutine(damageTicks(ticksAmount, tickDamage, ticksCounter));
		}
		else
		{
			healthEnemy = healthEnemy - playerDamage;
			healthBarEnemyImg.fillAmount = calculateHealthEnemy();
			//For popup text
			//instPopupText = Instantiate(popupText, new Vector3(0, 0, 0) , Quaternion.identity);
			//instPopupText.transform.parent = enemyCanvas.transform;
			// instPopupText.GetComponent<Animator>().Play("Hit"); 
			
		
			if (healthEnemy <= 0)
			{
				// Checks out if opponent is alive, so damage done after dead wont confuse levelManager
				if(live)
				{
					live = false;
					enemyModel.GetComponent<Animator>().Play("Die");
					lvlManager.GetComponent<LevelManager>().deadEnemy();
					enemyModel.transform.parent = null;
					transform.position = new Vector3(0, -120f, 0);
					waitTime = fadeTime(2.5f);
					StartCoroutine(waitTime);
				}
			}
		}
	}
	public void AttackEnemy(Transform target)
	{
	
		// Checks if player attacked u in certain amount of time, and makes new attack if true
	if(timeStampEnemyAttack <= Time.time)
		{
			// In futre ADD: Special abilities and Monster specific Attack TimeStamp
			if(target != null && Vector3.Distance(target.position, transform.position) < rangeEnemy)
			{
				enemyModel.GetComponent<Animator>().Play("Attack");
				timeStampEnemyAttack = Time.time + cooldownEnemyAttack;
				target.GetComponent<Player>().getHitPlayer(damageEnemy);
			}
		}
	}
	public void SpecialAbility_Attack_Enemy(Transform target)
	{
		target.GetComponent<Player>().getHitPlayer(Enemy_SA_Damage);
	}
	
	// enemy gets to portal
	public void enemySurvive()
	{
		// GetComponent<LevelManager>().deadEnemy();
		lvlManager.GetComponent<LevelManager>().survivedEnemies();
		transform.position = new Vector3(0, -70f, 0);
		// Destroy(gameObject);
		// In future contacts the LevelManager and drops players Live by -1 and drops enemy count by -1
	}
	
	float calculateHealthEnemy()
	{
		//Calculates HPbar HP to HpBarSlider 0-1 float 
		float healthEnemyFloat = healthEnemy;
		float maxHealthEnemyFloat = maxHealthEnemy;
		return (healthEnemyFloat / maxHealthEnemyFloat);
	}
	private IEnumerator fadeTime(float fader)
	{
		yield return new WaitForSeconds(fader);
		enemyModel.transform.position = new Vector3(0, -90f, 0);
	}
	private IEnumerator castTime_IE(float cast_Duration)
	{
		enemyModel.GetComponent<Animator>().Play("SpecialAbility_Prepare");
		yield return new WaitForSeconds(cast_Duration);
		gameObject.transform.GetComponent<EnemyMovement>().enemyMovementOnOff();
		Enemy_SpecialAttack.SetActive(!Enemy_SpecialAttack.activeInHierarchy);
		Enemy_SpecialAttack.GetComponent<spellBehavior>().ActivateObject();
		Enemy_SpecialAttack.GetComponent<Enemy_HitBoxCheck>().enemiesHitList();
		
	}
	private IEnumerator damageTicks(int ticksAmount, int tickDamage, int ticksCounter)
	{
		while(ticksCounter < ticksAmount)
		{
			// hitEffectDuration = ticksAmount - ticksCounter;
			// hitEffectEnemyImg.fillAmount = (hitEffectDuration / ticksAmount);
				
			yield return new WaitForSeconds(2f);
			
			healthEnemy = healthEnemy - tickDamage;
			// healthBarEnemyImg.fillAmount = calculateHealthEnemy();
			ticksCounter = ticksCounter + 1;
				
			if (healthEnemy <= 0)
			{
				// Checks out if opponent is alive, so damage done after dead wont confuse levelManager
				if(live)
				{
					live = false;
					ticksCounter = ticksAmount + 1;
				
					enemyModel.GetComponent<Animator>().Play("Die");
					lvlManager.GetComponent<LevelManager>().deadEnemy();
					enemyModel.transform.parent = null;
					transform.position = new Vector3(0, -60f, 0);
					waitTime = fadeTime(2.5f);
					StartCoroutine(waitTime);
				}
			}
		}
	}
	public float getRangeEnemy()
	{
		return rangeEnemy;
	}
	
	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
	public Wave[] waves;
	[SerializeField]
	private int rounds;
	[SerializeField]
	private int maxRounds;
	[SerializeField]
	private int deathCount;
	[SerializeField]
	private int enemyWave;
	[SerializeField]
	private int surviveEnemy;
	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private Transform spawnPoint2;
	[SerializeField]
	private Transform instantiatePoint;
	[SerializeField]
	private string menu;
	
	// End game conditions
	private bool gameAlive = true;
	[SerializeField]
	private Text livesCount_Text;
	private int livesCount = 4;
	[SerializeField]
	private GameObject lose_Screen;
	[SerializeField]
	private GameObject win_Screen;
	
	// Waves calculating
	[SerializeField]
	private Text waveCount_Text;
	private int wavesCount = 1;
	private int waveCount;
	
	[SerializeField]
	private Text waveName_Text;
	[SerializeField]
	private GameObject waveName;
	
	// Time
	[SerializeField]
	private Text time_Text;
	private int initialTime_Min;
	private int initialTime_Sec;
	
	
	//Dont touch, defines loop
	private int i;
	private int s = 1;
	private int z;

	// Use this for initialization
	void Start ()
	{
			StartCoroutine(spawnEnemies());
			livesCount_Text.text = "LIVES: " + surviveEnemy.ToString() +  "/" + livesCount.ToString();
			initialTime_Min = System.DateTime.Now.Minute;
			initialTime_Sec = System.DateTime.Now.Second;
			

	}
	
	// Update is called once per frame
	void Update () 
	{
		time_Text.text = (System.DateTime.Now.Minute - initialTime_Min).ToString() + ":" + (System.DateTime.Now.Second - initialTime_Sec).ToString();
		// time_Text.text = Time.time.ToString();
	}
	// Checks how many enemies gets through portal.
	public void survivedEnemies()
	{
		surviveEnemy = surviveEnemy + 1;
		livesCount_Text.text = "LIVES: " + surviveEnemy.ToString() +  "/" + livesCount.ToString();
		deadEnemy(); // Calls deadEnemy to add 1 more dead enemy
		if (surviveEnemy == livesCount)
		{
			// Debug.Log("You Lose");
			// Game End condition
			gameAlive = false;
			lose_Screen.SetActive(!lose_Screen.activeInHierarchy);
			gameObject.SetActive(!gameObject.activeInHierarchy);
		}
	}
	// Keeps track on how many enemies alive, so new wave can spawn
	public void deadEnemy()
	{
		Wave wave = waves[rounds-1];
		
		deathCount = deathCount + 1;
		if(waveCount <= deathCount)
		{
			if(gameAlive)
			{
				StartCoroutine(spawnEnemies());
				deathCount = 0; //Resets deathCount to 0
			}
		}
	}
	private void enemySpawner(GameObject enemy, int g)
	{
		if(g == 0)
		{
			Instantiate(enemy, spawnPoint.position , Quaternion.identity);
		}
		if(g == 1)
		{
			Instantiate(enemy, spawnPoint2.position , Quaternion.identity);
		}
		// return 0;

	} 
	// Spawns enemies between 2 locations
	IEnumerator spawnEnemies()
	{
		waveCount_Text.text = "WAVES: " + wavesCount.ToString();
		wavesCount = wavesCount + 1;
		
		if(rounds == maxRounds)
		{
			// GAME END, player Wins
			win_Screen.SetActive(!win_Screen.activeInHierarchy);
			gameObject.SetActive(!gameObject.activeInHierarchy);			
			// SceneManager.LoadScene(menu);
		}
		else
		{
			Wave wave = waves[UnityEngine.Random.Range(0, 15)];
			// Prints Enemy Eave names
			waveCount = wave.count;
			waveName_Text.text = wave.name;
			waveName.SetActive(!waveName.activeInHierarchy);
			int g = 1;
			for(int i = 0; i < wave.count; i++)
			{
				enemySpawner(wave.enemyPrefab, g);
				if(g == 0)
				{
					g = 1;
				}
				else
				{
					g = 0;
				}
				yield return new WaitForSeconds(0.50f);
			}
			rounds = rounds + 1;
			waveName_Text.text = " ";
			waveName.SetActive(!waveName.activeInHierarchy);
		}
	}


}


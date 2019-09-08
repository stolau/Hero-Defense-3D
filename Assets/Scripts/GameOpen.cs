using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOpen : MonoBehaviour {
	
	[SerializeField]
	private GameObject LevelManager;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject HeroRB;
	
	private HeroMovement getMovementScript;
	private Player getPlayer;
	
	private int timeCounter = 5;
	
	[SerializeField]
	private List<GameObject> mapThemes = new List<GameObject>();
	[SerializeField]
	private GameObject mapTheme;
	
	
	[SerializeField]
	private Text countText;
	[SerializeField]
	private GameObject countText_Holder;
	
	private IEnumerator gameBegins;

	// Use this for initialization
	void Start ()
	{
		mapTheme = mapThemes[Random.Range(0, 4)];
		mapTheme.SetActive(!mapTheme.activeInHierarchy);
		startTheGame();
		getMovementScript = player.GetComponent<HeroMovement>();
		getPlayer = player.GetComponent<Player>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void startTheGame()
	{
		// Here we manage all the behaviors needed for game to startTheGame
		// Setup Countdown
		// Randomize enemies
		// Selecet hero
		StartCoroutine(gameOpening());
		
	}
	IEnumerator gameOpening()
	{
		countText.text = timeCounter.ToString();
		timeCounter = timeCounter - 1;
		while(timeCounter > 0)
		{
			yield return new WaitForSeconds(1f);
			
			countText.text = timeCounter.ToString();
			timeCounter = timeCounter - 1;
		}
		yield return new WaitForSeconds(1f);
		countText.text = "GO!";
		// Enables player movmenent and hitpoints
		getMovementScript.enabled = !getMovementScript.enabled;
		getPlayer.enabled = !getPlayer.enabled;
		LevelManager.SetActive(!LevelManager.activeInHierarchy);
		LevelManager.GetComponent<Spellbook>().deActivateMute();
		// Here activate Player Movement and Player scripts
		// Play Awakening animation
		yield return new WaitForSeconds(0.95f);
		countText_Holder.SetActive(!countText_Holder.activeInHierarchy);
		countText.text = " ";
		

	}
	IEnumerator gameClosing_Lose()
	{
		yield return new WaitForSeconds(2f);
	}
	IEnumerator gameClosing_uWin()
	{
		yield return new WaitForSeconds(2f);
	}
}

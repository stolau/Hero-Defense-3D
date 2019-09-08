using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {
	

	private GameObject spellManager;


	public void playAgainButton(string newGameLevel)
	{
		SceneManager.LoadScene(newGameLevel);
	}
	public void BackToMenu(string menu)
	{
		// Destroys older spellManager container before leaving the scene
		// To avoid duplicate spellManagers in Menu scene.
		// In future must implement better system so spell manager is not always reseted
		spellManager = GameObject.FindWithTag("SpellManager");
		Destroy(spellManager);
		SceneManager.LoadScene(menu);
	}
	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
	public TMP_Text checkpintCounter;
	public TMP_Text winsCounter;

	void Start()
	{
		VarManager.loadGame();
		checkpintCounter.text = "Checkpoint: " + VarManager.checkpointNumber;
		winsCounter.text = "Wins: " + VarManager.wins;
	}

	public void playButtonClick()
	{
		SceneManager.LoadScene("SampleScene");
	}
	
	public void quitButtonClick()
	{
		print("Application Quit!");
		Application.Quit();
	}

	public void clearButtonClick()
	{
		PlayerPrefs.DeleteAll();
		VarManager.clearData();
		Application.Quit();

		print("Data cleared!");
	}
}

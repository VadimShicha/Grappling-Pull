using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
	public TMP_Text checkpintCounter;

	void Start()
	{
		checkpintCounter.text = "Checkpoint: " + VarManager.checkpointNumber;
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
}

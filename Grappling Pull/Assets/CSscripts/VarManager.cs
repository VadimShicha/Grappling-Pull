using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarManager : MonoBehaviour
{
    //used to make sure if you go on an old checkpoint the checkpoint position doesn't change
    public static int checkpointNumber;
    public static Vector3 checkpointPos;
    public static bool respawned;
    //how many times you have beaten the game
    public static int wins;


    public static void saveGame()
	{
		PlayerPrefs.SetFloat("CheckpointPosX", VarManager.checkpointPos.x);
		PlayerPrefs.SetFloat("CheckpointPosY", VarManager.checkpointPos.y);
		PlayerPrefs.SetFloat("CheckpointPosZ", VarManager.checkpointPos.z);

		PlayerPrefs.SetInt("CheckpointNumber", VarManager.checkpointNumber);

		PlayerPrefs.SetInt("Wins", VarManager.wins);
	}

	public static void loadGame()
	{
		if(PlayerPrefs.HasKey("CheckpointPosX"))
		{
			VarManager.checkpointPos.x = PlayerPrefs.GetFloat("CheckpointPosX");
			VarManager.checkpointPos.y = PlayerPrefs.GetFloat("CheckpointPosY");
			VarManager.checkpointPos.z = PlayerPrefs.GetFloat("CheckpointPosZ");
		}

		if(PlayerPrefs.HasKey("CheckpointNumber"))
		{
			VarManager.checkpointNumber = PlayerPrefs.GetInt("CheckpointNumber");
		}

		if(PlayerPrefs.HasKey("Wins"))
		{
			VarManager.wins = PlayerPrefs.GetInt("Wins");
		}
	}

	public static void clearData()
	{
		checkpointNumber = 0;
		checkpointPos = Vector3.zero;
		wins = 0;
	}
}

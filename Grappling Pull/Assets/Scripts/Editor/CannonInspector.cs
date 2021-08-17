using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cannon))]
public class CannonInspector : Editor
{
	//string[] targetModeOptions = { "X and Y", "X only", "Y only" };

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GameObject go = GameObject.Find(target.name);

		//incase you don't have the Cannon component attached to this GameObject
		if(go.GetComponent<Cannon>() == null)
		{
			Debug.LogError("You need to have a Cannon on this GameObject");
			return;
		}
		


		if(GUILayout.Button("Fire"))
		{
			if(Application.isPlaying == true)
			{
				go.GetComponent<Cannon>().StartCoroutine("FireShot");
			}
			else
			{
				Debug.LogError("You must be in PlayMode to fire the cannon");
			}
		}

		
		//go.GetComponent<Cannon>().targetMode = EditorGUILayout.Popup("Target mode", go.GetComponent<Cannon>().targetMode,  targetModeOptions);
	}
}

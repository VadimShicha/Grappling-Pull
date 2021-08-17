using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cannon))]
public class CannonInspector : Editor
{
	string[] targetModeOptions = { "X and Y", "X only", "Y only" };

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GameObject go = GameObject.Find(target.name);

		/*
		//incase the GameObject ins't selected
		try
		{
			go = (GameObject)target;
		}
		catch(System.InvalidCastException)
		{
			//handle exception
			Debug.Log("SD");
			return;
		}
		*/

		//incase you don't have the Cannon component attached to this GameObject
		if(go.GetComponent<Cannon>() == null)
		{
			Debug.LogError("You need to have a Cannon on this GameObject");
			return;
		}
		


		if(GUILayout.Button("Fire"))
		{
			go.GetComponent<Cannon>().StartCoroutine("FireShot");
		}

		go.GetComponent<Cannon>().targetMode = EditorGUILayout.Popup("Target mode", go.GetComponent<Cannon>().targetMode,  targetModeOptions);
	}
}

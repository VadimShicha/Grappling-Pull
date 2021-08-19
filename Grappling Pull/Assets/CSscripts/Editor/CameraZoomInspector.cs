using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraZoom))]
public class CameraZoomInspector : Editor
{
	string[] options = {"Set zoom", "Add amount"};
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//prevent from getting exception when inspecting a Prefab
		try
		{
			GameObject go = GameObject.Find(target.name);

			go.GetComponent<CameraZoom>().zoomMode = EditorGUILayout.Popup("Zoom Mode", go.GetComponent<CameraZoom>().zoomMode, options);
		}
		catch(System.NullReferenceException)
		{
			//handle exception
		}
	}
}

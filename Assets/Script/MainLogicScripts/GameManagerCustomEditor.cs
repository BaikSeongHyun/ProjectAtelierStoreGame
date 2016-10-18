﻿using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( GameManager ) )]
public class GameManagerCustomEditor :Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if( GUILayout.Button( "ResetUI" ) )
		{
			GameManager manager = ( GameManager ) target;
			manager.SetUI();
		}
	}
}

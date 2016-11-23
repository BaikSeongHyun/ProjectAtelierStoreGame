using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof ( StageManager ) )]
public class AIManagerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if( GUILayout.Button( "RushAllCustomer" ) )
		{
			StageManager manager = ( StageManager ) target;
			manager.RushAllCustomer();
		}
	}
}

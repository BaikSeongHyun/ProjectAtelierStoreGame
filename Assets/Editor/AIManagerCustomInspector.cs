using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof ( AIManager ) )]
public class AIManagerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if( GUILayout.Button( "RushAllCustomer" ) )
		{
			AIManager manager = ( AIManager ) target;
			manager.RushAllCustomer();
		}
	}
}

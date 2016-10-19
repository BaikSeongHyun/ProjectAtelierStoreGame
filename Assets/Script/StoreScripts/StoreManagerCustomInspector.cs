using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof( StoreManager ) )]
public class StoreManagerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if( GUILayout.Button( "RecreateStoreData " ) )
		{
			StoreManager store = ( StoreManager ) target;
			store.RecreateStoreObject();
		}
	}
}

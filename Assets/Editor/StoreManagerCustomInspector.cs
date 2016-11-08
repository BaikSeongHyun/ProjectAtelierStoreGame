using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof ( StoreManager ) )]
public class StoreManagerCustomInspector : Editor
{
	// field
	bool setNavMesh = false;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		StoreManager store = ( StoreManager ) target;

		if( GUILayout.Button( "RecreateStoreData " ) )
		{			
			store.RecreateStoreObject();
		}

		if( !store.CreateComplete )
			setNavMesh = false;

		if( store.CreateComplete && !setNavMesh )
		{
			GameObjectUtility.SetStaticEditorFlags( store.StoreField.gameObject, StaticEditorFlags.NavigationStatic );

			NavMeshBuilder.BuildNavMesh();

			setNavMesh = true;
		}
	}
}

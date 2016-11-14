using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof ( GameManager ) )]
public class GameManagerCustomInspector :Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		GameManager manager = ( GameManager ) target;

		if( GUILayout.Button( "ResetPlayerData" ) )
		{			
			manager.SetDefaultStatus();
		}

		if( GUILayout.Button( "OpenStore" ) )
		{			
			manager.SetStoreOpenMode();
		}

		if( GUILayout.Button( "SetItemAll" ) )
		{
			manager.SetItemsInSellObject();
		}	

		if( GUILayout.Button( "DeleteItemAll" ) )
		{
			manager.DeleteItemsInSellObject();
		}

		if( GUILayout.Button( "SetItemIndex" ) )
		{
			manager.SetItemsInSellObjectUseIndex();
		}

	}
}

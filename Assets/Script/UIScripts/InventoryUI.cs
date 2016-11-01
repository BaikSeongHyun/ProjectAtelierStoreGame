using UnityEngine;
using System;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] InventoryElement[] slots;

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		slots = GetComponentsInChildren<InventoryElement>();
		foreach( InventoryElement element in slots )
			element.LinkComponentElement();
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			try
			{				
				slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i ] );
			}
			catch( IndexOutOfRangeException e )
			{
				Debug.Log( e.StackTrace );
				Debug.Log( e.Message );
				slots[ i ].UpdateComponentElement();
			}

		}
	}

	// on click item inventory element
	public void OnClickItemInventoryElement( int index )
	{
		// manager.GamePlayer.ItemSet[ index ];
	}


}

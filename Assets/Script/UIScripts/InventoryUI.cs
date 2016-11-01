using UnityEngine;
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
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i ] );
		}
	}

	// on click item inventory element
	public void OnClickItemInventoryElement( int index )
	{
		// manager.GamePlayer.ItemSet[ index ];
	}


}

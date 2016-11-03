using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] InventoryElement[] slots;
	[SerializeField] RectTransform content;

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		CreateInventoryElement();

		slots = GetComponentsInChildren<InventoryElement>();
		foreach( InventoryElement element in slots )
			element.LinkComponentElement();

		Debug.Log( this.gameObject.name );
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			try
			{
				if( this.gameObject.name == "ItemInventory" )
					slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i ] );
				else if( this.gameObject.name == "FurnitureInventory" )
					slots[ i ].UpdateComponentElement( manager.GamePlayer.HaveFurnitureSet[ i ] );
			}
			catch( IndexOutOfRangeException e )
			{
				Debug.Log( e.StackTrace );
				Debug.Log( e.Message );
				slots[ i ].UpdateComponentElement();
			}

		}
	}

	// create inventory element slot
	public void CreateInventoryElement()
	{
		int count = 0;

		// set scroll view content
		if( this.gameObject.name == "ItemInventory" )
		{			
			content = transform.Find( "ItemScrollRect/Viewport/Content" ).gameObject.GetComponent<RectTransform>();
			count = manager.GamePlayer.ItemSet.Length;
		}
		else if( this.gameObject.name == "FurnitureInventory" )
		{
			content = transform.Find( "FurnitureScrollRect/Viewport/Content" ).gameObject.GetComponent<RectTransform>();
			count = manager.GamePlayer.HaveFurnitureSet.Length;
		}

		content.sizeDelta = new Vector2( -10f, ( ( count / 4 ) * 160f ) );
		// create slot
		for( int i = 0; i < count; i++ )
		{
			Vector3 setPosition = new Vector3( 891f, 788f, 0f );
			GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "UIComponent/Slot" ), 
			                                              setPosition - new Vector3( -( i % 4 ) * 160f, ( i / 4 ) * 160f, 0f ), Quaternion.identity, content.transform );			
		}

		content.sizeDelta += new Vector2( -20f, 110f );
	
	}

	// on click method
	// on click item inventory element
	public void OnClickItemInventoryElement( int index )
	{
		// manager.GamePlayer.ItemSet[ index ];
	}
}
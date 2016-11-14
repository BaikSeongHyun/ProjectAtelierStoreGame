using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StorageUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;

	// create element
	[SerializeField] GameObject content;
	[SerializeField] Transform contentRoot;
	[SerializeField] Transform horizontalMargin;
	[SerializeField] Transform verticalMargin;

	// component element
	[SerializeField] DataElement[] slots;

	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();	
		CreateInventoryElement();
		LinkComponentElement();
	}

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();	

		slots = GetComponentsInChildren<DataElement>();
		foreach( DataElement element in slots )
			element.LinkComponentElement();
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			try
			{
				if( this.gameObject.name == "StorageUI" )
					slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i ] );
				else if( this.gameObject.name == "FurnitureSetUI" )
					slots[ i ].UpdateComponentElement( manager.GamePlayer.FurnitureSet[ i ] );
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
		if( this.gameObject.name == "StorageUI" )
		{
			int count = 0;

			count = manager.GamePlayer.ItemSet.Length;

			// link create element
			content = transform.Find( "ScrollView/Viewport/Content" ).gameObject;
			contentRoot = transform.Find( "ScrollView/Viewport/Content/Root" ).gameObject.transform;
			horizontalMargin = transform.Find( "ScrollView/Viewport/Content/HorizontalMargin" ).gameObject.transform;
			verticalMargin = transform.Find( "ScrollView/Viewport/Content/VerticalMargin" ).gameObject.transform;

			// create margin
			Vector3 horizontal = contentRoot.transform.position - horizontalMargin.transform.position;
			Vector3 vertical = contentRoot.transform.position - verticalMargin.transform.position;

			// create slot
			for( int i = 0; i < count; i++ )
			{
				GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "UIComponent/StorageSlot" ), transform.position, Quaternion.identity );
				temp.transform.SetParent( content.transform, false );
				temp.transform.position = contentRoot.transform.position - ( horizontal * ( i % 5 ) ) - ( vertical * ( i / 5 ) );

			}
		}
		else if( this.gameObject.name == "FurnitureSetUI" )
		{			
			int count = 0;
			count = manager.GamePlayer.FurnitureSet.Length;

			// link create element
			content = transform.Find( "ScrollView/Viewport/Content" ).gameObject;
			contentRoot = transform.Find( "ScrollView/Viewport/Content/Root" ).gameObject.transform;
			horizontalMargin = transform.Find( "ScrollView/Viewport/Content/HorizontalMargin" ).gameObject.transform;

			// create margin
			Vector3 horizontal = contentRoot.transform.position - horizontalMargin.transform.position;
		
			// create slot
			for( int i = 0; i < count; i++ )
			{
				GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "UIComponent/StorageSlot" ), transform.position, Quaternion.identity );
				temp.transform.SetParent( content.transform, false );
				temp.transform.position = contentRoot.transform.position - ( horizontal * i );
			}
		}		
	}

	// on click method
	// on click item inventory element
	public void OnClickItemInventoryElement( int index )
	{
		// manager.GamePlayer.ItemSet[ index ];
	}

	public void OnCilckExitInventoryUI()
	{
		this.gameObject.SetActive( false );
	}
}
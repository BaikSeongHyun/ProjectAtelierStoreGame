using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CreateUI : MonoBehaviour
{
	// high structure
	[SerializeField] StoreManager storeManager;

	// field - component element
	[SerializeField] DataElement[] listSlots;
	[SerializeField] DataElement selectedSlot;
	[SerializeField] GameObject singleResourceControl;
	[SerializeField] DataElement singleResourceSlot;
	[SerializeField] GameObject doubleResourceControl;
	[SerializeField] DataElement[] doubleResourceSlots;
	[SerializeField] GameObject tripleResourceControl;
	[SerializeField] DataElement[] tripleResourceSlots;
	[SerializeField] Text presentCreateCount;
	[SerializeField] Text limitCreateCount;
	[SerializeField] Image confirmButtonImage;

	// low structure
	[SerializeField] GameObject createResultUI;
	[SerializeField] CreateResultUI createResultUILogic;

	// field - logic data

	// property
	public int ListSlotLength { get { return listSlots.Length; } }

	// unity method
	// awake
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link element component
	public void LinkComponentElement()
	{
		// high structure
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();

		// component element
		listSlots = transform.Find( "CreateListBackground" ).GetComponentsInChildren<DataElement>();
		selectedSlot = transform.Find( "CreateSelectedBackground" ).GetComponentInChildren<DataElement>();

		singleResourceControl = transform.Find( "CreateResourceBackground" ).Find( "SingleResource" ).gameObject;
		singleResourceSlot = singleResourceControl.GetComponentInChildren<DataElement>();

		doubleResourceControl = transform.Find( "CreateResourceBackground" ).Find( "DoubleResource" ).gameObject;
		doubleResourceSlots = doubleResourceControl.GetComponentsInChildren<DataElement>();

		tripleResourceControl = transform.Find( "CreateResourceBackground" ).Find( "TripleResource" ).gameObject;
		tripleResourceSlots = tripleResourceControl.GetComponentsInChildren<DataElement>();

		presentCreateCount = transform.Find( "CreateNumberBackground" ).Find( "CreateNumberText" ).GetComponent<Text>();
		limitCreateCount = transform.Find( "CreateNumberBackground" ).Find( "CreateLimitNumberText" ).GetComponent<Text>();

		confirmButtonImage = transform.Find( "CreateResourceBackground" ).Find( "CreateConfirmButton" ).GetComponent<Image>();
	}

	// set (update only one time) component element
	public void SetComponentElement()
	{
		// list item
		for( int i = 0; i < listSlots.Length; i++ )
		{
			try
			{
				listSlots[ i ].UpdateComponentElement( storeManager.ViewItemGroup[ i + ( storeManager.PresentCreateListIndex * listSlots.Length ) ] );
			}
			catch( ArgumentOutOfRangeException e )
			{
				listSlots[ i ].UpdateComponentElement();
			}
		}

		// selected item
		selectedSlot.UpdateComponentElement( storeManager.SelectedItem );

		// create button
		if( storeManager.ItemCreate )
			confirmButtonImage.sprite = Resources.Load<Sprite>( "Image/UI/CreateUI/CreateConfirmOn" );
		else
			confirmButtonImage.sprite = Resources.Load<Sprite>( "Image/UI/CreateUI/CreateConfirmOff" );

		// text 
		presentCreateCount.text = storeManager.CreateCount.ToString();
		limitCreateCount.text = storeManager.CreateLimitCount.ToString();

		// resource slot 
		if( storeManager.SelectedItem == null || storeManager.SelectedItem.ID == 0 )
		{
			singleResourceControl.SetActive( false );
			doubleResourceControl.SetActive( false );
			tripleResourceControl.SetActive( false );
		}
		else
		{
			switch( storeManager.SelectedItem.ResourceIDSet.Length )
			{
				case 1: 
					singleResourceControl.SetActive( true );
					doubleResourceControl.SetActive( false );
					tripleResourceControl.SetActive( false );
					break;
				case 2: 
					singleResourceControl.SetActive( false );
					doubleResourceControl.SetActive( true );
					tripleResourceControl.SetActive( false );
					break;
				case 3: 
					singleResourceControl.SetActive( false );
					doubleResourceControl.SetActive( false );
					tripleResourceControl.SetActive( true );
					break;
			}
		}

		if( singleResourceControl.activeSelf )
			singleResourceSlot.UpdateComponentElement( storeManager.ResourceItem[ 0 ], storeManager.SelectedItem.ResourceCountSet[ 0 ] );
		if( doubleResourceControl.activeSelf )
		{
			for( int i = 0; i < doubleResourceSlots.Length; i++ )
			{
				doubleResourceSlots[ i ].UpdateComponentElement( storeManager.ResourceItem[ i ], storeManager.SelectedItem.ResourceCountSet[ i ] );
			}
		}
		if( tripleResourceControl.activeSelf )
		{
			for( int i = 0; i < tripleResourceSlots.Length; i++ )
			{
				tripleResourceSlots[ i ].UpdateComponentElement( storeManager.ResourceItem[ i ], storeManager.SelectedItem.ResourceCountSet[ i ] );
			}
		}
		
	}

	// on click method
	//
	public void OnClickMoveListIndex( int direction )
	{
		if( direction == -1 )
		{
			if( storeManager.PresentCreateListIndex > 0 )
				storeManager.PresentCreateListIndex--;
		}
		else
		{
			if( ( storeManager.ViewItemGroup.Count / listSlots.Length ) > storeManager.PresentCreateListIndex )
				storeManager.PresentCreateListIndex++;
		}

		SetComponentElement();
	}

	// select item
	public void OnClickSelectItem( int index )
	{
		storeManager.SelectCreateItem( index );

		SetComponentElement();
	}

	// set create count
	public void OnClickSetCreateCount( int increaseDirection )
	{
		storeManager.ControlCreateCount( increaseDirection );

		SetComponentElement();
	}

	// create item
	public void OnClickConfirmCreate()
	{
		if( storeManager.ItemCreate )
		{
			storeManager.CreateItemConfirm();
			SetComponentElement();
		}
	}

	// close this ui
	public void OnClickExitCreateUI()
	{
		this.gameObject.SetActive( false );
	}
}

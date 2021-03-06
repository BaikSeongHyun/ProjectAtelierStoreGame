﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class FurnitureMarketUI : MonoBehaviour
{
	// high structure
	[SerializeField] StoreManager storeManager;
    [SerializeField] SoundManager soundManager;

	// field - component element
	[SerializeField] DataElement[] listSlots;

	// low structure
	[SerializeField] GameObject purchaseUI;
	[SerializeField] PurchaseUI purchaseUILogic;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}


	// public method
	public void LinkComponentElement()
	{
		// high structure
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
        soundManager = GameObject.FindWithTag("GameLogic").GetComponent<SoundManager>();

        // field
        listSlots = new DataElement[10];

		for( int i = 0; i < 10; i++ )
		{
			listSlots[ i ] = transform.Find( "SellSlot" + ( i + 1 ) ).GetComponent<DataElement>(); 
		}

		// low structure
		purchaseUI = transform.Find( "PurchaseUI" ).gameObject;
		purchaseUILogic = purchaseUI.GetComponent<PurchaseUI>();
		purchaseUI.SetActive( false );
	}

	// set component element
	public void SetComponentElement()
	{
		for( int i = 0; i < listSlots.Length; i++ )
		{
			try
			{
				listSlots[ i ].UpdateComponentElement( storeManager.ViewFurnitureGroup[ i + ( storeManager.PresentFurnitureListIndex * listSlots.Length ) ] );
			}
			catch( ArgumentOutOfRangeException e )
			{
				listSlots[ i ].UpdateComponentElement();
			}
		}	
	}

	// on click method
	// on click items
	public void OnClickFurnitureItems( int index )
	{
        soundManager.PlayUISoundPlayer(4);

        if ( !storeManager.SelectFurniture( index, listSlots.Length ) )
			return;
		purchaseUI.SetActive( true );
		purchaseUILogic.SetComponentElement();
	}

	// move list index
	public void OnClickMoveIndex( int direction )
	{
        soundManager.PlayUISoundPlayer(4);

        if ( direction == -1 )
		{
			if( storeManager.PresentFurnitureListIndex > 0 )
				storeManager.PresentFurnitureListIndex--;
		}
		else if( direction == 1 )
		{
			if( storeManager.ViewFurnitureGroup.Count / listSlots.Length > storeManager.PresentFurnitureListIndex )
				storeManager.PresentFurnitureListIndex++;
		}

		SetComponentElement();
	}

	// exit this ui
	public void OnClickExitFurnitureMarketUI()
	{
		this.gameObject.SetActive( false );
        soundManager.PlayUISoundPlayer(4);
    }

}
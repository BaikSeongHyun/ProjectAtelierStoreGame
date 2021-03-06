﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomizeUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;

	// component element
	[SerializeField] GameObject furnitureSetUI;
	[SerializeField] StorageUI furnitureSetUILogic;
	[SerializeField] GameObject buttonSet;

	// low structure
	[SerializeField] GameObject popUpSell;
	[SerializeField] Text sellPrice;

	// public method
	// link component
	public void LinkComponentElement()
	{
		// link structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
	
		// button 
		buttonSet = transform.Find( "ButtonSet" ).gameObject;
		buttonSet.SetActive( false );
		furnitureSetUI = transform.Find( "FurnitureSetUI" ).gameObject;
		furnitureSetUILogic = furnitureSetUI.GetComponent<StorageUI>();

		// low structure
		popUpSell = transform.Find( "PopUpSell" ).gameObject;
		popUpSell.SetActive( false );
		sellPrice = popUpSell.transform.Find( "SellPriceText" ).GetComponent<Text>();
	}

	// ui update
	public void UpdateComponentElement()
	{
		if( storeManager.PresentSelectedFurniture == null )
			buttonSet.SetActive( false );
		else
			buttonSet.SetActive( true );

		if( furnitureSetUI.activeSelf )
		{
			furnitureSetUILogic.UpdateComponentElement();
		}
	}

	// on click method
	// move furniture object
	public void OnClickMoveFurnitureObject( int direction )
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		storeManager.AllocateFurnintureObjectPositionSet( direction );
	}

	// rotation furniture object
	public void OnClickRotateFurnitureObject( int direction )
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		storeManager.AllocateFurnitureObjectRotationSet( direction );
	}

	// confirm locate furniture object
	public void OnClickAllocateConfirmFurnitureObject()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		storeManager.ConfirmMoveFurnitureObject();
	}

	// allocate collect -> and recollect in furniture inventory
	public void OnClickAllocateCollectFurnitureObject()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		storeManager.CancelAllocateFurnitureObject();
	}

	// exit customizing -> mode : store mode
	public void OnClickExitCustomizing()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		if( storeManager.PresentSelectedFurniture == null )
			manager.SetStoreMode();
	}

	// pop up on
	public void OnClickPopUpActivate()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		popUpSell.SetActive( true );
		sellPrice.text = ( ( int ) ( storeManager.PresentSelectedFurniture.InstanceData.Furniture.Price * 0.8f ) ).ToString();
	}

	// sell furniture object
	public void OnClickSellFurniture()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		// sell furniture
		storeManager.SellFurnitureObject();

		// close pop up
		popUpSell.SetActive( false );
	}

	// exit pop up
	public void OnClickExitPopup()
	{
		manager.SoundManager.PlayUISoundPlayer(4);
		popUpSell.SetActive( false );
	}
}

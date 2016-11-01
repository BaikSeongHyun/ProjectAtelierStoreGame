﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store mode game ui
public class StoreUI : MonoBehaviour
{
	// high structure
	GameManager manager;

	// component element
	// - text set
	[SerializeField] Text storeStep;
	[SerializeField] Text playerLevel;
	[SerializeField] Text playerName;
	[SerializeField] Text gold;
	[SerializeField] Text gem;

	// - object set
	[SerializeField] GameObject itemInventory;
	[SerializeField] InventoryUI itemInventoryLogic;
	[SerializeField] GameObject furnitureInventory;
	[SerializeField] InventoryUI furnitureInventoryLogic;
	[SerializeField] GameObject playerStatus;

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// text element
		storeStep = transform.Find( "StoreStep" ).GetComponentInChildren<Text>();
		playerLevel = transform.Find( "PlayerLevel" ).GetComponentInChildren<Text>();
		playerName = transform.Find( "PlayerName" ).GetComponentInChildren<Text>();
		gold = transform.Find( "Gold" ).GetComponentInChildren<Text>();
		gem = transform.Find( "Gem" ).GetComponentInChildren<Text>();

		// object element
		itemInventory = transform.Find( "ItemInventory" ).gameObject;
		itemInventory.SetActive( false );
		itemInventoryLogic = itemInventory.GetComponent<InventoryUI>();
		itemInventoryLogic.LinkComponentElement();


		furnitureInventory = transform.Find( "FurnitureInventory" ).gameObject;
		furnitureInventory.SetActive( false );
		furnitureInventoryLogic = furnitureInventory.GetComponent<InventoryUI>();
		furnitureInventoryLogic.LinkComponentElement();


		playerStatus = transform.Find( "PlayerStatus" ).gameObject;
		playerStatus.SetActive( false );
	}

	// update element
	public void UpdateComponentElement()
	{
		// store step
		storeStep.text = "Step " + manager.GamePlayer.StoreData.StoreStep;

		// level
		playerLevel.text = "Lv. " + manager.GamePlayer.Level;

		// player name
		playerName.text = manager.GamePlayer.Name;

		// gold
		gold.text = manager.GamePlayer.Gold.ToString();

		// gem
		gem.text = manager.GamePlayer.Gem.ToString();

		// if item inventory open -> update item inventory
		if( itemInventory.activeSelf )
			itemInventoryLogic.UpdateComponentElement();
		
		// if furniture inventory open -> update furniture inventory
		if( furnitureInventory.activeSelf )
			furnitureInventoryLogic.UpdateComponentElement();
	}

	// on click method
	// on click item inventory button
	public void OnClickItemInventoryButton()
	{
		itemInventory.SetActive( !itemInventory.activeSelf );
		furnitureInventory.SetActive( false );
	}

	// on click furniture inventory button
	public void OnClickFurnitureInventoryButton()
	{
		itemInventory.SetActive( false );
		furnitureInventory.SetActive( !furnitureInventory.activeSelf );
	}

	// on click player status button
	public void OnClickPlayerStatusButton()
	{
		playerStatus.SetActive( !playerStatus.activeSelf );
	}

	// on click mode customizing button
	public void OnClickCustomizingButton()
	{
		manager.SetCutomizeingMode();
	}


	// on click furniture inventory element
	public void OnClickFurnitureInventoryElement( int index )
	{
		// manager.GamePlayer.FurnitureSet[ index ];
	}

	// on click furniture shop items
	public void OnClickFurnitureShopElement( int index )
	{
		Debug.Log( index.ToString() + " 번째 가구 아이템을 구매합니다." );
		//manager.GamePlayer.AddFurnitureData();
	}
}

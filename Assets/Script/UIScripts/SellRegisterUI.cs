using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SellRegisterUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] CharacterManager charManager;


	// field - ui component
	[SerializeField] Image itemImage;
	[SerializeField] Text itemNameText;
	[SerializeField] Text itemTypeText;
	[SerializeField] Text itemPriceText;
	[SerializeField] Text registerCountText;
	[SerializeField] InputField registerSellPriceText;

	// field - temp data
	[SerializeField] int presentFurnitureSlotIndex;
	[SerializeField] int presentStorageSlotIndex;
	[SerializeField] int presentTempItemCounter;
	[SerializeField] ItemInstance tempData;

	[SerializeField] ItemData temp;

	// property
	public int PresentFurnitureSlotIndex { get { return presentFurnitureSlotIndex; } set { presentFurnitureSlotIndex = value; } }


	// unity method
	void Awake()
	{
		LinkCompoenentElement();
	}



	// public method
	// link component element
	public void LinkCompoenentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
		charManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();

		// component element
		itemImage = transform.Find( "StorageSlot" ).Find( "ElementIcon" ).GetComponent<Image>();
		itemPriceText = transform.Find( "ItemPrice" ).Find( "ItemPriceBack" ).Find( "Text" ).GetComponent<Text>();
		registerCountText = transform.Find( "ItemCount" ).Find( "ItemCountBack" ).Find( "Text" ).GetComponent<Text>();
		registerSellPriceText = transform.Find( "ItemSellPrice" ).Find( "ItemSellPriceField" ).GetComponent<InputField>();

	}

	// set component element
	public void SetFirstComponentElement()
	{
		if( stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ] == null || stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].Item == null || stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].Item.ID == 0 )
		{
			itemPriceText.text = "";
			itemImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/none" );
			presentTempItemCounter = 0;
			registerCountText.text = presentTempItemCounter.ToString();
			registerSellPriceText.text = 0.ToString();
		}
		else
		{
			itemImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].Item.File );
			itemPriceText.text = stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].Item.Price.ToString();
			registerSellPriceText.text = stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].SellPrice.ToString();
			presentTempItemCounter = stageManager.PresentSelectedFurniture.SellItem[ presentFurnitureSlotIndex ].Count;
			registerCountText.text = presentTempItemCounter.ToString();
		}
	}

	public void UpdateComponentElement()
	{
		itemImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + tempData.Item.File );
		itemPriceText.text = tempData.Item.Price.ToString();
	}

	// on click method
	// on click register item in register slot
	public void OnClickSetRegisterItem( int index )
	{
		try
		{
			if( ( manager.PresentMode == GameManager.GameMode.StoreOpenPreprocess ) || ( manager.PresentMode == GameManager.GameMode.StoreOpen ) )
			{
				// save slot index
				presentStorageSlotIndex = index + storeManager.StorageIndex * ( manager.GamePlayer.ItemSet.Length / 3 );
				// load item
				temp = DataManager.FindItemDataByID( manager.GamePlayer.ItemSet[ index + storeManager.StorageIndex * ( manager.GamePlayer.ItemSet.Length / 3 ) ].Item.ID );
			
				// check slot item
				for( int i = 0; i < stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet.Length; i++ )
				{
					if( stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet[ i ] == ( int ) temp.Type )
					{
						// check set slot item type
						tempData = new ItemInstance( temp.ID, 0, 0 );
						UpdateComponentElement();
						return;
					}
				}
			}
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.Message );
			Debug.Log( e.StackTrace );
		}
	}

	// on click confirm into slot
	public void OnClickConfirmRegisterItem()
	{
		if( ( Int32.Parse( registerCountText.text ) <= 0 ) || ( Int32.Parse( registerSellPriceText.text ) <= 0 ) )
			return;

		// ui off
		this.gameObject.SetActive( false );

		// set register data
		tempData.Count = Int32.Parse( registerCountText.text );
		tempData.SellPrice = Int32.Parse( registerSellPriceText.text );

		// slot data precess
		manager.GamePlayer.ItemSet[ presentStorageSlotIndex ].RegisterSellItems( tempData.Count );

		// add item
		stageManager.PresentSelectedFurniture.SetSellItem( tempData, presentFurnitureSlotIndex );

		// playerCharacter set items
		charManager.PlayerableCharacter.ItemSetting();

	}

	// on click set item price
	public void OnClickSetCount( int scale )
	{
		this.gameObject.SetActive( true );

		if( scale == 1 )
		{
			// check type
			for( int i = 0; i < stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet.Length; i++ )
			{
				if( stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet[ i ] == ( int ) temp.Type )
				{
					// item count add
					// check max limit & check have item
					if( ( presentTempItemCounter + 1 <= stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemCountSet[ i ] ) && ( presentTempItemCounter + 1 <= manager.GamePlayer.ItemSet[ presentStorageSlotIndex ].Count ) )
					{
						presentTempItemCounter++;
						registerCountText.text = presentTempItemCounter.ToString();
					}
				}
			}
		}
		else if( scale == -1 )
		{
			if( presentTempItemCounter - 1 >= 0 )
			{
				// item count subtract
				// check zero
				presentTempItemCounter--;
				registerCountText.text = presentTempItemCounter.ToString();
			}

		}
	}

	// on click close register ui
	public void OnClickCloseRegisterUI()
	{
		this.gameObject.SetActive( false );
	}
}

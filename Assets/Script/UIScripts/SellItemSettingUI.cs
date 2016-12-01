using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SellItemSettingUI : MonoBehaviour
{
	// high structure
	[SerializeField] StageManager stageManager;
	[SerializeField] UIManager mainUI;

	// field - for ui
	[SerializeField] Image background;
	[SerializeField] Text groupText;
	[SerializeField] DataElement[] slots;

	// sub ui
	[SerializeField] GameObject sellRegisterUI;
	[SerializeField] SellRegisterUI sellRegisterUILogic;



	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		// high structure 
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// component element
		background = transform.Find( "Background" ).GetComponent<Image>();
		groupText = transform.Find( "GroupText" ).GetComponent<Text>();
		
		sellRegisterUI = transform.Find( "SellRegisterUI" ).gameObject;
		sellRegisterUILogic = sellRegisterUI.GetComponent<SellRegisterUI>();
		
		slots = GetComponentsInChildren<DataElement>();
	}

	// initialize element
	public void InitializeElement()
	{
		// set background
		switch( stageManager.PresentSelectedFurniture.InstanceData.Furniture.Step )
		{
			case 1:
				background.sprite = Resources.Load<Sprite>( "Image/UI/StageUI/FirstStepSellBackground" ); 
				break;
			case 2:
				background.sprite = Resources.Load<Sprite>( "Image/UI/StageUI/SecondStepSellBackground" ); 
				break;
			case 3:
				background.sprite = Resources.Load<Sprite>( "Image/UI/StageUI/ThirdStepSellBackground" ); 
				break;
		}

		// string initialiize
		string groupString = "";

		// set item group
		for( int i = 0; i < stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet.Length; i++ )
		{
			switch( stageManager.PresentSelectedFurniture.InstanceData.Furniture.SellItemGroupSet[ i ] )
			{
				case 1:
					groupString += "재료 ";
					break;
				case 2:
					groupString += "포션 ";
					break;
				case 3:
					groupString += "허브 ";
					break;
				case 4:
					groupString += "마법가루 ";
					break;
				case 5:
					groupString += "스크롤 ";
					break;
				case 6:
					groupString += "스태프 ";
					break;
				case 7:
					groupString += "마법책 ";
					break;
			}
		}
	
		groupText.text = groupString;
		sellRegisterUI.SetActive( false );

		UpdateComponentElement();
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			try
			{
				// set defalut
				if( stageManager.PresentSelectedFurniture.SellItem[ i ] == null || stageManager.PresentSelectedFurniture.SellItem[ i ].Item == null || stageManager.PresentSelectedFurniture.SellItem[ i ].Item.ID == 0 )
				{
					slots[ i ].Count.enabled = false;
					slots[ i ].ElementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );
				}
				// set item icon
				else
				{
					slots[ i ].Count.enabled = true;
					slots[ i ].UpdateComponentElement( stageManager.PresentSelectedFurniture.SellItem[ i ] );
				}
			}
			catch( IndexOutOfRangeException e )
			{
				Debug.Log( e.Message );
				slots[ i ].LockSlot();
			}
		}
	}

	// on click method
	// open setting slot
	public void OnClickOpenSettingSlot( int index )
	{
		if( slots[ index ].IsLocked )
			return;
		else
		{			
			sellRegisterUI.SetActive( true );
			sellRegisterUILogic.PresentFurnitureSlotIndex = index;
			sellRegisterUILogic.SetFirstComponentElement();
		}
	}

	// using storage

	// closs this ui
	public void OnClickCloseSellItemSettingUI()
	{
		this.gameObject.SetActive( false );
		mainUI.StorageUI.SetActive( false );
	}
}

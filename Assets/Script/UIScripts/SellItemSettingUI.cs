using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SellItemSettingUI : MonoBehaviour
{
	// high structure
	[SerializeField] StoreManager storeManager;

	// field - for ui
	[SerializeField] Image background;
	[SerializeField] Text groupText;
	[SerializeField] DataElement[] slots;


	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
		background = transform.Find( "Background" ).GetComponent<Image>();
		groupText = transform.Find( "GroupText" ).GetComponent<Text>();
		slots = GetComponentsInChildren<DataElement>();
	}

	// initialize element
	public void InitializeElement()
	{
		// set background
		switch( storeManager.PresentAllocateObject.InstanceData.Furniture.Step )
		{
			case 1:
				background.sprite = Resources.Load<Sprite>( "Image/UI/SellUI/1stStepSellBackground" ); 
				break;
			case 2:
				background.sprite = Resources.Load<Sprite>( "Image/UI/SellUI/2ndStepSellBackground" ); 
				break;
			case 3:
				background.sprite = Resources.Load<Sprite>( "Image/UI/SellUI/3rdStepSellBackground" ); 
				break;
		}

		string groupString = "";
		// set item group
		for( int i = 0; i < storeManager.PresentAllocateObject.InstanceData.Furniture.SellItemGroupSet.Length; i++ )
		{
			switch( storeManager.PresentAllocateObject.InstanceData.Furniture.SellItemGroupSet[ i ] )
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
					groupString += "미법책 ";
					break;
			}
		}	
	}

	// update component element
	public void UpdateComponentElement()
	{
		for( int i = 0; i < slots.Length; i++ )
		{
			try
			{
				// set defalut
				if( storeManager.PresentAllocateObject.SellItem[ i ].Item == null )
					slots[ i ].ElementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );
				// set item icon
				else
					slots[ i ].ElementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon" + storeManager.PresentAllocateObject.SellItem[ i ].Item.File );
			}
			catch( IndexOutOfRangeException e )
			{
				Debug.Log( e.Message );
				slots[ i ].LockSlot();
			}
		}
	}

	// on click method
	//

}

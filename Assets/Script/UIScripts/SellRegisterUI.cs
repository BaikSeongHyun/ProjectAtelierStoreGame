using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SellRegisterUI : MonoBehaviour
{
	// high structure
	[SerializeField] StageManager stageManager;

	// field - ui component
	[SerializeField] Text itemNameText;
	[SerializeField] Text itemTypeText;
	[SerializeField] Text itemPriceText;
	[SerializeField] Text registerPriceText;
	[SerializeField] Text registerCountText;

	// field - temp data
	[SerializeField] ItemInstance tempData;


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
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();

	}

	// set component element
	public void SetComponentElement()
	{

	}

	// on click method
	// on click register item in register slot
	public void OnClickRegisterItem( int index )
	{
		
//		// load item
//		ItemData temp = DataManager.FindItemDataByID( manager.GamePlayer.ItemSet[ index + storeUILogic.StorageUILogic.PresentStepIndex * ( manager.GamePlayer.ItemSet.Length / 3 ) ].Item.ID );
//			
//		// check slot item
//		for( int i = 0; i < storeManager.PresentAllocateObject.InstanceData.Furniture.SellItemGroupSet.Length; i++ )
//		{
//			if( storeManager.PresentAllocateObject.InstanceData.Furniture.SellItemGroupSet[ i ] == ( int ) temp.Type )
//			{
//				// check set slot item type
//				tempData = new ItemInstance( temp.ID, 0, 0 );
//				return;
//			}
//		}
	}

	// on click set item price
	public void OnClickSetItemPrice( int scale )
	{

	}

	// on click set count
	public void OnClickSetItemCount( int scale )
	{

	}
}

using UnityEngine;
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
	}

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
		storeManager.AllocateFurnintureObjectPositionSet( direction );
	}

	// rotation furniture object
	public void OnClickRotateFurnitureObject( int direction )
	{
		storeManager.AllocateFurnitureObjectRotationSet( direction );
	}

	// confirm locate furniture object
	public void OnClickAllocateConfirmFurnitureObject()
	{
		storeManager.ConfirmMoveFurnitureObject();
	}

	// allocate cancel -> and recollect in furniture inventory
	public void OnClickAllocateCancelFurnitureObject()
	{
		storeManager.CancelAllocateFurnitureObject();
	}

	// exit customizing -> mode : store mode
	public void OnClickExitCustomizing()
	{
		if( storeManager.PresentSelectedFurniture == null )
			manager.SetStoreMode();
	}

}

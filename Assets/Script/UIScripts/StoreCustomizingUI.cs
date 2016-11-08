using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreCustomizingUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;

	// component element
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
	}

	public void UpdateComponentElement()
	{
		if( storeManager.PresentAllocateObject == null )
			buttonSet.SetActive( false );
		else
			buttonSet.SetActive( true );
	}

	// on click method
	// move furniture object
	public void OnClickMoveFurnitureObject( int direction )
	{
		storeManager.AllocateFurnintureObjectPositionSet( direction );
	}

	// rotation furniture object
	public void OnClickRotateFurnitureObject(int direction)
	{
		storeManager.AllocateFurnitureObjectRotationSet(direction);
	}

	// confirm locate furniture object
	public void OnClickAllocateConfirmFurnitureObject()
	{
		storeManager.ConfirmAllocateFurnitureObject();
	}

	// exit customizing -> mode : store mode
	public void OnClickExitCustomizing()
	{
		manager.SetStoreMode();
	}
}

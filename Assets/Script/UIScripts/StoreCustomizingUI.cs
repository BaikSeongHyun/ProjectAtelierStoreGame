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
		Vector3 moveDirection = Vector3.zero;
		switch( direction )
		{
			case 1:
				moveDirection = new Vector3( 0f, 0f, -0.5f );
				break;
			case 2:
				moveDirection = new Vector3( 0f, 0f, 0.5f );
				break;
			case 3:
				moveDirection = new Vector3( 0.5f, 0f, 0f );
				break;
			case 4:
				moveDirection = new Vector3( -0.5f, 0f, 0f );
				break;

		}

		// set plane scale
		float planeScale = 0.0f;

		switch( manager.GamePlayer.StoreData.StoreStep )
		{
			case 1:
				planeScale = 10f;
				break;
			case 2:
				planeScale = 15f;
				break;
			case 3:
				planeScale = 20f;
				break;
		}

		storeManager.PresentAllocateObject.ChangeObjectPosition( storeManager.PresentAllocateObject.transform.position + moveDirection, planeScale );
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

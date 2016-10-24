using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreCustomizingSet : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] FurnitureDataPopUp popUp;
	[SerializeField] Button xAxisUpButton;
	[SerializeField] Button xAxisDownButton;
	[SerializeField] Button zAxisUpButton;
	[SerializeField] Button zAxisDownButton;
	[SerializeField] Image allocateCheck;

	// public method
	// link component
	public void LinkComponentElement()
	{
		// link structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// link component element
		popUp = GetComponent<FurnitureDataPopUp>();
	}

	public void UpdateComponentElement()
	{

	}

	// on click method
	// move furniture object
	public void OnClickMoveFurnitureObject( int direction )
	{

	}

	// exit customizing -> mode : store mode
	public void OnClickExitCustomizing()
	{
		manager.SetStoreMode();
	}
}

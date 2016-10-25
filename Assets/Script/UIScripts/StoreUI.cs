using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store mode game ui
public class StoreUI : MonoBehaviour
{
	// component element

	// - text set
	[SerializeField] Text storeStep;
	[SerializeField] Text playerLevel;
	[SerializeField] Text playerName;
	[SerializeField] Text money;
	[SerializeField] Text gem;

	// - button set
	[SerializeField] Button buyButton;
	[SerializeField] Button sellButton;
	[SerializeField] Button statusButton;
	[SerializeField] Button itemInventoryButton;
	[SerializeField] Button furnitureInventoryButton;
	[SerializeField] Button customizingButton;

	// - object set
	[SerializeField] GameObject itemInventory;
	[SerializeField] GameObject furnitureInventory;
	[SerializeField] GameObject playerStatus;

	// public method
	// link component
	public void LinkComponentElement()
	{

	}

	// update element
	public void UpdateComponentElement()
	{
		// store step

		// level

		// money

		// gem

		// if item inventory open -> update item inventory

		// if furniture inventory open -> update furniture inventory
	}

	// on click method
	// on click item inventory button
	public void OnClickItemInventoryButton()
	{

	}

	// on click furniture inventory button
	public void OnClickFurnitureInventoryButton()
	{

	}

	// on click player status button
	public void OnClickPlayerStatusButton()
	{

	}

	// on click mode customizing button
	public void OnClickCustomizingButton()
	{

	}

	// on click item inventory element
	public void OnClickItemInventoryElement( int index )
	{

	}

	// on click  furniture inventory element
	public void OnClickFurnitureInventoryElement( int index )
	{

	}
}

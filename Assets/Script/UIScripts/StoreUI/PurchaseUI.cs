using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PurchaseUI : MonoBehaviour
{
	// high structure
	[SerializeField] StoreManager storeManager;

	// field - component element
	[SerializeField] DataElement furnitureSlot;
	[SerializeField] Text furnitureName;
	[SerializeField] Text price;

	// unity method
	// awake
	void Awake()
	{
		LinkComponentElement(); 
	}

	// public method
	// link component & data
	public void LinkComponentElement()
	{
		// high structure
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();

		// field
		furnitureSlot = GetComponentInChildren<DataElement>();
		furnitureName = transform.Find( "Name" ).GetComponent<Text>();
		price = transform.Find( "Price" ).GetComponent<Text>();
	}

	// set component use furniture data
	public void SetComponentElement()
	{
		furnitureSlot.UpdateComponentElement( storeManager.SelectedFurniture );
		furnitureName.text = storeManager.SelectedFurniture.Name;
		price.text = storeManager.SelectedFurniture.Price.ToString();
	}

	// on click method
	// buy item
	public void OnClickBuyFurniture()
	{
		if( storeManager.BuyFurniture() )
			this.gameObject.SetActive( false );
	}

	// exit this ui
	public void OnClickExitPurchaseUI()
	{
		this.gameObject.SetActive( false );
	}
}

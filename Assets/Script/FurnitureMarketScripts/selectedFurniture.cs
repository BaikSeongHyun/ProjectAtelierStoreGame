using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class selectedFurniture : MonoBehaviour {

    public FurnitureData furniture;

    public FurnitureMarketUI ui;

    public Button btn;

	void Start () {
        ui = GameObject.Find("FurnitureMarketUI").GetComponent<FurnitureMarketUI>();
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(DataSendToUI);
	}
	
	void Update () {
	
	}

    public void DataSendToUI()
    {
        ui.PurchasePopup(furniture);
        Debug.Log(furniture.Name);
    }
}

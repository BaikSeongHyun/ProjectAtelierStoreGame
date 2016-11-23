using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] GameObject storeUI;
	[SerializeField] StoreUI storeUILogic;
	[SerializeField] GameObject storeCustomizingSet;
	[SerializeField] CustomizeUI storeCustomizingSetLogic;
	[SerializeField] GameObject loadingScene;
	[SerializeField] GameObject fieldUI;
	[SerializeField] FieldUI fieldUILogic;
<<<<<<< HEAD
	[SerializeField] GameObject mixUI;
	[SerializeField] MixUI mixUILogic;
	[SerializeField] GameObject sellItemSettingUI;
	[SerializeField] SellItemSettingUI sellItemSettingUILogic;
=======
>>>>>>> 1c13fdbb3dfc8092509b2564f784c17975647968
	[SerializeField] Text testField;

    [SerializeField] GameObject furnitureMarket;
    [SerializeField] FurnitureMarketUI furnitureMarketUI;

	// unity method
	// awake
	void Awake()
	{		
		LinkComponentElement();
	}

	// public method
	// link element component
	public void LinkComponentElement()
	{
		// link structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// link element component
		storeUI = transform.Find( "StoreUI" ).gameObject;
		storeUILogic = storeUI.GetComponent<StoreUI>();
		storeUILogic.LinkComponentElement();

		storeCustomizingSet = transform.Find( "CustomizeUI" ).gameObject;
		storeCustomizingSetLogic = storeCustomizingSet.GetComponent<CustomizeUI>();
		storeCustomizingSetLogic.LinkComponentElement();

		fieldUI = transform.Find( "FieldUI" ).gameObject;
		fieldUILogic = fieldUI.GetComponent<FieldUI>();

		loadingScene = transform.Find( "LoadingScene" ).gameObject;

        furnitureMarket = transform.Find("FurnitureMarketUI").gameObject;
        furnitureMarketUI = furnitureMarket.GetComponent<FurnitureMarketUI>();
        furnitureMarketUI.LinkComponentElement();
    }

	// update ui component
	public void UIUpdate()
	{
		if( storeUI != null )
		{
			if( storeUI.activeSelf )
				storeUILogic.UpdateComponentElement();

			if( storeCustomizingSet.activeSelf )
				storeCustomizingSetLogic.UpdateComponentElement();

			if( fieldUI.activeSelf )
				fieldUILogic.UpdateComponentElement();

            if (furnitureMarket.activeSelf)
                furnitureMarketUI.UpdateComponentElement();

        }
		testField.text = "x:" + Camera.main.transform.position.x + ", y: " + Camera.main.transform.position.y + ", z: " + Camera.main.transform.position.z;
	}
	
	// mode change
	public void UIModeChange()
	{
		if( manager == null )
			LinkComponentElement();
		
		switch( manager.PresentMode )
		{
			case GameManager.GameMode.Start:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
<<<<<<< HEAD
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( true );
=======
>>>>>>> 1c13fdbb3dfc8092509b2564f784c17975647968
				break;
			case GameManager.GameMode.Loading:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
<<<<<<< HEAD
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( true );
=======
>>>>>>> 1c13fdbb3dfc8092509b2564f784c17975647968
				break;
			case GameManager.GameMode.Store:
				storeUI.SetActive( true );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
<<<<<<< HEAD
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( false );
=======
>>>>>>> 1c13fdbb3dfc8092509b2564f784c17975647968
				break;
			case GameManager.GameMode.StoreCustomizing:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( true );
				fieldUI.SetActive( false );
				break;
			case GameManager.GameMode.Village:
				break;
			case GameManager.GameMode.Field:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( true );
				fieldUILogic.ResetCard();
				break;
		}
	}
	
	// set loading scene
	public void LoadingSceneState( bool state )
	{
		loadingScene.SetActive( state );
	}
	
<<<<<<< HEAD
	// set sell item setting io
	public void ActivateSellItemSettingUI()
	{
		sellItemSettingUI.SetActive( true );
		sellItemSettingUILogic.InitializeElement();
	}

	// set mix ui activate
	public void ActivateMixUI()
	{
		mixUI.SetActive( true );
	}
=======
	// set create ui
	
	// set storage ui
>>>>>>> 1c13fdbb3dfc8092509b2564f784c17975647968
}
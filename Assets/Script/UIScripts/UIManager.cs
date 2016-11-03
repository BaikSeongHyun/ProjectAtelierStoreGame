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
	[SerializeField] StoreCustomizingUI storeCustomizingSetLogic;
	[SerializeField] GameObject loadingScene;

	[SerializeField] public GameObject questPopup;

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

		storeCustomizingSet = transform.Find( "StoreCustomizingUI" ).gameObject;
		storeCustomizingSetLogic = storeCustomizingSet.GetComponent<StoreCustomizingUI>();
		storeCustomizingSetLogic.LinkComponentElement();

		loadingScene = transform.Find( "LoadingScene" ).gameObject;
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
				break;
			case GameManager.GameMode.Loading:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				break;
			case GameManager.GameMode.Store:
				storeUI.SetActive( true );
				storeCustomizingSet.SetActive( false );
				break;
			case GameManager.GameMode.StoreCustomizing:
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( true );
				break;
			case GameManager.GameMode.Village:
				break;
			case GameManager.GameMode.Field:
                
                break;
		}
	}

	public void UILoadingState(bool state)
	{
		loadingScene.SetActive( state );
	}

	public void UIUpdate()
	{
        if (storeUI != null)
        {
            if (storeUI.activeSelf)
                storeUILogic.UpdateComponentElement();

            if (storeCustomizingSet.activeSelf)
                storeCustomizingSetLogic.UpdateComponentElement();
        }
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] GameObject loginForm;
	[SerializeField] LoginForm loginFormLogic;
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
		loginForm = transform.Find( "LoginForm" ).gameObject;
		loginFormLogic = loginForm.GetComponent<LoginForm>();
		loginFormLogic.LinkComponentElement();

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
				loginForm.SetActive( true );
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				break;
			case GameManager.GameMode.Loading:
				loginForm.SetActive( false );
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				break;
			case GameManager.GameMode.Store:
				loginForm.SetActive( false );
				storeUI.SetActive( true );
				storeCustomizingSet.SetActive( false );
				break;
			case GameManager.GameMode.StoreCustomizing:
				loginForm.SetActive( false );
				storeUI.SetActive( false );
				storeCustomizingSet.SetActive( true );
				break;
			case GameManager.GameMode.Village:
				break;
			case GameManager.GameMode.Field:
				break;
		}
	}

	public void UIUpdate()
	{
		if( storeUI.activeSelf )
			storeUILogic.UpdateComponentElement();

		if( storeCustomizingSet.activeSelf )
			storeCustomizingSetLogic.UpdateComponentElement();
	}


}

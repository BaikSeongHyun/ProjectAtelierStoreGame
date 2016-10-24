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
	[SerializeField] GameObject upsideBar;
	[SerializeField] UpsideBar upsideBarLogic;
	[SerializeField] GameObject downsideBar;
	[SerializeField] DownsideBar downsideBarLogic;
	[SerializeField] GameObject loadingScene;

	public GameObject questPopup;

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

		upsideBar = transform.Find( "UpsideBar" ).gameObject;
		upsideBarLogic = upsideBar.GetComponent<UpsideBar>();
		upsideBarLogic.LinkComponentElement();

		downsideBar = transform.Find( "DownsideBar" ).gameObject;
		downsideBarLogic = downsideBar.GetComponent<DownsideBar>();
		downsideBarLogic.LinkComponentElement();

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
				upsideBar.SetActive( false );
				downsideBar.SetActive( false );
				loadingScene.SetActive( false );
				break;
			case GameManager.GameMode.Loading:
				loginForm.SetActive( false );
				upsideBar.SetActive( false );
				downsideBar.SetActive( false );
				loadingScene.SetActive( true );
				break;
			case GameManager.GameMode.Store:
				loginForm.SetActive( false );
				upsideBar.SetActive( true );
				downsideBar.SetActive( true );
				loadingScene.SetActive( false );
                questPopup.SetActive(false);

                break;
			case GameManager.GameMode.StoreCustomizing:
				break;
			case GameManager.GameMode.Village:
				break;
			case GameManager.GameMode.Field:
				break;
		}
	}

	public void UIUpdate()
	{
		if( upsideBar.activeSelf )
			upsideBarLogic.UpdateComponentElement( manager.GamePlayer );
		//float temp = playerInfo.expRenew;

		//expbar.transform.localScale = new Vector3( temp, expbar.transform.localScale.y, expbar.transform.localScale.z );
	}


}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] GameObject storeUI;
	[SerializeField] StoreUI storeUILogic;

	[SerializeField] GameObject storageUI;
	[SerializeField] StorageUI storageUILogic;

	[SerializeField] GameObject storeCustomizingSet;
	[SerializeField] CustomizeUI storeCustomizingSetLogic;

	[SerializeField] GameObject createUI;
	[SerializeField] CreateUI createUILogic;

	[SerializeField] GameObject sellItemSettingUI;
	[SerializeField] SellItemSettingUI sellItemSettingUILogic;

	[SerializeField] GameObject furnitureMarketUI;
	[SerializeField] FurnitureMarketUI furnitureMarketUILogic;

	[SerializeField] GameObject stageUI;
	[SerializeField] StageUI stageUILogic;

	[SerializeField] GameObject resultUI;
	[SerializeField] ResultUI resultUILogic;

	[SerializeField] GameObject resultRewardUI;
	[SerializeField] ResultRewardUI resultRewardUILogic;

	[SerializeField] GameObject chatSceneUI;
	[SerializeField] ChatSceneUI chatSceneUILogic;

	[SerializeField] GameObject characterCreateUI;
	[SerializeField] CharacterCreateUI characterCreateUILogic;

	[SerializeField] GameObject loadingScene;

	// effect data
	[SerializeField] GameObject clickEffect;

	// property
	public GameObject StorageUI { get { return storageUI; } }

	public GameObject CustomizeUI { get { return storeCustomizingSet; } }

	public GameObject CreateUI { get { return createUI; } }

	public CreateUI CreateUILogic { get { return createUILogic; } }

	public GameObject FurnitureMarketUI { get { return furnitureMarketUI; } }

	public FurnitureMarketUI FurnitureMarketUILogic { get { return furnitureMarketUILogic; } }

	public GameObject ChatSceneUI { get { return chatSceneUI; } }

	public ChatSceneUI ChatSceneUILogic { get { return chatSceneUILogic; } }

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

		storageUI = transform.Find( "StorageUI" ).gameObject;
		storageUILogic = storageUI.GetComponent<StorageUI>();

		storeCustomizingSet = transform.Find( "CustomizeUI" ).gameObject;
		storeCustomizingSetLogic = storeCustomizingSet.GetComponent<CustomizeUI>();
		storeCustomizingSetLogic.LinkComponentElement();

		createUI = transform.Find( "CreateUI" ).gameObject;
		createUILogic = createUI.GetComponent<CreateUI>();

		sellItemSettingUI = transform.Find( "SellItemSettingUI" ).gameObject;
		sellItemSettingUILogic = sellItemSettingUI.GetComponent<SellItemSettingUI>();

		furnitureMarketUI = transform.Find( "FurnitureMarketUI" ).gameObject;
		furnitureMarketUILogic = furnitureMarketUI.GetComponent<FurnitureMarketUI>();

		stageUI = transform.Find( "StageUI" ).gameObject;
		stageUILogic = stageUI.GetComponent<StageUI>();

		resultRewardUI = transform.Find( "ResultRewardUI" ).gameObject;
		resultRewardUILogic = resultRewardUI.GetComponent<ResultRewardUI>();

		resultUI = transform.Find( "ResultUI" ).gameObject;
		resultUILogic = resultUI.GetComponent<ResultUI>();

		chatSceneUI = transform.Find( "ChatScene" ).gameObject;
		chatSceneUILogic = chatSceneUI.GetComponent<ChatSceneUI>();

		characterCreateUI = transform.Find( "CharacterCreateUI" ).gameObject;
		characterCreateUILogic = characterCreateUI.GetComponent<CharacterCreateUI>();

		loadingScene = transform.Find( "LoadingScene" ).gameObject;
	}

	// update ui component
	public void UIUpdate()
	{
		

		if( storeUI.activeSelf )
			storeUILogic.UpdateComponentElement();

		if( storeCustomizingSet.activeSelf )
			storeCustomizingSetLogic.UpdateComponentElement();

		if( resultRewardUI.activeSelf )
			resultRewardUILogic.UpdateComponentElement();
		
		if( storageUI.activeSelf )
			storageUILogic.UpdateComponentElement();

		if( stageUI.activeSelf )
			stageUILogic.UpdateComponentElement();

		if( sellItemSettingUI.activeSelf )
			sellItemSettingUILogic.UpdateComponentElement();
	}
	
	// mode change
	public void UIModeChange()
	{
		if( manager == null )
			LinkComponentElement();
		
		switch( manager.PresentMode )
		{
			case GameManager.GameMode.Create:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );	
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				furnitureMarketUI.SetActive( false );
				resultUI.SetActive( false );
				chatSceneUI.SetActive( false );
				resultRewardUI.SetActive( false );
				createUI.SetActive( false );
				loadingScene.SetActive( false );
				characterCreateUI.SetActive( true );
				break;
			case GameManager.GameMode.Start:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );	
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				resultUI.SetActive( false );
				chatSceneUI.SetActive( false );
				resultRewardUI.SetActive( false );
				createUI.SetActive( false );
				characterCreateUI.SetActive( false );
				loadingScene.SetActive( true );
				break;
			case GameManager.GameMode.Loading:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );	
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				resultUI.SetActive( false );
				chatSceneUI.SetActive( false );
				resultRewardUI.SetActive( false );
				createUI.SetActive( false );
				loadingScene.SetActive( false );
				characterCreateUI.SetActive( false );
				loadingScene.SetActive( true );
				break;
			case GameManager.GameMode.Store:
				storeUI.SetActive( true );
				chatSceneUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				furnitureMarketUI.SetActive( false );
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( false );
				resultRewardUI.SetActive( false );
				break;
			case GameManager.GameMode.StoreCustomizing:
				chatSceneUI.SetActive( false );
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				furnitureMarketUI.SetActive( false );
				storeCustomizingSet.SetActive( true );
				break;
			case GameManager.GameMode.StoreOpenPreprocess:
				chatSceneUI.SetActive( false );
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				furnitureMarketUI.SetActive( false );
				stageUI.SetActive( true );
				stageUILogic.ResetComponent();
				break;
			case GameManager.GameMode.StageResult:
				chatSceneUI.SetActive( false );
				storageUI.SetActive( false );
				stageUI.SetActive( false );
				stageUILogic.ResetComponent();
				sellItemSettingUI.SetActive( false );
				resultUI.SetActive( true );
				resultUILogic.SetComponentElement();
				break;
		}
	}
	
	// set loading scene
	public void LoadingSceneState( bool state )
	{
		loadingScene.SetActive( state );
	}
	

	// set sell item setting io
	public void ActivateSellItemSettingUI()
	{
		sellItemSettingUI.SetActive( true );
		sellItemSettingUILogic.InitializeElement();
		storageUI.SetActive( true );
	}

	// set result reward ui
	public void ActivateResultRewardUI()
	{
		resultRewardUI.SetActive( true );
		resultRewardUILogic.ResetCard();
	}
	
}
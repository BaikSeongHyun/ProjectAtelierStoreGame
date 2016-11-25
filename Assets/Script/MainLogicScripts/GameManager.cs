using UnityEngine;
using System;
using System.Collections;

// all game process control
public class GameManager : MonoBehaviour
{
	// game logic simple data field
	[SerializeField] GameMode presentGameMode;
	[SerializeField] bool gameStart = false;
	[SerializeField] int index;

	// game instance data field
	[SerializeField] PlayerData player;

	// network data field
	[SerializeField] PacketQueue receiveQueue;
	[SerializeField] PacketQueue sendQueue;

	// control logic field
	[SerializeField] StageManager stageManager;
	[SerializeField] CharacterManager characterManager;
	[SerializeField] FieldManager fieldManager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] UIManager mainUI;
	[SerializeField] CameraControl cameraControl;

	// enum state
	public enum GameMode : int
	{
		Start = 1,
		Loading = 2,
		Store = 3,
		StoreCustomizing = 4,
		StoreOpenPreprocess = 5,
		StoreOpen = 6,
		Village = 7,
		Field = 8}

	;

	// property
	public PlayerData GamePlayer { get { return player; } set { player = value; } }

	public GameMode PresentMode { get { return presentGameMode; } }

	// unity mono behaviour method
	// awake
	void Awake()
	{
		LinkLogicElement();
		DataInitailize();
		GameStart();
	}

	// fixed update -> process network logic
	void FixedUpdate()
	{

	}

	// update -> process game logic
	void Update()
	{
		// main ui component update
		mainUI.UIUpdate();

		switch( presentGameMode )
		{
			case GameMode.Store:
				storeManager.StorePolicy();
				break;
			case GameMode.StoreOpenPreprocess:
				stageManager.StagePreprocessPolicy();
				break;
			case GameMode.StoreCustomizing:
				storeManager.CustomzingFurnitureObject();
				break;
			case GameMode.Field:
				break;
		}

		if( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKey( KeyCode.Escape ) )
				Application.Quit();
		}
	}

	// late update -> process camera logic
	void LateUpdate()
	{
		//cameraControl.MoveObject();
  //      cameraControl.SetCameraPosition();

    }

	// private method
	// Link distribute game logic manager
	private void LinkLogicElement()
	{
		stageManager = GetComponent<StageManager>();
		characterManager = GetComponent<CharacterManager>();
		fieldManager = GetComponent<FieldManager>();
		storeManager = GetComponent<StoreManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		cameraControl = Camera.main.gameObject.GetComponent<CameraControl>();
	}

	// game manager data initialize
	private void DataInitailize()
	{
		// set mainUI
		presentGameMode = GameMode.Start;
		mainUI.UIModeChange();
	}


	// public method
	// game start -> for client test
	public void GameStart()
	{
		StartCoroutine( GameStartLoadingProcess() );
		gameStart = true;
	}

	// check game data loading
	// go to field
	public void CheckFieldDataLoading()
	{
		Debug.Log( "Field data loading process" );

	}

	// set ui -> use present game mode
	public void SetUI()
	{
		mainUI.UIModeChange();
	}

	// set store mode
	public void SetStoreMode()
	{
		presentGameMode = GameMode.Store;
		storeManager.StoreField.IsCustomizing = false;
		SetUI();
	}

	// set store customizing mode
	public void SetCutomizeingMode()
	{
		presentGameMode = GameMode.StoreCustomizing;
		storeManager.StoreField.IsCustomizing = true;
		SetUI();
	}

	// set field mode
	public void SetFieldMode()
	{
		presentGameMode = GameMode.Field;
		SetUI();
	}

	// set store open mode
	public void SetStoreOpenMode()
	{
		presentGameMode = GameMode.StoreOpen;
		stageManager.StoreOpen();
		SetUI();
	}

	public void SetDefaultStatus()
	{
		player.SetDefaultStatus();
	}

	// set item all allocate sell object
	public void SetItemsInSellObject()
	{
		player.InsertSellItem();
	}

	// set item all allocate sell object
	public void DeleteItemsInSellObject()
	{
		player.DeleteSellItem();
	}

	public void SetItemsInSellObjectUseIndex()
	{
		player.InsertSellItemUseIndex( index );
	}

	// coroutine section
	// game start loading Process
	IEnumerator GameStartLoadingProcess()
	{
		player = DataManager.GetPlayerData();

		// start store create
		StartCoroutine( storeManager.CreateStoreObject() );

		while( true )
		{		
			// loading game data false -> wait	
			if( !storeManager.CreateComplete )
			{
				// set main ui state -> loading state
				yield return 1.0f;
			}
			// loading game data success -> start game
			else
			{
				try
				{
					// set camera mode
					//cameraControl.SetCameraDefault( GameMode.Store );
				}
				catch( NullReferenceException e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
				catch( UnassignedReferenceException e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}

				// set main ui state -> store state
				presentGameMode = GameMode.Store;
				mainUI.UIModeChange();
				mainUI.LoadingSceneState( false );
				yield break;
			}
		}
	}

	// loading store mode process
	IEnumerator StoreLoadingProcess()
	{
		mainUI.LoadingSceneState( true );

		while( true )
		{		
			// loading game data false -> wait	
			if( !storeManager.CreateComplete )
			{
				// set main ui state -> loading state
				yield return 1.0f;
			}
			// loading game data success -> start game
			else
			{
				// set main ui state -> store state
				yield break;
			}
		}
	}

	// loading field mode process
	IEnumerator FieldLoadingProcess()
	{
		mainUI.LoadingSceneState( true );

		while( true )
		{		
			if( DataManager.CheckPlayerDataLoading() )
			{				
				// loading game data false -> wait	
				if( !storeManager.CreateComplete )
				{
					// set main ui state -> loading state
					yield return 1.0f;
				}
			// loading game data success -> start game
			else
				{
					// set main ui state -> store state
					yield break;
				}
			}
		}
	}
}
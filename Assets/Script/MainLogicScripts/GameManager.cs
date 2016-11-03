using UnityEngine;
using System;
using System.Collections;

// all game process control
public class GameManager : MonoBehaviour
{
	// game logic simple data field
	[SerializeField] GameMode presentGameMode;

	// game instance data field
	[SerializeField] PlayerData player;
	[SerializeField] GameObject playerCharacter;

	// network data field
	[SerializeField] PacketQueue receiveQueue;
	[SerializeField] PacketQueue sendQueue;

	// control logic field
	[SerializeField] AIManager aiManager;
	[SerializeField] CharacterManager characterManager;
	[SerializeField] FieldManager fieldManager;
	[SerializeField] NetworkController networkContoller;
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
		Village = 5,
		Field = 6}

	;

	// property
	public PlayerData GamePlayer { get { return player; } set { player = value; } }

	public GameMode PresentMode { get { return presentGameMode; } }

	public GameObject PlayerCharacter { get { return playerCharacter; } }

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
			case GameMode.StoreCustomizing:
				storeManager.CustomzingFurnutureObject();
				break;
			case GameMode.Field:
				
				break;
		}
	}

	// late update -> process camera logic
	void LateUpdate()
	{
		cameraControl.SetCameraPosition();
	}


	// customize method (private & public)

	// private method
	// Link distribute game logic manager
	private void LinkLogicElement()
	{
		aiManager = GetComponent<AIManager>();
		characterManager = GetComponent<CharacterManager>();
		fieldManager = GetComponent<FieldManager>();
		networkContoller = GetComponent<NetworkController>();
		storeManager = GetComponent<StoreManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
		cameraControl = Camera.main.gameObject.GetComponent<CameraControl>();
	}

	// game manager data initialize
	private void DataInitailize()
	{
		// game instance data field
		player = new PlayerData();


		// network data field;
		receiveQueue = new PacketQueue();
		sendQueue = new PacketQueue();

		// set mainUI
		presentGameMode = GameMode.Start;
		mainUI.UIModeChange();

		// player prefs loading
		player.AddFurnitureData( DataManager.FindFurnitureDataByUID( 40001 ) );

		// data connection
		player.StoreData.StoreStep = 1;
		player.AllocateFurnitureInstance( 0 );
	}


	// public method
	// game start -> for client test
	public void GameStart()
	{
		StartCoroutine( GameStartLoadingProcess() );
	}

	// check game data loading
	// if game data all loading (return true)
	public bool CheckGameDataLoading()
	{
		Debug.Log( "Game data loading process" );
		return storeManager.CreateStoreObject();
	}

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

	// coroutine section
	// game start loading Process
	IEnumerator GameStartLoadingProcess()
	{
		mainUI.UILoadingState( true );

		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
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
					cameraControl.SetCameraDefault( GameMode.Store );
				}
				catch( UnassignedReferenceException e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}

				// set main ui state -> store state
				presentGameMode = GameMode.Store;
				mainUI.UIModeChange();
				mainUI.UILoadingState( false );
				yield break;
			}
		}
	}

	// loading store mode process
	IEnumerator StoreLoadingProcess()
	{
		mainUI.UILoadingState( true );

		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
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
		mainUI.UILoadingState( true );

		while( true )
		{		
			// loading game data false -> wait	
			if( !CheckGameDataLoading() )
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

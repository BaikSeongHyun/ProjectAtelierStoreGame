﻿using UnityEngine;
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

	// control logic field
	[SerializeField] CharacterManager characterManager;
	[SerializeField] FieldManager fieldManager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] StageManager stageManager;
	[SerializeField] UIManager mainUI;
	[SerializeField] CameraControl cameraControl;
	[SerializeField] SoundManager soundManager;

	// enum state
	public enum GameMode : int
	{
		Create = 0,
		Start = 1,
		Loading = 2,
		Store = 3,
		StoreCustomizing = 4,
		StoreOpenPreprocess = 5,
		StoreOpen = 6,
		StageResult = 7}

	;

	// property
	public PlayerData GamePlayer { get { return player; } set { player = value; } }

	public SoundManager SoundManager { get { return soundManager; } }

	public GameMode PresentMode { get { return presentGameMode; } }

	// unity mono behaviour method
	// awake
	void Awake()
	{
		LinkLogicElement();
		DataInitailize();
		GameStart();
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
				fieldManager.FieldPolicy();
				break;
			case GameMode.StoreCustomizing:
				storeManager.CustomzingFurnitureObject();
				break;
			case GameMode.StoreOpenPreprocess:
				if( !characterManager.MrKakao.IsSendMessage )
					break;
				storeManager.StorePolicy();
				stageManager.StageItemSellRegisterPolicy();
				break;
			case GameMode.StoreOpen:
				stageManager.StageProcessPolicy();
				break;
		}

		if( Application.platform == RuntimePlatform.Android )
		{
			if( Input.GetKey( KeyCode.Escape ) )
				StartCoroutine( SaveAndQuitGame() );
		}
	}

	// late update -> process camera logic
	void LateUpdate()
	{
		cameraControl.SetCameraPosition();
	}

	// private method
	// Link distribute game logic manager
	private void LinkLogicElement()
	{
		stageManager = GetComponent<StageManager>();
		characterManager = GetComponent<CharacterManager>();
		fieldManager = GetComponent<FieldManager>();
		storeManager = GetComponent<StoreManager>();
		soundManager = GetComponent<SoundManager>();

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
		StartCoroutine( SaveCycle() );
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
		storeManager.IsCustomizing = false;
		SetUI();
		cameraControl.SetCameraDefault( presentGameMode );

		StartCoroutine( fieldManager.CreateFieldItemPolicy() );
	}

	// set store customizing mode
	public void SetCutomizeingMode()
	{
		presentGameMode = GameMode.StoreCustomizing;
		storeManager.SetCustomizing();
		SetUI();
		cameraControl.SetCameraDefault( presentGameMode );
	}

	// recreate store object -> data clear & create
	public void RecreateStoreField()
	{
		// set game mode
		presentGameMode = GameMode.Loading;
		
		// step up
		player.StoreData.RankUp();

		// save game data
		DataManager.SavePlayerData();

		// mode to loading
		DataInitailize();

		// data clear
		storeManager.ClearStoreObject();
		fieldManager.ClearObject();

		// create store object
		GameStart();
	}

	// set store open preprocess mode
	public void SetStoreOpenPreprocessMode()
	{
		presentGameMode = GameMode.StoreOpenPreprocess;
		stageManager.CreateGameInformation();
		characterManager.ActivateKakao();
		SetUI();
	}

	// set store open mode
	public void SetStoreOpenMode()
	{
		presentGameMode = GameMode.StoreOpen;
		stageManager.StoreOpen();
	}

	// set store stage end
	public void SetStoreStageEnd()
	{
		presentGameMode = GameMode.StageResult;

		player.Gold += stageManager.ResultData.RewardGold;
		player.StoreData.AddExperience( stageManager.ResultData.RewardExp );

		SetUI();

		stageManager.SetDataReset();
		stageManager.CustomerReset();
	}

	public void SetDefaultStatus()
	{
		player.SetDefaultStatus();
	}

	// coroutine section
	// game start loading Process
	IEnumerator GameStartLoadingProcess()
	{
		// load game data
		DataManager.LoadPlayerData();

		player = DataManager.GetPlayerData();

		if( DataManager.PlayFirst )
		{
			presentGameMode = GameMode.Create;
			SetUI();
			yield break;
		}
		else
		{
			presentGameMode = GameMode.Loading;
			SetUI();				
		}

		yield return new WaitForSeconds( 2f );
		// set field data
		fieldManager.CheckStepFieldData();

		// start store create
		StartCoroutine( storeManager.CreateStoreObject() );
		soundManager.PlayBackgroundAudio( );

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
					cameraControl.SetCameraDefault( GameMode.Store );
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
				StartCoroutine( fieldManager.CreateFieldItemPolicy() );
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

	// cycle data save
	IEnumerator SaveCycle()
	{
		while( true )
		{
			yield return new WaitForSeconds( 60f );

			DataManager.SavePlayerData();
			Debug.Log( "Save game data" );

		}
	}

	// game end process
	IEnumerator SaveAndQuitGame()
	{
		DataManager.SavePlayerData();

		yield return new WaitForSeconds( 2f );

		Application.Quit();
	}
}
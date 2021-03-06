﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DataManager : MonoBehaviour
{
	// high structure
	[SerializeField] static GameManager manager;

	// field - for check load
	[SerializeField] static bool playFirst;
	[SerializeField] static bool playerDataLoading;

	// field - data maps
	[SerializeField] static PlayerData playerData;
	[SerializeField] static List<StageResultData> tempDataSet;
	[SerializeField] static Dictionary<int , List<StageResultData>> stageResultSet;
	[SerializeField] static Dictionary<int, FurnitureData> furnitureSet;
	[SerializeField] static Dictionary<int, ItemData> itemSet;
	[SerializeField] static Dictionary<int, FieldData> fieldDataSet;
	[SerializeField] static Dictionary<int, StoreData> storeDataSet;
	[SerializeField] static Dictionary<int , SoundClipData> soundClipDataSet;
	[SerializeField] List<SoundClipData> list;

	// field - refine data
	[SerializeField] static List<ItemData> searchItemList;
	[SerializeField] static List<FurnitureData> searchFurnitureList;

	// property
	public static bool PlayFirst { get { return playFirst; } }

	public bool PlayerDataLoading { get { return playerDataLoading; } }

	public static List<ItemData> SearchItemList { get { return searchItemList; } }

	public static List<FurnitureData> SearchFurnitureList { get { return searchFurnitureList; } }


	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		playerDataLoading = false;
		LoadFurnitureData();
		LoadItemData();
		LoadFieldData();
		LoadStageResultData();
		LoadStoreData();
		LoadSoundClipData();
		playerDataLoading = true;

	}

	// public method
	// furniture data load
	public static void LoadFurnitureData()
	{
		furnitureSet = new Dictionary<int, FurnitureData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/FurnitureDataTable" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "furniture/object" );

		if( nodes == null )
		{
			Debug.Log( "Data is null" );
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				// data create
				int type = int.Parse( node.SelectSingleNode( "type" ).InnerText );
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string name = node.SelectSingleNode( "name" ).InnerText;
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int height = int.Parse( node.SelectSingleNode( "height" ).InnerText );
				int widthX = int.Parse( node.SelectSingleNode( "widthX" ).InnerText );
				int widthZ = int.Parse( node.SelectSingleNode( "widthZ" ).InnerText );
				int level = int.Parse( node.SelectSingleNode( "level" ).InnerText );
				string file = node.SelectSingleNode( "file" ).InnerText;
				string material = node.SelectSingleNode( "materials" ).InnerText;
				int[] firstDataTable = null;
				int[] secondDataTable = null;
				int slotLength = 0;
				switch( type )
				{
					case 1:
						//type = FunctionType.CreateObject;
						if( material.Length == 1 )
						{
							firstDataTable = new int[1];
							firstDataTable[ 0 ] = Int32.Parse( material );
							secondDataTable = null;
						}
						else if( material.Length > 1 )
						{
							string[] temp = material.Split( '.' );
							firstDataTable = new int[temp.Length];
							for( int i = 0; i < temp.Length; i++ )
							{
								firstDataTable[ i ] = Int32.Parse( temp[ i ] ); 
							}
							secondDataTable = null;
						}
						break;
					case 2:
						//type = FunctionType.SellObject;
						if( material.Length == 1 )
						{
							firstDataTable = null;
							secondDataTable = null;
						}
						else if( material.Length > 1 )
						{
							string[] temp = material.Split( '.' );
							firstDataTable = new int[temp.Length / 2];
							secondDataTable = new int[temp.Length / 2];
							for( int i = 0; i < ( temp.Length - 1 ); i += 2 )
							{						
								firstDataTable[ ( int ) ( i / 2 ) ] = Int32.Parse( temp[ i ] );
								secondDataTable[ ( int ) ( i / 2 ) ] = Int32.Parse( temp[ i + 1 ] );
								slotLength = Int32.Parse( temp[ temp.Length - 1 ] );
							}
						}
						break;				
					case 4:
						//type = FunctionType.StorageObject;
						firstDataTable = new int[1];
						firstDataTable[ 0 ] = Int32.Parse( material );
						secondDataTable = null;
						break;					
				}

				// insert data
				try
				{					
					furnitureSet.Add( id, new FurnitureData( type, id, file, name, guide, price, height, widthX, widthZ, level, firstDataTable, secondDataTable, slotLength, FurnitureData.AllocateType.Field ) );
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}
		searchFurnitureList = new List<FurnitureData>( furnitureSet.Values );
	}

	// item data load
	public static void LoadItemData()
	{
		itemSet = new Dictionary<int, ItemData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/ItemData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "Item/Data" );

		if( nodes == null )
		{
			Debug.Log( "Data is null" );
		}
		else
		{
			foreach( XmlNode node in nodes )
			{				
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string file = node.SelectSingleNode( "file" ).InnerText;
				string name = node.SelectSingleNode( "name" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int countLimit = int.Parse( node.SelectSingleNode( "countLimit" ).InnerText );
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				int grade = int.Parse( node.SelectSingleNode( "grade" ).InnerText );
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );
				int type = int.Parse( node.SelectSingleNode( "type" ).InnerText );

				string material = node.SelectSingleNode( "material" ).InnerText;
				int[] resourceIDSet = null;
				int[] resourceCountSet = null;

				if( material.Length == 1 )
				{
					resourceIDSet = null;
					resourceCountSet = null;
				}
				else if( material.Length > 1 )
				{
					string[] temp = material.Split( '.' );
					resourceIDSet = new int[temp.Length / 2];
					resourceCountSet = new int[temp.Length / 2];
					for( int i = 0; i < temp.Length; i += 2 )
					{						
						resourceIDSet[ ( int ) ( i / 2 ) ] = Int32.Parse( temp[ i ] );
						resourceCountSet[ ( int ) ( i / 2 ) ] = Int32.Parse( temp[ i + 1 ] );
					}
				}

				try
				{
					if( material.Length == 1 )
					{
						itemSet.Add( id, new ItemData( type, id, file, name, price, countLimit, guide, grade, step ) );
					}
					else if( material.Length > 1 )
					{
						itemSet.Add( id, new ItemData( type, id, file, name, price, countLimit, guide, grade, step, ref resourceIDSet, ref resourceCountSet ) );
					}
					else
					{
						Debug.Log( "오류잡혔으니 확인해보세요!" );
					}
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}	

		searchItemList = new List<ItemData>( itemSet.Values );
	}

	// field data load
	public static void LoadFieldData()
	{
		fieldDataSet = new Dictionary<int, FieldData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/FieldData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "Field/Data" );

		if( nodes == null )
		{
			Debug.Log( "Data is null" );
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );
				int createTime = int.Parse( node.SelectSingleNode( "createTime" ).InnerText );
				int acquireExperience = int.Parse( node.SelectSingleNode( "acquireExperience" ).InnerText );
				int objectMaxCount = int.Parse( node.SelectSingleNode( "objectMaxCount" ).InnerText );

				try
				{
					fieldDataSet.Add( step, new FieldData( step, createTime, acquireExperience, objectMaxCount ) );
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}
	}

	// stage result data
	public static void LoadStageResultData()
	{
		tempDataSet = new List<StageResultData>( );
		stageResultSet = new Dictionary<int, List<StageResultData>>( );
		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/StageResultData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "ResultData/Data" );

		if( nodes == null )
		{
			Debug.Log( "Data is null : stage result data" );		
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );
				int rank = int.Parse( node.SelectSingleNode( "rank" ).InnerText );
				int rewardExp = int.Parse( node.SelectSingleNode( "rewardExp" ).InnerText );
				int rewardGold = int.Parse( node.SelectSingleNode( "rewardGold" ).InnerText );
				int rewardTouchCount = int.Parse( node.SelectSingleNode( "rewardTouchCount" ).InnerText );
				int rankProfitMoney = int.Parse( node.SelectSingleNode( "rankProfitMoney" ).InnerText );
				int rankProfitCount = int.Parse( node.SelectSingleNode( "rankProfitCount" ).InnerText );

				try
				{
					tempDataSet.Add( new StageResultData( step, rank, rewardExp, rewardGold, rewardTouchCount, rankProfitMoney, rankProfitCount ) );

					if( rank == 4 )
					{
						stageResultSet.Add( step, tempDataSet );
						tempDataSet = new List<StageResultData>( );
					}
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}		
		}
	}

	// store data
	public static void LoadStoreData()
	{
		storeDataSet = new Dictionary<int, StoreData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/StoreData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "StoreData/Data" );


		if( nodes == null )
		{
			Debug.Log( "Data is null : stage result data" );		
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );
				int requireExperiance = int.Parse( node.SelectSingleNode( "requireExperience" ).InnerText );
				float stageTime = float.Parse( node.SelectSingleNode( "stageTime" ).InnerText );
				try
				{
					storeDataSet.Add( step, new StoreData( step, requireExperiance, stageTime ) );
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}
	}

	// sound data
	public static void LoadSoundClipData()
	{
		soundClipDataSet = new Dictionary<int, SoundClipData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/SoundData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "SoundData/Data" );

		if( nodes == null )
		{
			Debug.Log( "Data is null : stage result data" );		
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string name = node.SelectSingleNode( "name" ).InnerText;
				string fileName = node.SelectSingleNode( "fileName" ).InnerText;
				int type = int.Parse( node.SelectSingleNode( "type" ).InnerText );

				try
				{
					soundClipDataSet.Add( id, new SoundClipData( id, name, fileName, type ) );						
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}
	}

	// player data load -> use player pref
	public static void LoadPlayerData()
	{
		// set default data
		playerData = new PlayerData( );
		try
		{			
			// data load - check first loading
			if( PlayerPrefs.GetInt( "FirstData" ) != 7891279 )
			{
				playerData.SetDefaultStatus();
				playFirst = true;
				return;
			}
			else
			{
				playFirst = false;
			}

			// data load - player direct data
			playerData.Name = PlayerPrefs.GetString( "PlayerName" );

			if( playerData.Name == "" )
			{
				playerData.SetDefaultStatus();
				playFirst = true;
				return;
			}

			playerData.CharacterType = PlayerPrefs.GetString( "PlayerCharacter" );
			playerData.Level = PlayerPrefs.GetInt( "PlayerLevel" );
			playerData.Fame = PlayerPrefs.GetInt( "PlayerFame" );
			playerData.Charm = PlayerPrefs.GetInt( "PlayerCharm" );
			playerData.Gold = PlayerPrefs.GetInt( "PlayerGold" );
			playerData.Gem = PlayerPrefs.GetInt( "PlayerGem" );



			// data load - data length
			playerData.ItemSet = new ItemInstance[PlayerPrefs.GetInt( "ItemSetLength" )];
			playerData.FurnitureSet = new FurnitureInstance[PlayerPrefs.GetInt( "FurnitureSetLength" )];

			// data load - store data
			playerData.StoreData = new StoreData( DataManager.FindStoreDataByStep( PlayerPrefs.GetInt( "StoreStep" ) ), PlayerPrefs.GetInt( "PresentExperience" ) );

			// data load - have item set data
			for( int i = 0; i < playerData.ItemSet.Length; i++ )
			{
				if( PlayerPrefs.GetInt( "HaveItemID" + i ) == 0 )
				{
					playerData.ItemSet[ i ] = new ItemInstance( );
				}
				else
				{
					playerData.ItemSet[ i ] = new ItemInstance( PlayerPrefs.GetInt( "HaveItemID" + i ), 
					                                            PlayerPrefs.GetInt( "HaveItemSlot" + i ), 
					                                            PlayerPrefs.GetInt( "HaveItemCount" + i ) );
				}
			}

			// data load - have furniture set data
			for( int i = 0; i < playerData.FurnitureSet.Length; i++ )
			{
				if( PlayerPrefs.GetInt( "HaveFurnitureID" + i ) == 0 )
				{
					playerData.FurnitureSet[ i ] = new FurnitureInstance( );
				}
				else
				{
					playerData.FurnitureSet[ i ] = new FurnitureInstance( PlayerPrefs.GetInt( "HaveFurnitureID" + i ),
					                                                      PlayerPrefs.GetInt( "HaveFurnitureSlot" + i ) );
				}
			}

			// data load - allocate furniture set data
			for( int i = 0; i < PlayerPrefs.GetInt( "AllocateFurnitureLength" ); i++ )
			{
				// set position
				Vector3 position = new Vector3( PlayerPrefs.GetFloat( "AllocateFurniturePositionX" + i ),
				                                PlayerPrefs.GetFloat( "AllocateFurniturePositionY" + i ),
				                                PlayerPrefs.GetFloat( "AllocateFurniturePositionZ" + i ) );
				// set rotation
				Quaternion rotation = new Quaternion( PlayerPrefs.GetFloat( "AllocateFurnitureRotationX" + i ),
				                                      PlayerPrefs.GetFloat( "AllocateFurnitureRotationY" + i ),
				                                      PlayerPrefs.GetFloat( "AllocateFurnitureRotationZ" + i ),
				                                      PlayerPrefs.GetFloat( "AllocateFurnitureRotationW" + i ) );
				// add allocated data
				playerData.AllocateFurnitureSet.Add( new FurnitureInstance( PlayerPrefs.GetInt( "AllocateFurnitureID" + i ), 
				                                                            PlayerPrefs.GetInt( "AllocateFurnitureSlot" + i ), 
				                                                            true, 
				                                                            position, 
				                                                            rotation ) );
			}

		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			playerData.SetDefaultStatus();
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			playerData.SetDefaultStatus();
		}
	}

	// player data save -> use player pref
	public static void SavePlayerData()
	{
		// first check data
		PlayerPrefs.SetInt( "FirstData", 7891279 );

		// save data setting - player direct data
		PlayerPrefs.SetString( "PlayerName", manager.GamePlayer.Name );
		PlayerPrefs.SetString( "PlayerCharacter", manager.GamePlayer.CharacterType );
		PlayerPrefs.SetInt( "PlayerLevel", manager.GamePlayer.Level );
		PlayerPrefs.SetInt( "PlayerFame", manager.GamePlayer.Fame );
		PlayerPrefs.SetInt( "PlayerCharm", manager.GamePlayer.Charm );
		PlayerPrefs.SetInt( "PlayerGold", manager.GamePlayer.Gold );
		PlayerPrefs.SetInt( "PlayerGem", manager.GamePlayer.Gem );

		// save data setting - data length
		PlayerPrefs.SetInt( "ItemSetLength", manager.GamePlayer.ItemSet.Length );
		PlayerPrefs.SetInt( "FurnitureSetLength", manager.GamePlayer.FurnitureSet.Length );
		PlayerPrefs.SetInt( "AllocateFurnitureLength", manager.GamePlayer.AllocateFurnitureSet.Count );

		// save data setting - store data
		PlayerPrefs.SetInt( "StoreStep", manager.GamePlayer.StoreData.StoreStep );
		PlayerPrefs.SetInt( "PresentExperience", manager.GamePlayer.StoreData.PresentExperience );

		// save data setting - have item set data
		for( int i = 0; i < manager.GamePlayer.ItemSet.Length; i++ )
		{
			try
			{
				PlayerPrefs.SetInt( "HaveItemID" + i, manager.GamePlayer.ItemSet[ i ].Item.ID );
				PlayerPrefs.SetInt( "HaveItemSlot" + i, manager.GamePlayer.ItemSet[ i ].SlotNumber );
				PlayerPrefs.SetInt( "HaveItemCount" + i, manager.GamePlayer.ItemSet[ i ].Count );
			}
			catch( NullReferenceException e )
			{				
				PlayerPrefs.SetInt( "HaveItemID" + i, 0 );
				PlayerPrefs.SetInt( "HaveItemSlot" + i, i );
				PlayerPrefs.SetInt( "HaveItemCount" + i, 0 );
			}
		}

		// save data setting - have furniture set data
		for( int i = 0; i < manager.GamePlayer.FurnitureSet.Length; i++ )
		{
			try
			{
				PlayerPrefs.SetInt( "HaveFurnitureID" + i, manager.GamePlayer.FurnitureSet[ i ].Furniture.ID );
				PlayerPrefs.SetInt( "HaveFurnitureSlot" + i, manager.GamePlayer.FurnitureSet[ i ].SlotNumber );
			}
			catch( NullReferenceException e )
			{
				PlayerPrefs.SetInt( "HaveFurnitureID" + i, 0 );
				PlayerPrefs.SetInt( "HaveFurnitureSlot" + i, i );
			}
		}

		// save data setting - allocate furniture set data
		for( int i = 0; i < manager.GamePlayer.AllocateFurnitureSet.Count; i++ )
		{
			PlayerPrefs.SetInt( "AllocateFurnitureID" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Furniture.ID );
			PlayerPrefs.SetInt( "AllocateFurnitureSlot" + i, i );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionX" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Position.x );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionY" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Position.y );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionZ" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Position.z );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationX" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Rotation.x );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationY" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Rotation.y );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationZ" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Rotation.z );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationW" + i, manager.GamePlayer.AllocateFurnitureSet[ i ].Rotation.w );
		}
	
		// save data
		PlayerPrefs.Save();
	}

	// item type setting
	public static ItemData.GradeType ReturnGradeType( int _type )
	{
		ItemData.GradeType _grade;

		switch( _type )
		{
			case 1:
				_grade = ItemData.GradeType.common;
				break;
			case 2:
				_grade = ItemData.GradeType.rare;
				break;
			case 3:
				_grade = ItemData.GradeType.unique;
				break;
			case 4:
				_grade = ItemData.GradeType.legendary;
				break;
			default:
				_grade = ItemData.GradeType.Default;
				break;
		}
		return _grade;
	}

	// find furniture
	public static FurnitureData FindFurnitureDataByID( int id )
	{
		try
		{
			return DataManager.furnitureSet[ id ];
		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadFurnitureData();
		}

		return DataManager.furnitureSet[ id ];
	}

	// find item
	public static ItemData FindItemDataByID( int id )
	{
		try
		{
			return DataManager.itemSet[ id ];
		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadItemData();
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadItemData();
		}

		return DataManager.itemSet[ id ];
	}

	// find field data
	public static FieldData FindFieldDataByStep( int step )
	{
		try
		{
			return DataManager.fieldDataSet[ step ];
		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadFieldData();
		}

		return DataManager.fieldDataSet[ step ];
	}

	// find result use profit
	public static StageResultData ReturnStageResultData( int step, int profitCount, int profitMoney )
	{
		List<StageResultData> tempSet = new List<StageResultData>( );
		try
		{
			tempSet = stageResultSet[ step ];
		}
		catch( NullReferenceException e )
		{
			LoadStageResultData();
		}

		for( int i = 0; i < tempSet.Count; i++ )
		{
			if( ( tempSet[ i ].RankProfitCount <= profitCount ) || ( tempSet[ i ].RankProfitMoney <= profitMoney ) )
				return tempSet[ i ];
		}

		return tempSet[ tempSet.Count - 1 ];
	}

	// find store data use step
	public static StoreData FindStoreDataByStep( int step )
	{
		try
		{
			return DataManager.storeDataSet[ step ];
		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadStoreData();
		}

		return DataManager.storeDataSet[ step ];
	}

	// find sound clip data use id & type
	public static SoundClipData FindSoundClipDataByID( int id )
	{
		try
		{
			return soundClipDataSet[ id ];
		}
		catch( KeyNotFoundException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadSoundClipData();
		}

		return DataManager.soundClipDataSet[ id ];

	}

	// check player data loading
	public static bool CheckPlayerDataLoading()
	{
		return DataManager.playerDataLoading;
	}

	public static PlayerData GetPlayerData()
	{
		return playerData;
	}

	public static List<FurnitureData> GetSearchFurnitureList()
	{
		try
		{
			return DataManager.searchFurnitureList;
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.Message );
			Debug.Log( e.StackTrace );
			DataManager.searchFurnitureList = new List<FurnitureData>( furnitureSet.Values );
		}

		return DataManager.searchFurnitureList;
	}

	public static List<ItemData> GetSearchItemList()
	{
		try
		{
			return DataManager.searchItemList;
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.Message );
			Debug.Log( e.StackTrace );
			DataManager.searchItemList = new List<ItemData>( itemSet.Values );
		}

		return DataManager.searchItemList;
	}
}
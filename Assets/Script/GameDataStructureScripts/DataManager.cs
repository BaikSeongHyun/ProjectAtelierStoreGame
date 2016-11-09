using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DataManager : MonoBehaviour
{
	// field - for check load
	[SerializeField] static bool playerDataLoading;

	// field - for data
	[SerializeField] static GameManager gameManagerAddress;
	[SerializeField] static PlayerData playerData;
	[SerializeField] static Dictionary<int, FurnitureData> furnitureSet;
	[SerializeField] static Dictionary<int, ItemData> itemSet;

	// property
	public bool PlayerDataLoading { get { return playerDataLoading; } }

	// unity method
	// awake
	void Awake()
	{
		playerDataLoading = false;
		gameManagerAddress = GetComponent<GameManager>();
		LoadFurnitureData();
		LoadItemData();
		LoadPlayerData();
	}

	void OnApplicationQuit()
	{
		SavePlayerData();
	}

	// public method

	// furniture data load
	public static void LoadFurnitureData()
	{
		furnitureSet = new Dictionary<int, FurnitureData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/furnitureData" );
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
				string material = null;

				if( type == 1 )
				{
					material = node.SelectSingleNode( "materials" ).InnerText;
				}

				// insert data
				try
				{
					if( type == 1 )
					{
						furnitureSet.Add( id, new FurnitureData( type, id, file, name, guide, height, widthX, widthZ, level, material, FurnitureData.AllocateType.Field ) );
					}
					else
					{
						furnitureSet.Add( id, new FurnitureData( type, id, file, name, guide, height, widthX, widthZ, level, FurnitureData.AllocateType.Field ) );
					}
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
			Debug.Log( "End load furniture data" );
		}
	}

	// item data load
	public static void LoadItemData()
	{
		itemSet = new Dictionary<int, ItemData>( );

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/itemData" );
		XmlDocument document = new XmlDocument( );
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "MaterialList/item" );

		if( nodes == null )
		{
			Debug.Log( "Data is null" );
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
				int type = int.Parse( node.SelectSingleNode( "type" ).InnerText );
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string file = node.SelectSingleNode( "file" ).InnerText;
				string name = node.SelectSingleNode( "name" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int countLimit = int.Parse( node.SelectSingleNode( "countLimit" ).InnerText );
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				int grade = int.Parse( node.SelectSingleNode( "grade" ).InnerText );
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );

				try
				{
					itemSet.Add( id, new ItemData( type, id, name, file, price, countLimit, guide, grade, step ) );
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}

		playerDataLoading = true;
	}

	// player data load -> use player pref
	public static void LoadPlayerData()
	{
		// set default data
		playerData = new PlayerData( );
		try
		{			
			// data load - player direct data
			playerData.Name = PlayerPrefs.GetString( "PlayerName" );
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
			playerData.StoreData = new StoreData( PlayerPrefs.GetInt( "StoreStep" ) );

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

		// link data
		gameManagerAddress.GamePlayer = playerData;
	}

	// player data save -> use player pref
	public static void SavePlayerData()
	{
		// save data setting - player direct data
		PlayerPrefs.SetString( "PlayerName", playerData.Name );
		PlayerPrefs.SetString( "PlayerCharacter", playerData.CharacterType );
		PlayerPrefs.SetInt( "PlayerLevel", playerData.Level );
		PlayerPrefs.SetInt( "PlayerFame", playerData.Fame );
		PlayerPrefs.SetInt( "PlayerCharm", playerData.Charm );
		PlayerPrefs.SetInt( "PlayerGold", playerData.Gold );
		PlayerPrefs.SetInt( "PlayerGem", playerData.Gem );

		// save data setting - data length
		PlayerPrefs.SetInt( "ItemSetLength", playerData.ItemSet.Length );
		PlayerPrefs.SetInt( "FurnitureSetLength", playerData.FurnitureSet.Length );
		PlayerPrefs.SetInt( "AllocateFurnitureLength", playerData.AllocateFurnitureSet.Count );

		// save data setting - store data
		PlayerPrefs.SetInt( "StoreStep", playerData.StoreData.StoreStep );

		// save data setting - have item set data
		for( int i = 0; i < playerData.ItemSet.Length; i++ )
		{
			try
			{
				PlayerPrefs.SetInt( "HaveItemID" + i, playerData.ItemSet[ i ].Item.ID );
				PlayerPrefs.SetInt( "HaveItemSlot" + i, playerData.ItemSet[ i ].SlotNumber );
				PlayerPrefs.SetInt( "HaveItemCount" + i, playerData.ItemSet[ i ].Count );
			}
			catch( NullReferenceException e )
			{				
				PlayerPrefs.SetInt( "HaveItemID" + i, 0 );
				PlayerPrefs.SetInt( "HaveItemSlot" + i, i );
				PlayerPrefs.SetInt( "HaveItemCount" + i, 0 );
			}
		}

		// save data setting - have furniture set data
		for( int i = 0; i < playerData.FurnitureSet.Length; i++ )
		{
			try
			{
				PlayerPrefs.SetInt( "HaveFurnitureID" + i, playerData.FurnitureSet[ i ].Furniture.ID );
				PlayerPrefs.SetInt( "HaveFurnitureSlot" + i, playerData.FurnitureSet[ i ].SlotNumber );
			}
			catch( NullReferenceException e )
			{
				PlayerPrefs.SetInt( "HaveFurnitureID" + i, 0 );
				PlayerPrefs.SetInt( "HaveFurnitureSlot" + i, i );
			}
		}

		// save data setting - allocate furniture set data
		for( int i = 0; i < playerData.AllocateFurnitureSet.Count; i++ )
		{
			PlayerPrefs.SetInt( "AllocateFurnitureID" + i, playerData.AllocateFurnitureSet[ i ].Furniture.ID );
			PlayerPrefs.SetInt( "AllocateFurnitureSlot" + i, playerData.AllocateFurnitureSet[ i ].SlotNumber );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionX" + i, playerData.AllocateFurnitureSet[ i ].Position.x );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionY" + i, playerData.AllocateFurnitureSet[ i ].Position.y );
			PlayerPrefs.SetFloat( "AllocateFurniturePositionZ" + i, playerData.AllocateFurnitureSet[ i ].Position.z );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationX" + i, playerData.AllocateFurnitureSet[ i ].Rotation.x );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationY" + i, playerData.AllocateFurnitureSet[ i ].Rotation.y );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationZ" + i, playerData.AllocateFurnitureSet[ i ].Rotation.z );
			PlayerPrefs.SetFloat( "AllocateFurnitureRotationW" + i, playerData.AllocateFurnitureSet[ i ].Rotation.w );
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
				_grade = _grade = ItemData.GradeType.Default;
				break;
		}
		return _grade;
	}

	// find furnirue
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

		return DataManager.itemSet[ id ];
	}

	// check player data loading
	public static bool CheckPlayerDataLoading()
	{
		return DataManager.playerDataLoading;
	}
}
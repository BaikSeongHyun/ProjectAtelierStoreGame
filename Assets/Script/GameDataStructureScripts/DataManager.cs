using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DataManager : MonoBehaviour
{
	// field
	[SerializeField] static Dictionary<int, FurnitureData> furnitureSet;
	[SerializeField] static Dictionary<int, ItemData> itemSet;
			
	// unity method
	// awake
	void Awake()
	{
		LoadFurnitureData();
		//LoadItemData();
	}
	
	// public method
	// furniture data load
	public static void LoadFurnitureData()
	{
		furnitureSet = new Dictionary<int, FurnitureData>();

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/furnitureData" );
		XmlDocument document = new XmlDocument();
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
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string name = node.SelectSingleNode( "name" ).InnerText;
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int height = int.Parse( node.SelectSingleNode( "height" ).InnerText );
				int widthX = int.Parse( node.SelectSingleNode( "widthX" ).InnerText );
				int widthZ = int.Parse( node.SelectSingleNode( "widthZ" ).InnerText );
				int level = int.Parse( node.SelectSingleNode( "level" ).InnerText );
				string note = node.SelectSingleNode( "note" ).InnerText;				
				
				// insert data
				try
				{					
					furnitureSet.Add( id, new FurnitureData( id, name, height, widthX, widthZ, level, note, note, FurnitureData.AllocateType.Field ) );
				}
				catch( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}

				Debug.Log( "Input data " + id );
			}
		}

		Debug.Log( "End load furniture data" );
	}
	
	// item data load
	public static void LoadItemData()
	{
		itemSet = new Dictionary<int, ItemData>();

		TextAsset loadData = Resources.Load<TextAsset>( "Data/itemData" );
		XmlDocument document = new XmlDocument();
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "ItemList/item" );

		if( nodes == null )
		{
			Debug.Log( "Data is null" );	
		}
		else
		{
			foreach( XmlNode node in nodes )
			{
                int uid = int.Parse(node.SelectSingleNode("UID").InnerText);
                string name = node.SelectSingleNode("Name").InnerText;
                int price = int.Parse(node.SelectSingleNode("Price").InnerText);
                int countLimit = int.Parse(node.SelectSingleNode("UID").InnerText);
                string guide = node.SelectSingleNode("Guide").InnerText;


                try
                {

                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                    Debug.Log(e.Message);
                }

                Debug.Log("Input data " + uid);
            }
		}

        Debug.Log("End load item data");
    }
	
	// find furnirue
	public static FurnitureData FindFurnitureDataByUID( int uID )
	{
		try
		{
			return DataManager.furnitureSet[ uID ];
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadFurnitureData();
		}		
		
		return DataManager.furnitureSet[ uID ];
		
	}
	
	// find item
	public static ItemData FindItemDataByUID( int uID )
	{
		try
		{
			return DataManager.itemSet[ uID ];
		}
		catch( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadItemData();
		}		
		
		return DataManager.itemSet[ uID ];
	}
	
}

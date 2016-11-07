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
		LoadItemData();
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

		if ( nodes == null )
		{
			Debug.Log( "Data is null" );	
		}
		else
		{
			foreach ( XmlNode node in nodes )
			{
                // data create
                int type = int.Parse(node.SelectSingleNode("type").InnerText);
				int id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				string name = node.SelectSingleNode( "name" ).InnerText;
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int height = int.Parse( node.SelectSingleNode( "height" ).InnerText );
				int widthX = int.Parse( node.SelectSingleNode( "widthX" ).InnerText );
				int widthZ = int.Parse( node.SelectSingleNode( "widthZ" ).InnerText );
				int level = int.Parse( node.SelectSingleNode( "level" ).InnerText );
<<<<<<< HEAD
				string note = node.SelectSingleNode( "note" ).InnerText;				
				
				// insert data
				try
				{					
					furnitureSet.Add( id, new FurnitureData( id, name, height, widthX, widthZ, level, note, note, FurnitureData.AllocateType.Field ) );
				}
				catch ( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
=======
				string file = node.SelectSingleNode( "file" ).InnerText;
                string material = null;

                if (type == 1)
                {
                    material = node.SelectSingleNode("materials").InnerText;
                }

                // insert data
                try
                {
                    if (type == 1)
                    {
                        furnitureSet.Add(id, new FurnitureData(type, id, name, guide, height, widthX, widthZ, level, file, material, FurnitureData.AllocateType.Field));
                    }
                    else
                    {
                        furnitureSet.Add(id, new FurnitureData(type, id, name, guide, height, widthX, widthZ, level, file, FurnitureData.AllocateType.Field));
                    }
                }

                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                    Debug.Log(e.Message);
                }

				Debug.Log( "Input data " + id );
>>>>>>> 5ca8c52fc7310d8ee4d4003d971885cde85f5fc7
			}
		}
		Debug.Log( "End load furniture data" );
	}
	
	// item data load
	public static void LoadItemData()
	{
		itemSet = new Dictionary<int, ItemData>();

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/itemData" );
		XmlDocument document = new XmlDocument();
		document.LoadXml( loadData.text );

		XmlNodeList nodes = document.SelectNodes( "ItemList/item" );

		if ( nodes == null )
		{
			Debug.Log( "Data is null" );	
		}
		else
		{
			foreach ( XmlNode node in nodes )
			{
				int uid = int.Parse( node.SelectSingleNode( "uid" ).InnerText );
				string name = node.SelectSingleNode( "name" ).InnerText;
				int price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				int countLimit = int.Parse( node.SelectSingleNode( "countLimit" ).InnerText );
				string guide = node.SelectSingleNode( "guide" ).InnerText;
				ItemData.GradeType grade = ReturnGradeType( int.Parse( node.SelectSingleNode( "grade" ).InnerText ) );
				int step = int.Parse( node.SelectSingleNode( "step" ).InnerText );

				try
				{
					itemSet.Add( uid, new ItemData( uid, name, price, countLimit, guide, grade, step ) );
				}
				catch ( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}
			}
		}
		Debug.Log( "End load item data" );
	}


	public static ItemData.GradeType ReturnGradeType( int _type )
	{
		ItemData.GradeType _grade;

		switch ( _type )
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
	public static FurnitureData FindFurnitureDataByUID( int uID )
	{
		try
		{
			return DataManager.furnitureSet[ uID ];
		}
		catch ( NullReferenceException e )
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
		catch ( NullReferenceException e )
		{
			Debug.Log( e.StackTrace );
			Debug.Log( e.Message );
			DataManager.LoadItemData();
		}		
		
		return DataManager.itemSet[ uID ];
	}
	
}

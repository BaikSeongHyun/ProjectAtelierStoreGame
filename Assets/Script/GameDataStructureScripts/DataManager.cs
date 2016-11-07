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

		if( nodes == null )
		{
			Debug.Log( "Data is null" );	
		}
		else
		{
			foreach( XmlNode node in nodes )
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
                        furnitureSet.Add(id, new FurnitureData(type, id, file, name, guide, height, widthX, widthZ, level, material, FurnitureData.AllocateType.Field));
                    }
                    else
                    {
                        furnitureSet.Add(id, new FurnitureData(type, id, file, name, guide, height, widthX, widthZ, level, FurnitureData.AllocateType.Field));
                    }
                }

                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                    Debug.Log(e.Message);
                }

				//Debug.Log( "Input data " + id );
			}
            Debug.Log("End load furniture data");
        }
	}
	
	// item data load
	public static void LoadItemData()
	{
		itemSet = new Dictionary<int, ItemData>();

		TextAsset loadData = Resources.Load<TextAsset>( "DataDocument/itemData" );
		XmlDocument document = new XmlDocument();
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
                int type = int.Parse(node.SelectSingleNode("type").InnerText);
                int id = int.Parse(node.SelectSingleNode("id").InnerText);
                string file = node.SelectSingleNode("file").InnerText;
                string name = node.SelectSingleNode("name").InnerText;
                int price = int.Parse(node.SelectSingleNode("price").InnerText);
                int countLimit = int.Parse(node.SelectSingleNode("countLimit").InnerText);
                string guide = node.SelectSingleNode("guide").InnerText;
                int grade = int.Parse(node.SelectSingleNode("grade").InnerText);
                int step = int.Parse(node.SelectSingleNode("step").InnerText);

                try
				{
                    itemSet.Add(id, new ItemData(type, id, name, file, price, countLimit, guide, grade, step));
                }
                catch ( Exception e )
				{
					Debug.Log( e.StackTrace );
					Debug.Log( e.Message );
				}

				//Debug.Log( "Input data " + id );
			}
            Debug.Log("End load item data");
        }
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

using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Collections;

public class InventoryDataLoader : MonoBehaviour
{

	public List<Furniture> furnitures;

	private int id;
	private string f_name;
	private string guide;
	private int price;
	private float height;
	private float widthX;
	private float widthZ;
	private int level;
	private string note;

	void Awake()
	{
		furnitures = new List<Furniture>();
	}

	void Start()
	{

		DataLoader();

	}

	void Update()
	{
	
	}

	void DataLoader()
	{
		TextAsset xml_load = Resources.Load<TextAsset>( "DataDocument/furnitureData" );
		XmlDocument doc = new XmlDocument();
		doc.LoadXml( xml_load.text );

		XmlNodeList nodes = doc.SelectNodes( "furniture/object" );

		if( nodes == null )
		{
			Debug.Log( "null : " + nodes.Count );
		}
		else
		{

			Debug.Log( "id : " + nodes[ 0 ].SelectSingleNode( "note" ).InnerText );
			foreach( XmlNode node in nodes )
			{
				Debug.Log( "---------" );
				Debug.Log( "count : " + nodes.Count );
				//Debug.Log("id : " + node.SelectSingleNode("id").InnerText
				//    + " / name : " + node.SelectSingleNode("name").InnerText
				//    + " / guide : " + node.SelectSingleNode("guide").InnerText
				//    + " / price : " + node.SelectSingleNode("price").InnerText
				//    + " / height : " + node.SelectSingleNode("height").InnerText
				//    + " / widhtX : " + node.SelectSingleNode("widhtX").InnerText
				//    + " / widhtZ : " + node.SelectSingleNode("widhtZ").InnerText
				//    + " / level : " + node.SelectSingleNode("level").InnerText
				//    + " / note : " + node.SelectSingleNode("note").InnerText
				//    );
				Debug.Log( "id : " + node.SelectSingleNode( "id" ).InnerText );

				id = int.Parse( node.SelectSingleNode( "id" ).InnerText );
				name = node.SelectSingleNode( "name" ).InnerText;
				guide = node.SelectSingleNode( "guide" ).InnerText;
				price = int.Parse( node.SelectSingleNode( "price" ).InnerText );
				height = float.Parse( node.SelectSingleNode( "height" ).InnerText );
				widthX = float.Parse( node.SelectSingleNode( "widthX" ).InnerText );
				widthZ = float.Parse( node.SelectSingleNode( "widthZ" ).InnerText );
				level = int.Parse( node.SelectSingleNode( "level" ).InnerText );
				note = node.SelectSingleNode( "note" ).InnerText;

				furnitures.Add( new Furniture(
						id,
						name,
						guide,
						price,
						height,
						widthX,
						widthZ,
						level,
						note
					) );

				//furnitures.Add(new Furniture());
			}
			Debug.Log( "result:" + furnitures.Count );
		}
	}
}

[System.Serializable]
public class Furniture
{
	[SerializeField] int id;
	public string f_name;
	public string guide;
	public int price;
	public float height;
	public float widthX;
	public float widthZ;
	public int level;
	public string note;

	// property
	public int ID { get { return id; } set { id = value; } }

	// constructor
	public Furniture()
	{
		id = -1;
		f_name = null;
		guide = null;
		price = -1;
		height = -1.0f;
		widthX = -1.0f;
		widthZ = -1.0f;
		level = -1;
		note = null;      
	}

	public Furniture(
		int _id,
		string _name,
		string _guide,
		int _price,
		float _height,
		float _widthX,
		float _widthZ,
		int _level,
		string _note )
	{
		id = _id;
		f_name = _name;
		guide = _guide;
		price = _price;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		level = _level - 1;
		note = _note;
	}

	public Furniture(
		int _id,
		string _name,
		string _guide,
		int _price,
		float _height,
		float _widthX,
		float _widthZ,
		int _level )
	{
		id = _id;
		f_name = _name;
		guide = _guide;
		price = _price;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		level = _level - 1;
	}

	public Furniture FurnitureGet { get { return new Furniture(
			id,
			f_name,
			guide,
			price,
			height,
			widthX,
			widthZ,
			level,
			note ); } }



	public string Names { get { return f_name; } set { f_name = value; } }
}

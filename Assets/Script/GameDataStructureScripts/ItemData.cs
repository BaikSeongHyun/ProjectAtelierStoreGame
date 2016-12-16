using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemData
{
	// field
	[SerializeField] ItemType itemType;
	[SerializeField] int id;
	[SerializeField] string file;
	[SerializeField] string name;
	[SerializeField] int price;
	[SerializeField] int countLimit;
	[SerializeField] string guide;
	[SerializeField] GradeType gradeType;
	[SerializeField] int step;
	[SerializeField] int[] resourceIDSet;
	[SerializeField] int[] resourceCountSet;

	// property
	public int ID { get { return id; } }

	public string Name { get { return name; } }

	public string File { get { return file; } }

	public int CountLimit { get { return countLimit; } }

	public string Guide { get { return guide; } }

	public int Step { get { return step; } }

	public ItemType Type { get { return itemType; } }

	public int Price { get { return price; } }

	public int[] ResourceIDSet { get { return resourceIDSet; } }

	public int[] ResourceCountSet { get { return resourceCountSet; } }

	// enum data

	public enum GradeType : int
	{
		Default = 0,
		common = 1,
		rare = 2,
		unique = 3,
		legendary = 4}

	;

	public enum ItemType : int
	{
		Default = 0,
		FoundationMaterial = 1,
		Potion = 2,
		MagicHerb = 3,
		MagicPowder = 4,
		Scroll = 5,
		Staff = 6,
		MagicBook = 7}

	;

	// constructor - default
	public ItemData()
	{
		itemType = ItemType.Default;
		id = 0;
		file = null;
		name = null;
		price = 0;
		countLimit = 0;
		guide = null;
		gradeType = GradeType.Default;
		step = 0;
	}

	// constructor - all parameter
	// use xml data format

	public ItemData( int _itemType, int _id, string _file, string _name, int _price, int _countLimit, string _guide, int _gradeType, int _step )
	{
		itemType = ReturnType( _itemType );
		id = _id;
		file = _file;
		name = _name;
		price = _price;
		countLimit = _countLimit;
		guide = _guide;
		gradeType = ReturnGradeType( _gradeType );
		step = _step;
	}

	public ItemData( int _itemType, int _id, string _file, string _name, int _price, int _countLimit, string _guide, int _gradeType, int _step, ref int[] _resourceIDSet, ref int[] _resourceCountSet )
	{
		itemType = ReturnType( _itemType );
		id = _id;
		file = _file;
		name = _name;
		price = _price;
		countLimit = _countLimit;
		guide = _guide;
		gradeType = ReturnGradeType( _gradeType );
		step = _step;
		resourceIDSet = _resourceIDSet;
		resourceCountSet = _resourceCountSet;
	}

	public ItemData( ItemData data )
	{
		itemType = data.itemType;
		id = data.id;
		file = data.file;
		name = data.name;
		price = data.price;
		countLimit = data.countLimit;
		guide = data.guide;
		gradeType = data.gradeType;
		step = data.step;

		if( resourceIDSet != null )
		{
			resourceIDSet = new int[data.resourceIDSet.Length];
			for( int i = 0; i < data.resourceIDSet.Length; i++ )
			{
				resourceIDSet[ i ] = data.resourceIDSet[ i ];
			}
		}

		if( resourceCountSet != null )
		{
			resourceCountSet = new int[data.resourceCountSet.Length];
			for( int i = 0; i < data.resourceCountSet.Length; i++ )
			{
				resourceCountSet[ i ] = data.resourceCountSet[ i ];
			}
		}
	}


	GradeType ReturnGradeType( int _gradeType )
	{
		GradeType grade;

		switch( _gradeType )
		{
			case 1:
				grade = GradeType.common;
				break;
			case 2:
				grade = GradeType.rare;
				break;
			case 3:
				grade = GradeType.unique;
				break;
			case 4:
				grade = GradeType.legendary;
				break;
			default:
				grade = GradeType.Default;
				break;
		}
		return grade;
	}

	public static ItemType ReturnType( int _itemType )
	{
		ItemType item = ItemType.Default;

		switch( _itemType )
		{
			case 1:
				item = ItemType.FoundationMaterial;
				break;
			case 2:
				item = ItemType.Potion;
				break;
			case 3:
				item = ItemType.MagicHerb;
				break;
			case 4:
				item = ItemType.MagicPowder;
				break;
			case 5:
				item = ItemType.Scroll;
				break;
			case 6:
				item = ItemType.Staff;
				break;
			case 7:
				item = ItemType.MagicBook;
				break;
		}
		return item;
	}

	public static string ReturnTypeString( ItemType type )
	{
		switch( type )
		{
			case ItemType.Potion:
				return "포션";
			case ItemType.MagicHerb:
				return "허브";
			case ItemType.MagicPowder:
				return "마법가루";
			case ItemType.Scroll:
				return "스크롤";
			case ItemType.Staff:
				return "스태프";
			case ItemType.MagicBook:
				return "마법책";		
		}

		return null;
	}
}
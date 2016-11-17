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

	public string File { get { return file; } }

	public ItemType Type { get { return itemType; } }

	public int Price { get { return price; } }

	public int CountLimit { get { return countLimit; } }

	public int[] ResourceIDSet { get { return resourceIDSet; } }

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
	public ItemData( int _itemType, int _id, string _file, string _name, int _price, int _countLimit, string _guide, int _gradeType, int _step, int[] _resourceIDSet, int[] _resourceCountSet )
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
				item = ItemData.ItemType.FoundationMaterial;
				break;
			case 2:
				item = ItemData.ItemType.Potion;
				break;
			case 3:
				item = ItemData.ItemType.MagicHerb;
				break;
			case 4:
				item = ItemData.ItemType.MagicPowder;
				break;
			case 5:
				item = ItemData.ItemType.Scroll;
				break;
			case 6:
				item = ItemData.ItemType.Staff;
				break;
			case 7:
				item = ItemData.ItemType.MagicBook;
				break;
		}
		return item;
	}
}
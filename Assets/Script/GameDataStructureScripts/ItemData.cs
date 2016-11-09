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

	// property
	public int ID { get { return id; } }

	public int CountLimit { get { return countLimit; } }

	// enum data

	public enum GradeType : int
	{
		Default = 0,
		common,
		rare,
		unique,
		legendary}

	;

	public enum ItemType : int
	{
		Default = 0,
		Potion = 1,
		MagicHerb = 2,
		MagicPowder = 3,
		Scroll = 4,
		Staff = 5,
		MagicBook = 6}

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

	// constructor - self parameter
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
	}

	GradeType ReturnGradeType( int _temp )
	{
		GradeType _grade;

		switch( _temp )
		{
			case 1:
				_grade = GradeType.common;
				break;
			case 2:
				_grade = GradeType.rare;
				break;
			case 3:
				_grade = GradeType.unique;
				break;
			case 4:
				_grade = GradeType.legendary;
				break;
			default:
				_grade = GradeType.Default;
				break;
		}
		return _grade;
	}

	public static ItemType ReturnType( int _temp )
	{
		ItemType _item = ItemType.Default;

		switch( _temp )
		{
			case 1:
				_item = ItemData.ItemType.Potion;
				break;
			case 2:
				_item = ItemData.ItemType.MagicHerb;
				break;
			case 3:
				_item = ItemData.ItemType.MagicPowder;
				break;
			case 4:
				_item = ItemData.ItemType.Scroll;
				break;
			case 5:
				_item = ItemData.ItemType.Staff;
				break;
			case 6:
				_item = ItemData.ItemType.MagicBook;
				break;
		}
		return _item;
	}
}
using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemData
{
	// field
	[SerializeField] int uid;
	[SerializeField] string name;
	[SerializeField] int price;
	[SerializeField] int countLimit;
	[SerializeField] string guide;
	[SerializeField] GradeType gradeType;
	[SerializeField] int step;
	[SerializeField] ItemType itemType;

	// property
	public int UID { get { return uid; } }

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
		//천천히 수정.....
		Default = 0,
		a,
		b,
		c}

	;

	// constructor - default
	public ItemData()
	{
		uid = 0;
		name = null;
		price = 0;
		countLimit = 0;
		guide = null;
		step = 0;
		gradeType = GradeType.Default;
	}

	// constructor - all parameter
	// use xml data format
	public ItemData( 
        int _uid, 
        string _name, 
        int _price, 
        int _countLimit, 
        string _guide,
        GradeType _gradeType, 
        int _step )
	{
		uid = _uid;
		name = _name;
		price = _price;
		countLimit = _countLimit;
		guide = _guide;
		step = _step;
        gradeType = _gradeType;

        switch ( _uid / 10000 )
		{
			case 1:
				itemType = ItemType.a;
				break;
			case 2:
				itemType = ItemType.b;
				break;
			case 3:
				itemType = ItemType.c;
				break;
			default:
				Debug.Log( "error" );
				break;
		}

	}

	// constructor - self parameter
	public ItemData( ItemData data )
	{
		uid = data.uid;
		name = data.name;
		price = data.price;
		countLimit = data.countLimit;
		guide = data.guide;
		gradeType = data.gradeType;
		step = data.step;
		itemType = data.itemType;
	}
}
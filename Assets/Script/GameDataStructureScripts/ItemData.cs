using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemData
{
	// field
<<<<<<< HEAD
	[SerializeField] int id;
=======
    [SerializeField] ItemType itemType;
	[SerializeField] int id;
    [SerializeField] string file;
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
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
		//천천히 수정.....
		Default = 0,
		a,
		b,
		c}

	;

	// constructor - default
	public ItemData()
	{
<<<<<<< HEAD
		id = 0;
=======
        itemType = ItemType.Default;
		id = 0;
        file = null;
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
		name = null;
		price = 0;
		countLimit = 0;
		guide = null;
		gradeType = GradeType.Default;
        step = 0;
    }

	// constructor - all parameter
	// use xml data format
	public ItemData( 
<<<<<<< HEAD
		int _id, 
		string _name, 
		int _price, 
		int _countLimit, 
		string _guide,
		GradeType _gradeType, 
		int _step )
	{
		id = _id;
=======
        int _itemType,
        int _id, 
        string _file,
        string _name, 
        int _price, 
        int _countLimit, 
        string _guide,
        int _gradeType, 
        int _step )
	{
        itemType = ReturnType(_itemType);
		id = _id;
        file = _file;
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
		name = _name;
		price = _price;
		countLimit = _countLimit;
		guide = _guide;
<<<<<<< HEAD
		step = _step;
		gradeType = _gradeType;

		switch( _id / 10000 )
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

=======
        gradeType = ReturnGradeType(_gradeType);
        step = _step;
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
	}

	// constructor - self parameter
	public ItemData( ItemData data )
	{
<<<<<<< HEAD
		id = data.id;
=======
        itemType = data.itemType;
		id = data.id;
        file = data.file;
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
		name = data.name;
		price = data.price;
		countLimit = data.countLimit;
		guide = data.guide;
		gradeType = data.gradeType;
        step = data.step;
    }

    GradeType ReturnGradeType(int _temp)
    {
        GradeType _grade;

        switch (_temp)
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

    ItemType ReturnType(int _temp)
    {
        ItemType _item;

        switch (_temp)
        {
            case 1:
                _item = ItemType.a;
                break;

            case 2:
                _item = ItemType.b;
                break;
            case 3:
                _item = ItemType.c;
                break;
            default:
                _item = ItemType.Default;
                break;
        }
        return _item;
    }
}
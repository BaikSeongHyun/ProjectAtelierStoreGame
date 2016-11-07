using System;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
	// field
	[SerializeField] int id;
	[SerializeField] string name;
    [SerializeField] string guide;
	[SerializeField] int height;
	[SerializeField] int widthX;
	[SerializeField] int widthZ;
	[SerializeField] int level;
	[SerializeField] string fileName;
    [SerializeField] string[] material;
	[SerializeField] FunctionType functionType;
    [SerializeField] AllocateType allocateType;

	// property
	public int ID { get { return id; } }

	public string Name { get { return name; } }

	public int Height { get { return height; } }

	public int WidthX { get { return widthX; } }

	public int WidthZ { get { return widthZ; } }

	public int Level { get { return level; } }

	public string FileName { get { return fileName; } }

	public FunctionType Function { get { return functionType; } }

	public AllocateType Allocate { get { return allocateType; } }

    // enum type
    // function type
    public enum FunctionType : int
    {
        Default = 0,
        CreateObject = 1,
        SellObject = 2,
        DecorateObject = 3,
        StorageObject = 4
    };

    // allocate type
    public enum AllocateType : int
    {
        Default = 0,
        Field = 1,
        Wall = 2,
        Wherever = 3
    };

    // constructor - no parameter -> set default;
    public FurnitureData()
	{
		id = 0;
		height = 0;
		widthX = 0;
		widthZ = 0;
		fileName = null;
		functionType = FunctionType.Default;
		allocateType = AllocateType.Default;
	}

    // constructor - all parameter -> set up data
	public FurnitureData(int _type, int _id, string _name, string _guide, int _height, int _widthX, int _widthZ, int _level, string _fileName, AllocateType _allocateType)
    {
        id = _id;
        name = _name;
        guide = _guide;
        height = _height;
        widthX = _widthX;
        widthZ = _widthZ;
        level = _level;
		fileName = _fileName;
        
        // allocate function type
        switch (_type)
        {
            case 1:
                functionType = FunctionType.CreateObject;
                break;
            case 2:
                functionType = FunctionType.SellObject;
                break;
            case 3:
                functionType = FunctionType.DecorateObject;
                break;
            case 4:
                functionType = FunctionType.StorageObject;
                break;
            default:
                Debug.Log("error");
                break;
        }

        allocateType = _allocateType;
    }

	public FurnitureData( int _type, int _id, string _name, string _guide, int _height, int _widthX, int _widthZ, int _level, string _fileName, string _mat, AllocateType _allocateType )
	{
		id = _id;
		name = _name;
        guide = _guide;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		level = _level;
		fileName = _fileName;
        MaterialList(_mat);

        // allocate function type
        switch ( _type )
		{
			case 1:
				functionType = FunctionType.CreateObject;
				break;
			case 2:
				functionType = FunctionType.SellObject;
				break;
			case 3:
				functionType = FunctionType.DecorateObject;
				break;
			case 4:
				functionType = FunctionType.StorageObject;
				break;
            default:
                Debug.Log("error");
                break;
        }

		allocateType = _allocateType;
	}

	// constructor - data parameter -> make same data instance
	public FurnitureData( FurnitureData data )
	{
		id = data.id;
		height = data.height;
		widthX = data.widthX;
		widthZ = data.widthZ;
		fileName = data.fileName;
		functionType = data.functionType;
		allocateType = data.allocateType;
	}

    public void MaterialList(string _mat)
    {
        material = _mat.Split(new char[] { ',' });
    }
}
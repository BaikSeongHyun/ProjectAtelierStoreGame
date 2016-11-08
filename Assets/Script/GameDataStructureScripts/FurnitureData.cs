using System;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
	// field
	[SerializeField] int id;
    [SerializeField] string file;
	[SerializeField] string name;
    [SerializeField] string guide;
	[SerializeField] int height;
	[SerializeField] int widthX;
	[SerializeField] int widthZ;
	[SerializeField] int level;
<<<<<<< HEAD
	[SerializeField] string fileName;
=======
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
    [SerializeField] string[] material;
	[SerializeField] FunctionType functionType;
    [SerializeField] AllocateType allocateType;

	// property
	public int ID { get { return id; } }

    public string File { get { return file; } }

    public string Name { get { return name; } }

    public string Guide { get { return guide; } }

	public int Height { get { return height; } }

	public int WidthX { get { return widthX; } }

	public int WidthZ { get { return widthZ; } }

	public int Level { get { return level; } }

<<<<<<< HEAD
	public string FileName { get { return fileName; } }

=======
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
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
        functionType = FunctionType.Default;
        id = 0;
        file = null;
        name = null;
        guide = null;
        height = 0;
		widthX = 0;
		widthZ = 0;
<<<<<<< HEAD
		fileName = null;
		functionType = FunctionType.Default;
=======
        level = 0;
        material = new string[0];
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
		allocateType = AllocateType.Default;
	}

    // constructor - all parameter -> set up data
<<<<<<< HEAD
	public FurnitureData(int _type, int _id, string _name, string _guide, int _height, int _widthX, int _widthZ, int _level, string _fileName, AllocateType _allocateType)
=======
    public FurnitureData(int _type, int _id, string _file, string _name, string _guide, int _height, int _widthX, int _widthZ, int _level, AllocateType _allocateType)
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
    {
        functionType = ReturnType(_type);
        id = _id;
        file = _file;
        name = _name;
        guide = _guide;
        height = _height;
        widthX = _widthX;
        widthZ = _widthZ;
        level = _level;
<<<<<<< HEAD
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
=======
        material = new string[1] { null };
        allocateType = _allocateType;
    }

    public FurnitureData( int _type, int _id, string _file, string _name, string _guide, int _height, int _widthX, int _widthZ, int _level, string _mat, AllocateType _allocateType )
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
	{
        functionType = ReturnType(_type);
        id = _id;
        file = _file;
        name = _name;
        guide = _guide;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		level = _level;
<<<<<<< HEAD
		fileName = _fileName;
=======
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
        MaterialList(_mat);
        allocateType = _allocateType;
	}

	// constructor - data parameter -> make same data instance
	public FurnitureData( FurnitureData data )
	{
        functionType = data.functionType;
        id = data.id;
        file = data.file;
        name = data.name;
        guide = data.guide;
		height = data.height;
		widthX = data.widthX;
		widthZ = data.widthZ;
<<<<<<< HEAD
		fileName = data.fileName;
		functionType = data.functionType;
=======
        level = data.level;
        Array.Copy(data.material, material, data.material.Length);
>>>>>>> 9d761361a6e019db4acc3e2f0304a5415c24cfcd
		allocateType = data.allocateType;
	}

    public void MaterialList(string _mat)
    {
        material = _mat.Split(new char[] { ',' });
    }

    FunctionType ReturnType(int _temp)
    {
        // allocate function type
        FunctionType type;

        switch (_temp)
        {
            case 1:
                type = FunctionType.CreateObject;
                break;
            case 2:
                type = FunctionType.SellObject;
                break;
            case 3:
                type = FunctionType.DecorateObject;
                break;
            case 4:
                type = FunctionType.StorageObject;
                break;
            default:
                type = FunctionType.Default;
                Debug.Log("error");
                break;
        }

        return type;
    }
}
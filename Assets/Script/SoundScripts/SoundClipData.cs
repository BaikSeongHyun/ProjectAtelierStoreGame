using UnityEngine;
using System.Collections;

public class SoundClipData
{
	// field - data
	[SerializeField] int id;
	[SerializeField] string name;
	[SerializeField] string fileName;
	[SerializeField] Type soundType;

	// property
	public string FileName { get { return fileName; } }

	public enum Type : int
	{
		Default = 0,
		Background = 1,
		UISound = 2,
		CharacterSound = 3}
	;

	// constructure -> all parameter
	public SoundClipData( int _id, string _name, string _fileName, int _type )
	{
		id = _id;
		name = _name;
		fileName = _fileName;
		soundType = SoundClipData.ReturnSoundType( _type );
	}


	// type check
	public static Type ReturnSoundType( int type )
	{
		switch( type )
		{
			case 1:
				return Type.Background;
			case 2:
				return Type.UISound;
			case 3: 
				return Type.CharacterSound;
		}

		return Type.Default;
	}
}


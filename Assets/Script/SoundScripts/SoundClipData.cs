using UnityEngine;
using System.Collections;

public class SoundClipData
{
	// field - data
	[SerializeField] int id;
	[SerializeField] string name;
	[SerializeField] string fileName;
	[SerializeField] Type soundType;

	public enum Type : int
	{
		Background = 1,
		UISound = 2,
		CharacterSound = 3}

	;
}


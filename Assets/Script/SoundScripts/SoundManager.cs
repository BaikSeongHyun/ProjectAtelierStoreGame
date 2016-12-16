using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] DataManager dataManager;

	// field data
	[SerializeField] int dataLength;

	// sound data structure
	[SerializeField] static Dictionary<string , AudioClip> backgroundSource;
	[SerializeField] static Dictionary<string , AudioClip> pointSoundSource;
		 
	// audio play
	[SerializeField] AudioSource backgroundPlayer;
	[SerializeField] AudioSource pointSoundPlayer;

	// property

	// unity method
	// awake
	void Awake()
	{
		LinkComponentElement();
		DataInitialize();
	}

	// update
	void Update()
	{

	}

	// public method
	// link element component
	public void LinkComponentElement()
	{
		// link audio player
		backgroundPlayer = GameObject.Find( "BackgroundPlayer" ).GetComponent<AudioSource>();
		pointSoundPlayer = GameObject.Find( "PointSoundPlayer" ).GetComponent<AudioSource>();
	}

	// data initialize
	public static void DataInitialize()
	{
		// background soruce

		// point sound soruce
	}

	public void PlayBackgroundAudio( string item )
	{
		try
		{
			backgroundPlayer.PlayOneShot( backgroundSource[ item ] );
		}
		catch( KeyNotFoundException e )
		{
			// load sound data
		}
		catch( NullReferenceException e )
		{
			// load sound data
		}
	}

	public void PlayPointSoundPlayer( string item )
	{
		try
		{
			pointSoundPlayer.PlayOneShot( pointSoundSource[ item ] );
		}
		catch( KeyNotFoundException e )
		{
			// load sound data
		}
		catch( NullReferenceException e )
		{
			// load sound data
		}
	}
}


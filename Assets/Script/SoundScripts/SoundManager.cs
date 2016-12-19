using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	// sound data structure
	[SerializeField] static Dictionary<int , AudioClip> backgroundSource;
	[SerializeField] static Dictionary<int , AudioClip> characterSoundSource;
	[SerializeField] static Dictionary<int , AudioClip> uiSoundSource;
		 
	[SerializeField] List <AudioClip> backCheck;

	// audio play
	[SerializeField] AudioSource backgroundPlayer;
	[SerializeField] AudioSource uiSoundPlayer;

	// property

	// unity method
	// awake
	void Awake()
	{
		LinkComponentElement();
		DataInitialize();
	}

	// public method
	// link element component
	public void LinkComponentElement()
	{
		// link audio player
		backgroundPlayer = GameObject.Find( "BackgroundPlayer" ).GetComponent<AudioSource>();
		uiSoundPlayer = GameObject.Find( "UISoundPlayer" ).GetComponent<AudioSource>();
	}

	// data initialize
	public static void DataInitialize()
	{
		// background source
		backgroundSource = new Dictionary<int, AudioClip>( );

		// ui sound source
		uiSoundSource = new Dictionary<int, AudioClip>( );

		// character sound source
		characterSoundSource = new Dictionary<int, AudioClip>( );
	}

	public void PlayBackgroundAudio( int id )
	{
		try
		{
			backgroundPlayer.clip = backgroundSource[ id ];
		}
		catch( KeyNotFoundException e )
		{
			// load sound data & play
			backgroundSource.Add( id, Resources.Load<AudioClip>( "SoundClip/" + DataManager.FindSoundClipDataByID( id ).FileName ) );
			backgroundPlayer.clip = backgroundSource[ id ];
		}
		catch( NullReferenceException e )
		{
			// load sound data & play
			backgroundSource.Add( id, Resources.Load<AudioClip>( "SoundClip/" + DataManager.FindSoundClipDataByID( id ).FileName ) );
			backgroundPlayer.clip = backgroundSource[ id ];
		}
		backgroundPlayer.Play();
	}

	public void PlayUISoundPlayer( int id )
	{
		try
		{
			uiSoundPlayer.PlayOneShot( uiSoundSource[ id ] );
		}
		catch( KeyNotFoundException e )
		{
			// load sound data & play
			uiSoundSource.Add( id, Resources.Load<AudioClip>( "SoundClip/" + DataManager.FindSoundClipDataByID( id ).FileName ) );
			uiSoundPlayer.PlayOneShot( uiSoundSource[ id ] );
		}
		catch( NullReferenceException e )
		{
			// load sound data & play
			uiSoundSource.Add( id, Resources.Load<AudioClip>( "SoundClip/" + DataManager.FindSoundClipDataByID( id ).FileName ) );
			uiSoundPlayer.PlayOneShot( uiSoundSource[ id ] );
		}
	}
}


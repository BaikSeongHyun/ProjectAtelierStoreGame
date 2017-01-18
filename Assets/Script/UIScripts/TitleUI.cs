using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TitleUI : MonoBehaviour, IPointerDownHandler
{
	// field - component element
	[SerializeField] Image clickImage;
	[SerializeField] Image eyeSpriteApply;

	// field - sprite : eyes
	[SerializeField] List<Sprite> eyesBlink;
	[SerializeField] List<Sprite> eyesWink;

	// field - data
	[SerializeField] float blinkEyesFrameTime;
	[SerializeField] float winkEyesFrameTime;

	// unity method
	void Awake()
	{
		LinkComponentElement();
		StartCoroutine( MainCharacterEyeControl() );
	}

	// public method
	// link component
	public void LinkComponentElement()
	{
		// component element 
		eyeSpriteApply = transform.Find( "EyeSpriteApply" ).GetComponent<Image>();

		// sprite data load
		eyesBlink = new List<Sprite>();
		eyesWink = new List<Sprite>();

		// : blink
		for( int i = 0; i < 5; i++ )
			eyesBlink.Add( Resources.Load<Sprite>( "Image/UI/Title/EyeBlink" + (i + 1) ) );
		
		for( int i = 0; i < 5; i++ )
			eyesWink.Add( Resources.Load<Sprite>( "Image/UI/Title/EyeWink" + (i + 1) ) );
			

		blinkEyesFrameTime = 0.01f;
		winkEyesFrameTime = 0.01f;
	}

	// title image click
	public void OnPointerDown( PointerEventData eventData )
	{
		SceneManager.LoadScene( "GameMainScene" );
	}

	// coroutineSection
	// main character eye control
	IEnumerator MainCharacterEyeControl()
	{
		while( true )
		{
			if( UnityEngine.Random.Range( 0, 5 ) == 4 )
			{
				// wink eyes
				for( int i = 0; i < (eyesWink.Count * 2) - 1; i++ )
				{
					if( i < eyesWink.Count )
						eyeSpriteApply.sprite = eyesWink[ i ];
					else
						eyeSpriteApply.sprite = eyesWink[ ((eyesBlink.Count * 2) - 2) - i ];

					yield return new WaitForSeconds( winkEyesFrameTime );
				}
			}
			else
			{
				// blink eyes
				for( int i = 0; i < (eyesBlink.Count * 2) - 1; i++ )
				{
					if( i < eyesBlink.Count )
						eyeSpriteApply.sprite = eyesBlink[ i ];
					else
						eyeSpriteApply.sprite = eyesBlink[ ((eyesBlink.Count * 2) - 2) - i ];

					yield return new WaitForSeconds( blinkEyesFrameTime );
				}
			}

			yield return new WaitForSeconds( 2f );
		}
	}

	// fly mr.kakao
	IEnumerator FlyKakao()
	{
		while( true )
		{
			
		}

		yield break;
	}

	// fly mrs.pinky
	IEnumerator FlyPinky()
	{
		while( true )
		{

		}

		yield break;
	}
}

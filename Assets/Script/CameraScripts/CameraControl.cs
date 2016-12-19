using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// main camera
	[SerializeField] Camera viewCamera;

	// field
	[SerializeField] float sensitive;
	Vector2?[] oldTouchPositions = { null, null };
	Vector2 oldTouchVector;
	float oldTouchDistance;


	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		viewCamera = GetComponent<Camera>();
		sensitive = 0.0015f;
	}

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		// set up position & rotation
		if( state != GameManager.GameMode.StoreCustomizing )
		{			
			switch( manager.GamePlayer.StoreData.StoreStep )
			{
				case 1:
					transform.position = new Vector3( 34f, 24f, 34f );
					break;
				case 2:
					transform.position = new Vector3( 36f, 25f, 36f );
					break;
				case 3:
					transform.position = new Vector3( 46f, 30f, 46f );
					break;
			}
			transform.localRotation = Quaternion.Euler( 30, 225, 0 );
		}
		else
		{
			switch( manager.GamePlayer.StoreData.StoreStep )
			{
				case 1:
					transform.position = new Vector3( 21f, 38f, 21f );
					break;
				case 2:
					transform.position = new Vector3( 28f, 48f, 28f );
					break;
				case 3:
					transform.position = new Vector3( 30f, 50f, 30f );
					break;
			}
			transform.localRotation = Quaternion.Euler( 60, 225, 0 );
		}
	}

	// camera position use update
	public void SetCameraPosition()
	{	
		if( Input.touchCount == 0 )
		{
			oldTouchPositions[ 0 ] = null;
			oldTouchPositions[ 1 ] = null;
		}
		else if( Input.touchCount == 1 )
		{
			if( oldTouchPositions[ 0 ] == null || oldTouchPositions[ 1 ] != null )
			{
				oldTouchPositions[ 0 ] = Input.GetTouch( 0 ).position;
				oldTouchPositions[ 1 ] = null;
			}
			else
			{
				Vector2 newTouchPosition = Input.GetTouch( 0 ).position;

				transform.position += transform.TransformDirection( ( Vector3 ) ( ( oldTouchPositions[ 0 ] - newTouchPosition ) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f ) );

				oldTouchPositions[ 0 ] = newTouchPosition;
			}
		}
		else
		{
			if( oldTouchPositions[ 1 ] == null )
			{
				oldTouchPositions[ 0 ] = Input.GetTouch( 0 ).position;
				oldTouchPositions[ 1 ] = Input.GetTouch( 1 ).position;
				oldTouchVector = ( Vector2 ) ( oldTouchPositions[ 0 ] - oldTouchPositions[ 1 ] );
				oldTouchDistance = oldTouchVector.magnitude;
			}
			else
			{
				Touch touchZero = Input.GetTouch( 0 );
				Touch touchOne = Input.GetTouch( 1 );

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				float prevTouchDeltaMag = ( touchZeroPrevPos - touchOnePrevPos ).magnitude;
				float touchDeltaMag = ( touchZero.position - touchOne.position ).magnitude;

				float deltaMagnitureDiff = prevTouchDeltaMag - touchDeltaMag;

				transform.position -= ( transform.position * deltaMagnitureDiff * -sensitive );
			}
		}

		if( manager.PresentMode == GameManager.GameMode.StoreCustomizing )
		{
			transform.position = new Vector3( Mathf.Clamp( transform.position.x, 15f, 30f ), Mathf.Clamp( transform.position.y, 30f, 50f ), Mathf.Clamp( transform.position.z, 15f, 30f ) );
			transform.rotation = Quaternion.Euler( 60, 225, 0 );
		}
		else
		{
			transform.position = new Vector3( Mathf.Clamp( transform.position.x, 30f, 55f ), Mathf.Clamp( transform.position.y, 25f, 40f ), Mathf.Clamp( transform.position.z, 30f, 55f ) );
			transform.rotation = Quaternion.Euler( 30, 225, 0 );
		}
	}
}

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// main camera
	[SerializeField] Camera viewCamera;

	// field

	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;


	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		viewCamera = GetComponent<Camera>();
	}

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		// set up position & rotation
		if( state != GameManager.GameMode.StoreCustomizing )
		{
			transform.position = new Vector3( 20f, 16f, 20f );
			transform.localRotation = Quaternion.Euler( 40, 225, 0 );
		}
		else
		{
			transform.position = new Vector3( 7.5f, 30f, 7.5f );
			transform.localRotation = Quaternion.Euler( 90, 225, 0 );
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
				Vector2 screen = new Vector2( GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight );

				Vector2[] newTouchPositions = {
					Input.GetTouch( 0 ).position,
					Input.GetTouch( 1 ).position
				};
				Vector2 newTouchVector = newTouchPositions[ 0 ] - newTouchPositions[ 1 ];
				float newTouchDistance = newTouchVector.magnitude;

				transform.position += transform.TransformDirection( ( Vector3 ) ( ( oldTouchPositions[ 0 ] + oldTouchPositions[ 1 ] - screen ) * GetComponent<Camera>().orthographicSize / screen.y ) );
				transform.localRotation *= Quaternion.Euler( new Vector3( 0, 0, Mathf.Asin( Mathf.Clamp( ( oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y ) / oldTouchDistance / newTouchDistance, -1f, 1f ) ) / 0.0174532924f ) );
				GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
				transform.position -= transform.TransformDirection( ( newTouchPositions[ 0 ] + newTouchPositions[ 1 ] - screen ) * GetComponent<Camera>().orthographicSize / screen.y );

				oldTouchPositions[ 0 ] = newTouchPositions[ 0 ];
				oldTouchPositions[ 1 ] = newTouchPositions[ 1 ];
				oldTouchVector = newTouchVector;
				oldTouchDistance = newTouchDistance;
			}
		}
	}
}

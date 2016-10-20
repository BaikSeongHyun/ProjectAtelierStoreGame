using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// control field
	Quaternion rootStoreOrthographicRotation = Quaternion.Euler( new Vector3( 30f, 225f, 0f ) );
	Vector3 rootStoreOrthographicPosition = new Vector3( 16, 10, 16 );
	Vector3 fieldOrthographicDistance = new Vector3( 16f, 13.5f, 16f );
	const float rootOrthographicViewSize = 6f;
	[SerializeField] float orthographicViewSize = 6f;
	[SerializeField] float viewSensitive = 10f;
	[SerializeField] float moveSensitive = 10f;

	// control camera
	[SerializeField] Camera mainCamera;

	// property
	private float ViewSize { get { return orthographicViewSize; } set { orthographicViewSize = Mathf.Clamp( value, 4f, 12f ); } }

	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		mainCamera = GetComponent<Camera>(); 
		mainCamera.orthographic = true;
		mainCamera.orthographicSize = rootOrthographicViewSize;
	}

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		switch( state )
		{
			case GameManager.GameMode.Store:
				mainCamera.orthographic = true;
				mainCamera.orthographicSize = rootOrthographicViewSize;
				mainCamera.transform.position = rootStoreOrthographicPosition;
				mainCamera.transform.rotation = rootStoreOrthographicRotation;
				break;
			case GameManager.GameMode.Field:
				mainCamera.orthographic = true;
				mainCamera.orthographicSize = rootOrthographicViewSize;
				mainCamera.transform.position = fieldOrthographicDistance;
				mainCamera.transform.rotation = rootStoreOrthographicRotation;
				break;
		}
		Debug.Log( mainCamera.transform.rotation );
	}

	// camera position use update
	public void SetCameraPosition()
	{
		// set view size
		if( Input.GetAxis( "Mouse ScrollWheel" ) > 0 )
			ViewSize += Time.deltaTime * viewSensitive;
		else if( Input.GetAxis( "Mouse ScrollWheel" ) < 0 )
			ViewSize -= Time.deltaTime * viewSensitive;
		
		mainCamera.orthographicSize = ViewSize;

		// set camera view position
		if( manager.PresentMode != GameManager.GameMode.Field )
		{
			if( Input.GetKey( KeyCode.UpArrow ) )
			{
				mainCamera.transform.position += new Vector3( 0, Time.deltaTime * moveSensitive, 0 );
			}
			else if( Input.GetKey( KeyCode.DownArrow ) )
			{
				mainCamera.transform.position += new Vector3( 0, -Time.deltaTime * moveSensitive, 0 );
			}
			else if( Input.GetKey( KeyCode.RightArrow ) )
			{
				mainCamera.transform.position += new Vector3( -Time.deltaTime * moveSensitive, 0, Time.deltaTime * moveSensitive );
			}
			else if( Input.GetKey( KeyCode.LeftArrow ) )
			{
				mainCamera.transform.position += new Vector3( Time.deltaTime * moveSensitive, 0, -Time.deltaTime * moveSensitive );
			}
		}
		else if( manager.PresentMode == GameManager.GameMode.Field )
		{
			mainCamera.transform.position = manager.PlayerCharacter.transform.position + fieldOrthographicDistance;
		}

	}
}

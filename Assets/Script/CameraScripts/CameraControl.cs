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
	[SerializeField] Transform cusRot;
	[SerializeField] Transform norRot;

	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		viewCamera = GetComponent<Camera>();
		sensitive = 0.001f;
	}

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		// set up position & rotation
		if( state != GameManager.GameMode.StoreCustomizing )
		{
			transform.position = new Vector3( 20f, 16f, 20f );
			transform.localRotation = norRot.rotation;
		}
		else
		{
			transform.position = new Vector3( 7.5f, 30f, 7.5f );
			transform.localRotation = cusRot.rotation;
		}
	}

	// camera position use update
	public void SetCameraPosition()
	{	
		int count = Input.touchCount;
		// one touch
		if( count == 2 )
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

		if( manager.PresentMode != GameManager.GameMode.StoreCustomizing )
		{
			transform.position = new Vector3( Mathf.Clamp( transform.position.x, 10f, 50f ),
			                                  Mathf.Clamp( viewCamera.transform.position.y, 8f, 40f ),
			                                  Mathf.Clamp( viewCamera.transform.position.z, 10f, 50f ) );
		}
		else
		{
			transform.position = new Vector3( Mathf.Clamp( transform.position.x, 7.5f, 7.5f ),
			                                  Mathf.Clamp( transform.position.y, 7.5f, 50f ),
			                                  Mathf.Clamp( transform.position.z, 7.5f, 7.5f ) );
		}
	}
}

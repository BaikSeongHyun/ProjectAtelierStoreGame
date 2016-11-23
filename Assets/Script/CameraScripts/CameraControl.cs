using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// main camera
	[SerializeField] Camera viewCamera;

	// field position
	[SerializeField] float xPosition = 10f;
	[SerializeField] float yPosition = 10f;
	[SerializeField] float zPosition = 10f;

	// field - rotation
	[SerializeField] float xRotation;
	[SerializeField] float yRotation;

	[SerializeField] Transform charPos;

	// property

	public float XPosition { get { return xPosition; } set { xPosition = Mathf.Clamp( value, -30f, 30f ); } }

	public float YPosition { get { return yPosition; } set { yPosition = Mathf.Clamp( value, 0f, 100f ); } }

	public float ZPosition { get { return zPosition; } set { zPosition = Mathf.Clamp( value, -30f, 30f ); } }

	public float XRotation { get { return xRotation; } set { xRotation = Mathf.Clamp( value, 30f, 90f ); } }

	public float YRotation { get { return yRotation; } set { yRotation = Mathf.Clamp( value, 180f, 270f ); } }

	// unity method
	// awake
	void Awake()
	{
		viewCamera = GetComponent<Camera>();
	}

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		
	}

	// camera position use update
	public void SetCameraPosition()
	{	

		int count = Input.touchCount;
		// one touch
		if ( count == 1 )
		{
			if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved )
			{			
				XPosition += Input.GetTouch( 0 ).deltaPosition.x * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
				ZPosition -= Input.GetTouch( 0 ).deltaPosition.x * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;

				XPosition += Input.GetTouch( 0 ).deltaPosition.y * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
				ZPosition += Input.GetTouch( 0 ).deltaPosition.y * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
			}
		}	
		// two touch
		else if ( count == 2 )
		{
			// check y position
			if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 )
				YPosition = transform.position.y + Time.deltaTime * 10f;
			else if ( Input.GetAxis( "Mouse ScrollWheel" ) > 0 )
				YPosition = transform.position.y - Time.deltaTime * 10f;
		}
		viewCamera.transform.position = new Vector3( xPosition, yPosition, zPosition );
	}

	public void MoveObject()
	{
		//오류떠서 주석칩니다.
		//viewCamera.transform.position = charPos.position + new Vector3( xPosition, yPosition, zPosition );
	}
}

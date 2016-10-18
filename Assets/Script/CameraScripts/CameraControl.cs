using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// control field
	[SerializeField] Vector3 position;
	[SerializeField] float viewSize;

	// control camera
	[SerializeField] Camera mainCamera;

	// unity method
	// awake
	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		mainCamera = GetComponent<Camera>(); 
		mainCamera.orthographic = true;
	}

	// camera set method
	public void SetCameraPosition()
	{
		switch( manager.PresentMode )
		{
			case GameManager.GameMode.Store:
				mainCamera.transform.position = new Vector3( -3f, 7f, -3f );
				mainCamera.transform.rotation = Quaternion.Euler( new Vector3( 30f, 45f, 0f ) );
				break;
		}
	}
}

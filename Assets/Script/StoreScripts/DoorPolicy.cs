using UnityEngine;
using System.Collections;

public class DoorPolicy : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// field - component element
	[SerializeField] GameObject rightDoor;
	[SerializeField] GameObject leftDoor;

	// unity method
	// first activate
	void Awake()
	{
		LinkComponentElement();
	}

	// Update is called once per frame
	void Update()
	{
		// open store -> open the door
		if( manager.PresentMode == GameManager.GameMode.StoreOpen )
			DoorOpen();
			// close the door
		else
			DoorClose();
				
	}


	// public method
	// iink data & element
	public void LinkComponentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// component element
		if( this.gameObject.name == "Step1DoorFrame" )
			rightDoor = transform.Find( "Door" ).gameObject;
		else
		{
			rightDoor = transform.Find( "RightDoor" ).gameObject;
			leftDoor = transform.Find( "LeftDoor" ).gameObject;
		}
	}

	// open the door
	public void DoorOpen()
	{
		if( this.gameObject.name == "Step1DoorFrame" )
			rightDoor.transform.localRotation = Quaternion.Lerp( rightDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, 180f ), Time.deltaTime );
		else
		{
			rightDoor.transform.localRotation = Quaternion.Lerp( rightDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, 180f ), Time.deltaTime );
			leftDoor.transform.localRotation = Quaternion.Lerp( leftDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, -180f ), Time.deltaTime );
		}
	}

	// close the door
	public void DoorClose()
	{
		if( this.gameObject.name == "Step1DoorFrame" )
			rightDoor.transform.localRotation = Quaternion.Lerp( rightDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, 0.01f ), Time.deltaTime );
		else
		{
			rightDoor.transform.localRotation = Quaternion.Lerp( rightDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, 0.01f ), Time.deltaTime );
			leftDoor.transform.localRotation = Quaternion.Lerp( leftDoor.transform.localRotation, Quaternion.Euler( 0f, 0f, -0.01f ), Time.deltaTime );
		}
	}
}


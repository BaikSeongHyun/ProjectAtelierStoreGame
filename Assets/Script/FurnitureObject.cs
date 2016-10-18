using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class FurnitureObject : MonoBehaviour
{
	// field
	[SerializeField] FurnitureData data;
	[SerializeField] bool allocateMode;
	[SerializeField] bool allocatePossible;
	[SerializeField] MeshRenderer objectRenderer;
	[SerializeField] Collider objectCollider;
	[SerializeField] Collider[] tempSet;
	[SerializeField] Vector3 planeScale;

	// property
	public FurnitureData ObjectData { get { return data; } set { data = value; } }

	public bool AllocateMode { get { return allocateMode; } set { allocateMode = value; } }

	public bool AllocatePossible { get { return allocatePossible; } set { allocatePossible = value; } }

	// unity standard method
	// awake -> set element
	void Awake()
	{
		LinkComponentElement();

	}

	// public method
	// Link element & initialize data
	public void LinkComponentElement()
	{
		allocateMode = false;
		objectRenderer = GetComponent<MeshRenderer>();
		objectCollider = GetComponent<Collider>();
	}

	// change object renderer state
	public void ChangeObjectState( bool state )
	{
		objectRenderer.enabled = state;
	}

	// change object position
	public void ChangeObjectPosition( Vector3 point, GameObject plane )
	{
		Vector3 position = SetPointElement( point, plane );
		this.gameObject.transform.position = position;
		data.Position = position;
	}

	// change object rotation
	public void ChangeObjectRotation( string direction )
	{
		switch( direction )
		{
			case "Right":
				transform.Rotate( 0, 90, 0 );
				break;
			case "Left":
				transform.Rotate( 0, -90, 0 );
				break;
			case "Reset":
				transform.rotation = new Quaternion( 0f, 0f, 0f, 0f );
				break;	
			case "WallLeft":
				transform.rotation = new Quaternion( 0f, -0.7f, 0f, 0.7f );
				break;
			case "WallRight":
				transform.rotation = new Quaternion( 0f, 0f, 0f, 0f );
				break;
		}
	}

	// check allocate possible
	public bool CheckAllocatePossible()
	{
		tempSet = Physics.OverlapBox( transform.position, new Vector3( data.WidthX, data.Height, data.WidthZ ), transform.rotation, 1 << LayerMask.NameToLayer( "Furniture" ) );

		if( tempSet.Length == 1 )
		{
			allocatePossible = true;
			return true;
		}
		else
		{
			allocatePossible = false;
			return false;
		}
	}

	// set point element
	public Vector3 SetPointElement( Vector3 point, GameObject plane )
	{
		planeScale = plane.transform.lossyScale * 10f;

		// field initialize
		float resultX, resultY, resultZ;

		// point set - type cast & 0.5 set
		if( ( point.x - ( int ) point.x ) > 0.5f )
			resultX = ( int ) point.x + 0.5f;
		else
			resultX = ( int ) point.x;

		if( ( point.y - ( int ) point.y ) > 0.5f )
			resultY = ( int ) point.y + 0.5f;
		else
			resultY = ( int ) point.y;

		if( ( point.z - ( int ) point.z ) > 0.5f )
			resultZ = ( int ) point.z + 0.5f;
		else
			resultZ = ( int ) point.z;

		// check plane out - type field
		if( data.Allocate == FurnitureData.AllocateType.Field )
		{
			// set x axis
			// larger than plane axis x
			if( ( resultX + ( ( float ) data.WidthX ) / 2f ) > ( plane.transform.position.x + planeScale.x / 2f ) )
			{
				resultX = ( plane.transform.position.x + planeScale.x / 2f ) - ( ( float ) data.WidthX ) / 2f;
			}
			// less than plane axis x
			else if( ( resultX - ( ( float ) data.WidthX ) / 2f ) < ( plane.transform.position.x - planeScale.x / 2f ) )
			{
				resultX = ( plane.transform.position.x - planeScale.x / 2f ) + ( ( float ) data.WidthX ) / 2f;
			}

			// set z axis
			// larger than planme axis z
			if( ( resultZ + ( ( float ) data.WidthZ ) / 2f ) > ( plane.transform.position.z + planeScale.z / 2f ) )
			{
				resultZ = ( plane.transform.position.z + planeScale.z / 2f ) - ( ( float ) data.WidthZ ) / 2f;
			}
			// less than plane axis z
			else if( ( resultZ - ( ( float ) data.WidthZ ) / 2f ) < ( plane.transform.position.z - planeScale.z / 2f ) )
			{
				resultZ = ( plane.transform.position.z - planeScale.z / 2f ) + ( ( float ) data.WidthZ ) / 2f;
			}
		}
		// check plane out - type wall
		else if( data.Allocate == FurnitureData.AllocateType.Wall )
		{
			// wall left
			if( plane.layer == LayerMask.NameToLayer( "StoreWallLeft" ) )
			{
				// set y axis
				// larger than plane axis y
				if( ( resultY + ( ( float ) data.Height ) / 2f ) > ( plane.transform.position.y + planeScale.y / 2f ) )
				{					
					resultY = ( plane.transform.position.y + planeScale.y / 2f ) - ( ( float ) data.Height ) / 2f;
				}
				// less than plane axis y
				else if( ( resultY - ( ( float ) data.Height ) / 2f ) < ( plane.transform.position.y - planeScale.y / 2f ) )
				{
					resultY = ( plane.transform.position.y - planeScale.y / 2f );
				}

				// set z axis
				// larger than planme axis z
				if( ( resultZ + ( ( float ) data.WidthX ) / 2f ) > ( plane.transform.position.z + planeScale.z / 2f ) )
				{
					resultZ = ( plane.transform.position.z + planeScale.z / 2f ) - ( ( float ) data.WidthX ) / 2f;
				}
				// less than plane axis z
				else if( ( resultZ - ( ( float ) data.WidthX ) / 2f ) < ( plane.transform.position.z - planeScale.z / 2f ) )
				{
					resultZ = ( plane.transform.position.z - planeScale.z / 2f ) + ( ( float ) data.WidthX ) / 2f;
				}
			}
			// wall right
			else if( plane.layer == LayerMask.NameToLayer( "StoreWallRight" ) )
			{
				// set x axis
				// larger than plane axis x
				if( ( resultX + ( ( float ) data.WidthX ) / 2f ) > ( plane.transform.position.x + planeScale.x / 2f ) )
				{
					Debug.Log( "1" );
					resultX = ( plane.transform.position.x + planeScale.x / 2f ) - ( ( float ) data.WidthX ) / 2f;
				}
				// less than plane axis x
				else if( ( resultX - ( ( float ) data.WidthX ) / 2f ) < ( plane.transform.position.x - planeScale.x / 2f ) )
				{
					Debug.Log( "2" );
					resultX = ( plane.transform.position.x - planeScale.x / 2f ) + ( ( float ) data.WidthX ) / 2f;
				}

				// set y axis
				// larger than plane axis y
				if( ( resultY + ( ( float ) data.Height ) / 2f ) > ( plane.transform.position.y + planeScale.y / 2f ) )
				{					
					resultY = ( plane.transform.position.y + planeScale.y / 2f ) - ( ( float ) data.Height ) / 2f;
				}
				// less than plane axis y
				else if( ( resultY - ( ( float ) data.Height ) / 2f ) < ( plane.transform.position.y - planeScale.y / 2f ) )
				{
					resultY = ( plane.transform.position.y - planeScale.y / 2f );
				}
			}
		}

		// check overlap furniture
		CheckAllocatePossible();

		// return data
		return new Vector3( resultX, resultY, resultZ );
	}

}

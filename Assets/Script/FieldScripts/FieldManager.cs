using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FieldManager : MonoBehaviour
{
	// high structure
	GameManager manager;

	// logic data field
	GameObject target;
	//clicked object
	int layerMask;

	public PresentState state;

	public GameObject[] objects;

	// field - field object location
	private GameObject[] step1Position;
	private GameObject[] step2Position;
	private GameObject[] step3Position;
	public GameObject[] stepPosition;

	public bool[] stepLocation;

	public int temp;
	public Vector3 position;

	public int currentObjectNumber;
	private int maxObject;
	private bool objectLoad = false;

	public float updateTime;

	// temporary storage of prefab data
	GameObject destroyBody;
	ObjectOnField ClickItemData;

	// field - present step field data
	[SerializeField] FieldData presentFieldData;
	[SerializeField] List<GameObject> fieldObjectSet;

	// enum - present step
	public enum PresentState : int
	{
		Default = 0,
		Step1,
		Step2,
		Step3}

	;


	void Awake()
	{
		DataInitialize();
		layerMask = 1 << LayerMask.NameToLayer( "Cloud" );

	}

	public void DataInitialize()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// data pull
		CheckStepFieldData();

		objects = new GameObject[4];
		objects[ 0 ] = Resources.Load( "FieldObject/bushPurple" ) as GameObject;
		objects[ 1 ] = Resources.Load( "FieldObject/bushYellow" ) as GameObject;
		objects[ 2 ] = Resources.Load( "FieldObject/StoneBlue" ) as GameObject;
		objects[ 3 ] = Resources.Load( "FieldObject/StoneRed" ) as GameObject;

		step1Position = GameObject.FindGameObjectsWithTag( "Step1Position" );
		step2Position = GameObject.FindGameObjectsWithTag( "Step2Position" );
		step3Position = GameObject.FindGameObjectsWithTag( "Step3Position" );

		stepPosition = new GameObject[step1Position.Length + step2Position.Length + step3Position.Length];
		Array.Copy( step1Position, 0, stepPosition, 0, step1Position.Length );
		Array.Copy( step2Position, 0, stepPosition, step1Position.Length, step2Position.Length );
		Array.Copy( step3Position, 0, stepPosition, step1Position.Length + step2Position.Length, step3Position.Length );

		stepLocation = new bool[stepPosition.Length];

		position = new Vector3( );

		if( state == PresentState.Step1 )
		{
			maxObject = step1Position.Length;
		}
		else if( state == PresentState.Step2 )
		{
			maxObject = step1Position.Length + step2Position.Length;
		}
		else if( state == PresentState.Step3 )
		{
			maxObject = step1Position.Length + step2Position.Length + step3Position.Length;
		}

		fieldObjectSet = new List<GameObject>( );
	}

	public void CheckStepFieldData()
	{
		presentFieldData = DataManager.FindFieldDataByStep( manager.GamePlayer.StoreData.StoreStep );
	}

	public void FieldPolicy()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			target = GetClickedObject();

			if( target != null )
			{
				if( target.name == "CloudCanvas" )
				{
					target.GetComponent<ObjectOnField>().CloudCooltime();
				}
			}

		}

		if( currentObjectNumber < maxObject )
		{
			if( !objectLoad )
			{
				StartCoroutine( "ObjectUpdating" );
				objectLoad = true;
			}
		}
	}

	public void CloneObject()
	{
		GameObject _temp;
	
		switch( presentFieldData.Step )
		{
			case 1:
				temp = UnityEngine.Random.Range( 0, step1Position.Length );
				Overlap( temp, 1 );
				position = stepPosition[ temp ].transform.position;

				break;
			case 2:
				temp = UnityEngine.Random.Range( 0, step1Position.Length + step2Position.Length );
				Overlap( temp, 2 );
				position = stepPosition[ temp ].transform.position;
				break;

			case 3:
				temp = UnityEngine.Random.Range( 0, step1Position.Length + step2Position.Length + step3Position.Length );
				Overlap( temp, 3 );
				position = stepPosition[ temp ].transform.position;

				break;

			default:

				break;
		}



		_temp = Instantiate( objects[ UnityEngine.Random.Range( 0, 4 ) ], position, new Quaternion( 0, UnityEngine.Random.Range( -1.0f, 1.0f ), 0, 1 ) ) as GameObject;
		ObjectOnField tempObject = _temp.transform.Find( "CloudCanvas" ).GetComponent<ObjectOnField>();
		tempObject.position = temp;

		fieldObjectSet.Add( _temp );

		currentObjectNumber++;
	}

	public void ClearObject()
	{
		for( int i = 0; i < fieldObjectSet.Count; i++ )
		{
			Destroy( fieldObjectSet[ i ] );
		}

		fieldObjectSet = new List<GameObject>( );
		currentObjectNumber = 0;
	}

	private void Overlap( int i, int step )
	{
		temp = i;

		switch( step )
		{
			case 1:
				if( stepLocation[ i ] )
				{
					Overlap( UnityEngine.Random.Range( 0, step1Position.Length ), 1 );
				}
				else
				{
					stepLocation[ i ] = true;
				}

				break;
			case 2:
				if( stepLocation[ i ] )
				{
					Overlap( UnityEngine.Random.Range( 0, step1Position.Length + step2Position.Length ), 2 );
				}
				else
				{
					stepLocation[ i ] = true;
				}
				break;

			case 3:
				if( stepLocation[ i ] )
				{
					Overlap( UnityEngine.Random.Range( 0, step1Position.Length + step2Position.Length + step3Position.Length ), 3 );
				}
				else
				{
					stepLocation[ i ] = true;
				}

				break;
		}
	}

	private GameObject GetClickedObject()
	{
		RaycastHit hit;
		GameObject target = null;

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		if( true == ( Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask ) ) )
		{
			target = hit.collider.gameObject;
		}
		Debug.DrawLine( Camera.main.transform.position, hit.point, Color.red );


        
		return target;
	}

	// coroutine section

	IEnumerator ObjectUpdating()
	{
		yield return new WaitForSeconds( updateTime );
		CloneObject();
		objectLoad = false;
	}

	// stage
	public IEnumerator CreateFieldItemPolicy()
	{
		while( ( manager.PresentMode == GameManager.GameMode.Store ) || ( manager.PresentMode == GameManager.GameMode.StoreCustomizing ) )
		{
			if( currentObjectNumber >= presentFieldData.ObjectMaxCount )
			{
				yield return new WaitForSeconds( presentFieldData.CreateTime + currentObjectNumber );
			}
			else
			{
				CloneObject();
				yield return new WaitForSeconds( presentFieldData.CreateTime + currentObjectNumber );
			}
		}
	}
}



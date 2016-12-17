using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;

	// cast data
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;

	// player character
	[SerializeField] Transform storeDoor;
	[SerializeField] PlayerAgent playerableCharacter;

	// kakao character
	[SerializeField] KakaoAgent kakaoAgent;

	// marshmello character
	[SerializeField] bool marshmelloActivate;
	[SerializeField] MarshmelloAgent pinkyAgent;

	// create field
	[SerializeField] string characterName;

	// property
	public PlayerAgent PlayerableCharacter { get { return playerableCharacter; } }

	public KakaoAgent MrKakao { get { return kakaoAgent; } }

	public MarshmelloAgent PinkyAgent { get { return pinkyAgent; } }

	public bool MarshmelloActivate { get { return marshmelloActivate; } set { marshmelloActivate = value; } }

	// unity method
	void Awake()
	{
		DataInitialize();
	}

	// public method
	public void DataInitialize()
	{
		// high structure
		manager = GetComponent<GameManager>();
		storeManager = GetComponent<StoreManager>();

		// field
		kakaoAgent = GameObject.Find( "KakaoAgent" ).GetComponent<KakaoAgent>();
		pinkyAgent = GameObject.Find( "MarshmelloAgent" ).GetComponent<MarshmelloAgent>();
	}

	// player section
	// create player agent -> store create
	public void CreatePlayerAgent()
	{		
		storeDoor = GameObject.Find( "StoreDoor" ).transform;
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Character/Playerable/" + manager.GamePlayer.CharacterType + "Step" + manager.GamePlayer.StoreData.StoreStep ),
		                                              storeDoor.position - new Vector3( 0f, 0f, 2f ),
		                                              Quaternion.identity );
		playerableCharacter = temp.GetComponent<PlayerAgent>();
	}

	// clear player agent
	public void ClearPlayerAgent()
	{
		Destroy( playerableCharacter.gameObject );
	}

	// set information character
	public void SetCharacterInformation( int type )
	{
		if( type == 0 )
			characterName = "Chou";
		else if( type == 1 )
			characterName = "Berry";
	}

	//
	public void SetInformation( string name )
	{
		manager.GamePlayer.CharacterType = characterName;
		manager.GamePlayer.Name = name;
		DataManager.SavePlayerData();
	}

	// kakao section
	public void ActivateKakao()
	{
		kakaoAgent.GoToStore();
	}

	// marshmello section
	public void ActivateMarshMello()
	{
		if( pinkyAgent.PresentSequence == MarshmelloAgent.Sequence.Ready )
		{
			pinkyAgent.GoToStore();
			marshmelloActivate = true;
		}
	}

	// click count on -> use stage policy
	public bool DamageMarshmello()
	{
		if( pinkyAgent.PresentSequence != MarshmelloAgent.Sequence.Ready )
		{			
			ray = Camera.main.ScreenPointToRay( Input.mousePosition );

			if( Input.GetButtonDown( "LeftClick" ) && !EventSystem.current.IsPointerOverGameObject( Input.GetTouch( 0 ).fingerId ) )
			{
				Debug.Log( "check click" );
				if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Marshmello" ) ) )
				{
					Debug.Log( " ray cast success" );
					GameObject tempSearch = hitInfo.collider.gameObject;
					MarshmelloAgent tempMarshmello = tempSearch.GetComponent<MarshmelloAgent>();

					tempMarshmello.PresentClick += 1;
					return true;
				}
			}
		}

		return false;
	}


	// logic stage reset
	// stage preprocess reset
	public void StagePreprocessReset()
	{
		kakaoAgent.GoToOffice();	
	}

	// stage reset
	public void StageReset()
	{
		kakaoAgent.GoToOffice();
		pinkyAgent.GoToHome();
		pinkyAgent.ResetLoopData();
	}
}

using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;

	// player character
	[SerializeField] Transform storeDoor;
	[SerializeField] PlayerAgent playerableCharacter;

	// kakao character
	[SerializeField] KakaoAgent kakaoAgent;

	// marshmello character

	// create field
	[SerializeField] string characterName;

	// property
	public PlayerAgent PlayerableCharacter { get { return playerableCharacter; } }

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
	}

	// player section
	// create player agent -> store create
	public void CreatePlayerAgent()
	{		
		storeDoor = GameObject.Find( "StoreDoor" ).transform;
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Character/Playerable/" + manager.GamePlayer.CharacterType + "Step" + manager.GamePlayer.StoreData.StoreStep ),
		                                              storeDoor.position,
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
	}
}

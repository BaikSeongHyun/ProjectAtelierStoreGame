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

	// create field
	[SerializeField] string characterName;
	// unity method
	void Awake()
	{
		DataInitialize();
	}

	// public method
	public void DataInitialize()
	{
		manager = GetComponent<GameManager>();
		storeManager = GetComponent<StoreManager>();
	}

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
}

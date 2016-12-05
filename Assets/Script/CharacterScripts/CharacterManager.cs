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
}

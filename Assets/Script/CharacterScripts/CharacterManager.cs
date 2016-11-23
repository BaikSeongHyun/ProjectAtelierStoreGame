using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// player character
	[SerializeField] PlayerAgent playerCharacter;

	// unity method
	void Awake()
	{
		DataInitialize();
	}

	// public method
	public void DataInitialize()
	{
		manager = GetComponent<GameManager>();
	}

	// create player agent -> store create
	public void CreatePlayerAgent( Vector3 position )
	{
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Character/Playerable/" + manager.GamePlayer.CharacterType + "Step" + manager.GamePlayer.StoreData.StoreStep ),
		                                              position,
		                                              Quaternion.identity );
		playerCharacter = temp.GetComponent<PlayerAgent>();
	}
}

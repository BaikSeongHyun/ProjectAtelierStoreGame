using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// field - store open data
	[SerializeField] float customerCycle;
	[SerializeField] int customerIndex;

	// field - information data
	[SerializeField] int probabilityOfBuyScale;
	[SerializeField] int probabilityOfFavoriteGroup;
	[SerializeField] ItemData.ItemType favoriteGroup;
	[SerializeField] CustomerAgent.BuyScale buyScale;

	// field - element object
	[SerializeField] PlayerAgent playerAgent;
	[SerializeField] List<CustomerAgent> customerAgentSet;

	// unity method
	// awake -> data initialize
	void Awake()
	{
		manager = GetComponent<GameManager>();
		//customerAgentSet = new List<CustomerAgent>( );
	}

	// public method
	// create game information
	public void CreateGameInformation()
	{
		// allocate probability
		switch( manager.GamePlayer.StoreData.StoreStep )
		{
			case 1:
				{
					probabilityOfBuyScale = 100;
					probabilityOfFavoriteGroup = 100;
				}
				break;
			case 2:
				{
					probabilityOfBuyScale = 75;
					probabilityOfFavoriteGroup = 75;
				}
				break;
			case 3:
				{
					probabilityOfBuyScale = 50;
					probabilityOfFavoriteGroup = 50;
				}
				break;
		}

		// allocate data
		buyScale = CustomerAgent.ReturnBuyScale( Random.Range( 1, 6 ) );
		favoriteGroup = ItemData.ReturnType( Random.Range( 1, 7 ) );
	}

	// create player agent -> store create
	public void CreatePlayerAgent( Vector3 position )
	{
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Character/Playerable/" + manager.GamePlayer.CharacterType + "Step" + manager.GamePlayer.StoreData.StoreStep ),
		                                              position,
		                                              Quaternion.identity );
		playerAgent = temp.GetComponent<PlayerAgent>();
	}

	// create customer agent
	public void CreateCustomerAgent( Vector3 position )
	{
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "Character/Customer/Customer" + Random.Range( 0, 2 ) ),
		                                              position,
		                                              Quaternion.identity );
		customerAgentSet.Add( temp.GetComponent<CustomerAgent>() );
	}

	public void StoreOpen()
	{
		StartCoroutine( CustomerGoStore() );
	}

	public void StoreClose()
	{

	}
	//
	public void RushAllCustomer()
	{
		foreach( CustomerAgent element in customerAgentSet )
		{
			element.GoToStore();
		}
	}

	IEnumerator CustomerGoStore()
	{
		while( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			customerAgentSet[ customerIndex ].MoveStart();
			customerIndex++;
			if( customerIndex >= customerAgentSet.Count )
				customerIndex = 0;
			yield return new WaitForSeconds( customerCycle ); 
		}
	}

}

using UnityEngine;
using System.Collections.Generic;

public class SendData : MonoBehaviour
{

	[SerializeField] PlayerData player;
	public List<ItemData> data;
	public List<int> count;
	public bool isMain = false;

	void Awake()
	{
		data = new List<ItemData>();
	}

	void Start()
	{
		FieldStart();
	}

	void Update()
	{
		StoreStart();
	}


	void ReceiveDataSave()
	{
		for( int i = 0; i < data.Count; i++ )
		{
			player.AddItemData( data[ i ], count[ i ] );
		}
		Debug.Log( "Data Save Success" );
	}

	void FieldStart()
	{
		if( GameObject.Find( "FieldUI" ) )
		{
			DontDestroyOnLoad( this.gameObject );
		}
	}

	void StoreStart()
	{
		if( !isMain )
		{
			if( GameObject.Find( "MainUI" ) )
			{
				player = GameObject.Find( "GameLogic" ).GetComponent<GameManager>().GamePlayer;
				ReceiveDataSave();
				Destroy( this.gameObject );
				isMain = true;
			}
		}
	}
}

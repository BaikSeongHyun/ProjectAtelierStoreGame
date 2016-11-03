using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSave : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	void Start()
	{
		if( GameObject.Find( "MainUI" ) )
		{
			gameManager = GameObject.Find( "GameLogic" ).GetComponent<GameManager>();
		}
	}


	//Save
	public void Data()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create( Application.persistentDataPath + GameData.fileName );

		GameData data = new GameData();

		for( int i = 0; i < gameManager.GamePlayer.ItemSet.Length; i++ )
		{
			if( gameManager.GamePlayer.ItemSet[ i ].Count != 0 )
			{
				data.item.Add( gameManager.GamePlayer.ItemSet[ i ].Item );
				data.count.Add( gameManager.GamePlayer.ItemSet[ i ].Count );
			}
		}

		bf.Serialize( file, data );
		file.Close();
	}
}

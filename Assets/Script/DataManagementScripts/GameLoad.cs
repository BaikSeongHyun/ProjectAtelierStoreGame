using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameLoad : MonoBehaviour
{
    [SerializeField] GameManager manager;

    void Start()
    {
        if (GameObject.Find("MainUI"))
        {
            manager = GameObject.Find("GameLogic").GetComponent<GameManager>();

        }
    }

    //Load
    public void Data()
    {
        if (File.Exists(Application.persistentDataPath + GameData.fileName))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + GameData.fileName, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);

            file.Close();

            if (data.item != null)
            {
                for (int i = 0; i < data.item.Count; i++)
                {
                    Debug.Log(data.item[i]);
                    Debug.Log(data.count[i]);
					manager.GamePlayer.AddItemData(data.item[i], data.count[i]);
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SceneChangeManager : MonoBehaviour {

    [SerializeField] GameManager manager;
    [SerializeField] GameSave save;
    [SerializeField] GameLoad load;
    [SerializeField] static bool isGameStart = false;

    void Start () {
        save = GetComponent<GameSave>();
        load = GetComponent<GameLoad>();

        if(File.Exists(Application.persistentDataPath + GameData.fileName) && GameObject.Find("MainUI"))
        {
            manager = GameObject.Find("GameLogic").GetComponent<GameManager>();
            load.Data();

            if (isGameStart)
            {
                Debug.Log("static true");
                manager.GameStart(); 
            }
            else
            {
                Debug.Log("static false");
                isGameStart = true;
            }
        }

    }
	
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadSceneAsync("InvenTest002");
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            save.Data();
            SceneManager.LoadSceneAsync("Field");
        }
	}
}

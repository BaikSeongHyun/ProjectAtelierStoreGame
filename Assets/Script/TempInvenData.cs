using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TempInvenData : MonoBehaviour {

    public int[] inventory;

    void Start () {
        inventory = new int[3] {0,0,0}; //빨파초
        DontDestroyOnLoad(this.gameObject);
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.K))
        {
            
            SceneManager.LoadScene("UIScene");
        }

        if(Input.GetKeyDown(KeyCode.L))
        {

            SceneManager.LoadScene("Field");
        }
	}

    public int numberOfItem(string _item)
    {
        switch (_item)
        {
            case "red":
                return inventory[0];
            case "blue":
                return inventory[1];
            case "green":
                return inventory[2];
            default:
                Debug.Log("Data error");
                return 0;
        }

    }
}

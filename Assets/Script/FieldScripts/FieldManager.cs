using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour {

<<<<<<< HEAD
	// Use this for initialization
	void Start () {
	
=======
    void Awake()
    {
        FieldSettingStart();
    }

    void Start()
    {
        objRegenLogic.SetObjRegeneration();
    }

    public void FieldSettingStart()
    {
        player = GameObject.Find("Player");
        playerLogic = player.GetComponent<PlayerOnField>();
        objRegen = GameObject.Find("ObjectManager");
        objRegenLogic = objRegen.GetComponent<ObjectRegeneration>();
    }

    // 필드 만들기
    public bool CreateField()
	{
        return true;
>>>>>>> 76d2c379ab57c7bae4cf9fb5f350d93664b81e9c
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

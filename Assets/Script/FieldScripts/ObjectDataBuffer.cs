using UnityEngine;
using System.Collections;

public class ObjectDataBuffer : MonoBehaviour {

    public FieldUIManager fieldMgr;

    public float cooltime;
    public bool receiveData=false;
    public bool sendData=false;

    void Start()
    {
        fieldMgr = GameObject.Find("FieldUI").GetComponent<FieldUIManager>();
        fieldMgr.cooltimeImage.SetActive(false);

    }

    void Update()
    {
        if(receiveData)
        {
            sendData = true;
            fieldMgr.cooltimeImage.SetActive(true);
            receiveData = false;
        }
    }
}

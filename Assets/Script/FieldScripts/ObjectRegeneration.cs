using UnityEngine;
using System.Collections;

public class ObjectRegeneration : MonoBehaviour {

    public Camera _camera;

    public GameObject[] objects; //prefabs

    private Vector3 pos;
    private float[] randPos;

    public static int curObjects;
    private int maxObject = 10;
    public bool objectLoad = false;

    void Awake()
    {
        _camera = Camera.main;

        //Field에서 x,z 범위 입니다. 1.4f 간격입니다.
        randPos = new float[7] { -1.2f, 0.2f, 1.6f, 3.0f, 4.4f, 5.8f, 7.2f };

    }
    void Start()
    {
        
    }

    void Update()
    {
        if (curObjects < maxObject)
        {
            if (!objectLoad)
            {
                objectLoad = true;
                StartCoroutine("ObjectUpdating");
            }
        }
    } 

    //public void SetObjRegeneration()
    //{
    //    curObjects = Random.Range(4, 11);
    //    Debug.Log(curObjects+ "개 소환합니다");

    //    for (int i = 0; i < curObjects; i++)
    //    {
    //        CloneObject();
    //    }
    //}

    public void CloneObject()
    {
        float x = randPos[Random.Range(0, 7)];
        float z = randPos[Random.Range(0, 7)];
        pos = new Vector3(x, 3.5f, z);

        Instantiate(objects[Random.Range(0, objects.Length)], pos, Quaternion.identity);
    }

    IEnumerator ObjectUpdating()
    {
        Debug.Log("오브젝트 소환중  ");
        yield return new WaitForSeconds(0.1f);
        CloneObject();
        curObjects++;
        Debug.Log("필드 내 오브젝트 수  :  " + curObjects + " / " + maxObject);
        objectLoad = false;
    }
}

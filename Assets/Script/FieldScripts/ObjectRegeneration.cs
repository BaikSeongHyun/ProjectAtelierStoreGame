using UnityEngine;
using System.Collections;

public class ObjectRegeneration : MonoBehaviour {

    public Camera _camera;

    public GameObject[] objects;

    private Vector3 pos;
    private float[] randPos;
     
    void Awake()
    {
        _camera = Camera.main;

        //Field에서 x,z 범위 입니다. 1.4f 간격입니다.
        randPos = new float[7] { -1.2f, 0.2f, 1.6f, 3.0f, 4.4f, 5.8f, 7.2f };

    }
    void Start()
    {
        CloneObject();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CloneObject();
        }

        Ray ray = _camera.ScreenPointToRay(pos);

        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit, 10f))
        {
            Debug.Log("ㅇㅅㅇ");
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);
        }
    } 

    public void CloneObject()
    {
        float x = randPos[Random.Range(0, 7)];
        float z = randPos[Random.Range(0, 7)];
        pos = new Vector3(x, 3.5f, z);


        Instantiate(objects[Random.Range(0, 3)], pos, Quaternion.identity);
    }
}

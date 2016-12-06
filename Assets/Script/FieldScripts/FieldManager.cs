using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FieldManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
    [SerializeField] PlayerData player;

    // logic data field
    GameObject target; //clicked object
    int layerMask; 

    public PresentState state;

    public GameObject[] objects; //prefabs
    private GameObject[] step1Position;
    private GameObject[] step2Position;
    private GameObject[] step3Position;

    public GameObject[] stepPosition;

    public bool[] stepLocation;

    public int temp;

    public Vector3 position;

    public static int currentObjectNumber;
    private int maxObject;
    private bool objectLoad = false;

    public float updateTime;

    // temporary storage of prefab data
    GameObject destroyBody;
    ObjectOnField tempItemData;

    // now step
    public enum PresentState : int
    {
        Default = 0,
        Step1,
        Step2,
        Step3
    };


    void Awake()
    {
        LinkComponentElement();
    }

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Cloud");

    }

    void Update()
    {
        FieldPolicy();

    }

    public void LinkComponentElement()
    {
        manager = GetComponent<GameManager>();
        player = manager.GamePlayer;

        objects = new GameObject[4];
        objects[0] = Resources.Load("Prefabs/Field/bushPurple") as GameObject;
        objects[1] = Resources.Load("Prefabs/Field/bushYellow") as GameObject;
        objects[2] = Resources.Load("Prefabs/Field/StoneBlue") as GameObject;
        objects[3] = Resources.Load("Prefabs/Field/StoneRed") as GameObject;

        step1Position = GameObject.FindGameObjectsWithTag("Step1Position");
        step2Position = GameObject.FindGameObjectsWithTag("Step2Position");
        step3Position = GameObject.FindGameObjectsWithTag("Step3Position");

        stepPosition = new GameObject[step1Position.Length + step2Position.Length + step3Position.Length];
        Array.Copy(step1Position, 0, stepPosition, 0, step1Position.Length);
        Array.Copy(step2Position, 0, stepPosition, step1Position.Length, step2Position.Length);
        Array.Copy(step3Position, 0, stepPosition, step1Position.Length+step2Position.Length, step3Position.Length);

        stepLocation = new bool[stepPosition.Length];

        position = new Vector3();

        if (state == PresentState.Step1)
        {
            maxObject = step1Position.Length;
        }
        else if(state == PresentState.Step2)
        {
            maxObject = step1Position.Length + step2Position.Length;
        }
        else if(state == PresentState.Step3)
        {
            maxObject = step1Position.Length + step2Position.Length + step3Position.Length;
        }

    }

    public void FieldPolicy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();

            if (target != null)
            {
                if (target.name == "CloudCanvas")
                {
                    tempItemData = target.GetComponent<ObjectOnField>();
                    destroyBody = tempItemData.body;
                    player.AddItemData(tempItemData.id, tempItemData.count);
                    stepLocation[tempItemData.position] = false;
                    currentObjectNumber--;
                    Destroy(destroyBody);
                }
            }

        }

        if (currentObjectNumber < maxObject)
        {
            if (!objectLoad)
            {
                StartCoroutine("ObjectUpdating");
                objectLoad = true;
            }
        }
    }

    public void CloneObject()
    {
        GameObject _temp;

        switch (state)
        {
            case PresentState.Step1:
                temp = UnityEngine.Random.Range(0, step1Position.Length);
                Overlap(temp, PresentState.Step1);
                position = stepPosition[temp].transform.position;

                break;
            case PresentState.Step2:
                temp = UnityEngine.Random.Range(0, step1Position.Length + step2Position.Length);
                Overlap(temp, PresentState.Step2);
                position = stepPosition[temp].transform.position;
                break;

            case PresentState.Step3:
                temp = UnityEngine.Random.Range(0, step1Position.Length + step2Position.Length + step3Position.Length);
                Overlap(temp, PresentState.Step3);
                position = stepPosition[temp].transform.position;

                break;

            default:

                break;
        }

        _temp = Instantiate(objects[UnityEngine.Random.Range(0, 4)], position, new Quaternion(0, UnityEngine.Random.Range(-1.0f, 1.0f), 0, 1)) as GameObject;
        ObjectOnField tempObject = _temp.transform.Find("CloudCanvas").GetComponent<ObjectOnField>();
        tempObject.position = temp;


        currentObjectNumber++;

    }

    IEnumerator ObjectUpdating()
    {
        yield return new WaitForSeconds(updateTime);
        CloneObject();
        objectLoad = false;
    }

    void Overlap(int i, PresentState step)
    {
        temp = i;

        switch (state)
        {
            case PresentState.Step1:
                if(stepLocation[i])
                {
                    Overlap(UnityEngine.Random.Range(0, step1Position.Length), PresentState.Step1);
                }
                else
                {
                    stepLocation[i] = true;
                }

                break;
            case PresentState.Step2:
                if (stepLocation[i])
                {
                    Overlap(UnityEngine.Random.Range(0, step1Position.Length + step2Position.Length), PresentState.Step2);
                }
                else
                {
                    stepLocation[i] = true;
                }
                break;

            case PresentState.Step3:
                if (stepLocation[i])
                {
                    Overlap(UnityEngine.Random.Range(0, step1Position.Length + step2Position.Length + step3Position.Length), PresentState.Step3);
                }
                else
                {
                    stepLocation[i] = true;
                }

                break;
        }


        }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)))
        {
            target = hit.collider.gameObject;
        }
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);


        
        return target;
    }
}



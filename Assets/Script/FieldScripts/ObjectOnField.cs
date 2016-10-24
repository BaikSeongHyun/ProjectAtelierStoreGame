using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectOnField : MonoBehaviour {

    public PlayerOnField player;
    public TempInvenData tempdata;
    public ObjectDataBuffer databf;

    public Renderer rend;
    public bool objectOnMouse = false;
    public bool getObject = false;
    public Color myColor;

    //Item List 에서 갖고있는 아이템이름'ㅅ'
    public string haveItem;
    public int itemCount;
    public float cooltime;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerOnField>();
        tempdata = GameObject.Find("tempInven").GetComponent<TempInvenData>();
        databf = GameObject.Find("ObjectManager").GetComponent<ObjectDataBuffer>();

        rend = GetComponent<Renderer>();
        myColor = rend.material.color;
        itemCount = Random.Range(1, 4);


    }

    void Update()
    {
        GetMaterials();

    }

    void OnMouseEnter()
    {
        rend.material.color = Color.gray;
        objectOnMouse = true;
    }
    void OnMouseExit()
    {
        rend.material.color = myColor;
        objectOnMouse = false;
    }


    void GetMaterials()
    {
        if (objectOnMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(this.name + " 오브젝트를 클릭했다.");
                getObject = true;
            }
        }

        if(getObject)
        {
            if(player.frontOfObject)
            {
                Debug.Log("플레이어가 물체앞");
                StartCoroutine("StartAction");
                getObject = false;
                player.frontOfObject = false;
            }

        }
    }


    public IEnumerator StartAction()
    {
        Debug.Log("재료수집 쿨타임 " + cooltime);
        databf.receiveData = true;
        databf.cooltime = cooltime;

        yield return new WaitForSeconds(cooltime);
        Debug.Log("재료수집성공");
        tempdata.inventory[returnItems(haveItem)] += itemCount;
        ObjectRegeneration.curObjects--;
        Destroy(this.gameObject);
    }

    int returnItems(string _name)
    {
        switch (_name) 
        {
            case "red":
                return 0;
            case "blue":
                return 1;
            case "green":
                return 2;
            default:
                Debug.Log("오류");
                return 0;
        }

    }



}

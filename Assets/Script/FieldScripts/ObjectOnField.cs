using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ObjectOnField : MonoBehaviour {

    [SerializeField] PlayerOnField player;
    [SerializeField] SendData sendData;
    public ObjectDataBuffer databf;

    public Renderer rend;
    public bool objectOnMouse = false;
    public bool getObject = false;
    public Color myColor;

    //Item List 에서 갖고있는 아이템이름

    public int uid;

    public int count;
    public int min, max;

    public float cooltime;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerOnField>();
        databf = GameObject.Find("ObjectManager").GetComponent<ObjectDataBuffer>();
        sendData = GameObject.Find("Data").GetComponent<SendData>();

        rend = GetComponent<Renderer>();
        myColor = rend.material.color;

        count = Random.Range(min, max);
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

        sendData.data.Add(new ItemData(DataManager.FindItemDataByUID(uid)));
        sendData.count.Add(count);

        ObjectRegeneration.curObjects--;
        Destroy(this.gameObject);
    }
}

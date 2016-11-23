using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MixManager : MonoBehaviour {

    public GameObject listMould; //Resource ->Prefabs -> MixMaterialListMould 1

    //제작목록
    public Image[] mixList; //제작할 것들의 이미지를 넣을 목록

    //ShopList에 필요한 것들
    private GameObject mixContent; //awake
    private Transform contentStart; //awake
    private Transform contentEnd; //awake
    private Transform contentBottom;
    private float interval; //start
    public int num; //제작목록 갯수

    public bool loading = false;

    void Awake()
    {
        mixContent = GameObject.Find("MixContent");
        contentStart = GameObject.Find("MixStartPosition").GetComponent<Transform>();
        contentEnd = GameObject.Find("MixEndPosition").GetComponent<Transform>();
        contentBottom = GameObject.Find("MixBottomPosition").GetComponent<Transform>();
    }

    void Start () {
        interval = contentEnd.position.x - contentStart.position.x;
        //FristCreateList는
        //폰과 화면크기에 따른 interval 간격조절 때문에 Start에서 interval 계산후 실행
        FirstCreateList();
    }
	
	void Update () {
	
	}

    void FirstCreateList()
    {
        //List를 갯수만큼 생성하고 스크립트랑 오브젝트랑 연결시켜주기.

        for ( int i = 0 ; i < num/2 ; i++ )
        {
            GameObject temp = Instantiate(listMould, transform.position, Quaternion.identity) as GameObject;
            temp.transform.SetParent(mixContent.transform, false);
            temp.transform.position = contentStart.position + new Vector3(interval * i, 0f, 0f);
            temp.name = "mixList" + i.ToString();
        }
        for( int i = num/2 ; i < num ; i++ )
        {
            GameObject temp = Instantiate(listMould, transform.position, Quaternion.identity) as GameObject;
            temp.transform.SetParent(mixContent.transform, false);
            temp.transform.position = contentBottom.position + new Vector3(interval * (i - (num/2)), 0f, 0f);
            temp.name = "mixList" + i.ToString();
        }
    }

    public void SecondCreateList()
    {
        if (!loading)
        {
            Debug.Log("첫로딩");

            //스크립트와 캔버스의 오브젝트 연결
            mixList = new Image[num];
            for (int i = 0; i < num; i++)
            {
                mixList[i] = GameObject.Find("mixList" + i.ToString()).GetComponent<Image>();
            }

            //복제되는 영역 content 조절하기위해서 RectTransform잡아옴
            RectTransform content = mixContent.GetComponent<RectTransform>();
            //갯수(num)에따라 길이조절
            content.sizeDelta = new Vector2(201f * num, content.sizeDelta.y);

            //Debug.Log(content.sizeDelta);



            //이미지와 btn의 스크립트에 ItemData 연결해주기
            for (int i = 0; i < 9; i++)
            {
                DataSetting(i, i + 7);
            }
            DataSetting(9, 18);
            loading = true;
        }
    }

    public void DataSetting(int num, int id)
    {
        if (id != 0)
        {
            mixList[num].sprite = Resources.Load<Sprite>("Image/UI/ItemIcon/" + ID(id).File) as Sprite;

            //List Button 스크립트로 데이터 전송
            mixSelected data;
            data = mixList[num].GetComponent<mixSelected>();
            data.info = ID(id);

            if(loading && !data.btn.isActiveAndEnabled)
            {
                Debug.Log("enable : false -> true");
                data.btn.enabled = true;
            }
        }
        else
        {
            mixList[num].sprite = Resources.Load<Sprite>("Image/UI/ItemIcon/none") as Sprite;

            mixSelected data;
            data = mixList[num].GetComponent<mixSelected>();
            data.info = null;

            Debug.Log("enable : true -> false");
            data.btn.enabled = false;
        }

    }


    public ItemData ID(int id)
    {
        // id reading in XML => Item Data All Load
        ItemData data = DataManager.FindItemDataByID(id);
        return data;
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Quest : MonoBehaviour {

    private UIManager uiManager;
    private QuestList queList;

    private Image 캐릭터이미지;
    private Text 주제;
    private Text 대사;

    private Text[] 재료명 = new Text[3];
    private Text[] 조건 = new Text[3];
    private Text[] 소유 = new Text[3]; //Inventory에서 끌어오기

    private Text 보상아이템명 ; //차후에 이미지로 바꿔도 괜찮을것같음. 골드랑 경험치듕'ㅅ')~
    private Text 보상경험치 ;
    private Text 보상골드 ;
    private Text 보상재료명 ;

    public GameObject QuestIcon;
    private GameObject clone;
    private List<GameObject> clones;
    private Vector3 IconPosition;
    private int iconNum = 1;

    private int cloneNum = 0;
    private int num;
    private int temp;


    void Awake()
    {
        UISetting();
    }

    void Start () {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        clones = new List<GameObject>();
        IconPosition = new Vector3(150f, 1400f, 0f);

        
    }
	
	void Update () {

        //연습용
    	if(Input.GetKeyDown(KeyCode.A))
        {
            clones.Add(Instantiate(QuestIcon, IconPosition + (Vector3.down * 260f * iconNum++), Quaternion.identity) as GameObject);
            clones[cloneNum++].transform.SetParent(this.transform);
        }
	}

    public void PopupContent(int _storyNumber)
    {
        num = _storyNumber;

        주제.text = queList.addQue[num].title;
        대사.text = queList.addQue[num].ment;

        for(int i=0; i < queList.addQue[num].demandItem.Length; i++)
        {
            재료명 [i].text = queList.addQue[num].demandItem[i];
            //소유 inventory에서 끌어오기
            소유[i].text = "0";
            조건 [i].text = "/ " + queList.addQue[num].amount[i].ToString();
        }
        if (queList.addQue[num].demandItem.Length != 3)
        {
            for (int i = queList.addQue[num].demandItem.Length; i < 3; i++)
            {
                재료명 [i].text = "";
                소유 [i].text = "";
                조건 [i].text = "";
            }
        }

        보상아이템명.text = queList.addQue[num].rewardItem;
        보상경험치.text = queList.addQue[num].rewardExp.ToString();
        보상골드.text = queList.addQue[num].rewardGold.ToString();
        보상재료명.text = queList.addQue[num].numberOfRewardItem.ToString();
    }

    void UISetting()
    {
        queList = GameObject.Find("sc").GetComponent<QuestList>();

        캐릭터이미지 = GameObject.Find("캐릭터image").GetComponent<Image>();
        주제  = GameObject.Find("주제text").GetComponent<Text>();
        대사  = GameObject.Find("캐릭터대사text").GetComponent<Text>();

        for (int i = 0; i < 3; i++)
        {
            재료명[i] = GameObject.Find("재료text" + i.ToString()).GetComponent<Text>();
            소유[i] = GameObject.Find("소유text" + i.ToString()).GetComponent<Text>();
            조건[i] = GameObject.Find("조건text" + i.ToString()).GetComponent<Text>();
        }

        보상아이템명 = GameObject.Find("보상재료명_text").GetComponent<Text>();
        보상경험치 = GameObject.Find("exp_text").GetComponent<Text>();
        보상골드 = GameObject.Find("gold_text").GetComponent<Text>();
        보상재료명 = GameObject.Find("mat_text").GetComponent<Text>();
    }

    public void QuestSuccessBtn()
    {
        uiManager.questPopup.SetActive(false);

        for(int i=0; i<clones.Count; i++)
        {
            if(GameObject.Find(num.ToString()).name == i.ToString())
            {
                temp = i;
                break;
            }
        }
        Destroy(clones[temp]);
        clones.Remove(clones[temp]);
        cloneNum = clones.Count;

        IconArrangement();
    }

    void IconArrangement()
    {
        iconNum = 1;
        Debug.Log("Icon Arrangement");
        foreach(GameObject clone in clones)
        {
            clone.transform.position = IconPosition + (Vector3.down * 260f * iconNum++);
        }
    }
    
}

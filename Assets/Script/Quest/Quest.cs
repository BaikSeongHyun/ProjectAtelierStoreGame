using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{

	private UIManager uiManager;
	private QuestList queList;

	private Image 캐릭터이미지;
	private Text 주제;
	private Text 대사;

	private Text[] 재료명 = new Text[3];
	private Text[] 조건 = new Text[3];
	private Text[] 소유 = new Text[3];
	//Inventory에서 끌어오기

	private Text 보상아이템명;
	//차후에 이미지로 바꿔도 괜찮을것같음. 골드랑 경험치듕'ㅅ')~
	private Text 보상경험치;
	private Text 보상골드;
	private Text 보상재료명;

	public GameObject QuestIcon;
	//private GameObject clone;
	private List<GameObject> clones;
	private Vector3 IconPosition;
	private int iconNum = 1;

	private int cloneNum = 0;
	private int num;
	private int temp;

    private RectTransform questBtnSize;
    private float width;
    private float height;
    private float interval;


    private TempInvenData inven;


    void Awake()
	{
		UISetting();
	}

	void Start()
	{
		uiManager = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		clones = new List<GameObject>();
		IconPosition = new Vector3( 0f,0f, 0f );


        //필드 씬에서 시작해야하게 해놨습니다. (수정중)
        //tempInven - 체크꺼놨는데 켜면 됩니당
        inven = GameObject.Find("tempInven").GetComponent<TempInvenData>();

    }

	void Update()
	{

		//밑에 계속 수정중 ㅠㅠ...
		if( Input.GetKeyDown( KeyCode.A ) )
		{
			clones.Add( Instantiate( QuestIcon, Vector3.zero, Quaternion.identity ) as GameObject );
//            clones[cloneNum].transform.localPosition = IconPosition;
//			clones[ cloneNum++ ].transform.SetParent( this.transform );
            clones[cloneNum].transform.parent= gameObject.transform;

            clones[cloneNum].GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            clones[cloneNum].GetComponent<RectTransform>().transform.localPosition = new Vector3( width/2 , (height/-4.5f * iconNum++) ,1f) ;
//            clones[cloneNum].transform.GetChild(this.transform);
            Debug.Log(clones[cloneNum].transform.position);
            Debug.Log(clones[cloneNum].transform.localPosition);
            Debug.Log(clones[cloneNum].GetComponent<RectTransform>().rect.x);
            DontDestroyOnLoad(clones[cloneNum]);
            cloneNum++;
		}
	}

	public void PopupContent( int _storyNumber )
	{
		num = _storyNumber;

		주제.text = queList.addQue[ num ].title;
		대사.text = queList.addQue[ num ].ment;

		for( int i = 0; i < queList.addQue[ num ].demandItem.Length; i++ )
		{
            //quest에서 요구하는 아이템의 재료명
			재료명[ i ].text = queList.addQue[ num ].demandItem[ i ];

            //inventory에서 해당 재료명의 갯수를 반환.ToString();
            소유[i].text = inven.numberOfItem(queList.addQue[num].demandItem[i]).ToString();

            //player가 모아와야 할 갯수.
			조건[ i ].text = "/ " + queList.addQue[ num ].amount[ i ].ToString();
		}
		if( queList.addQue[ num ].demandItem.Length != 3 )
		{
			for( int i = queList.addQue[ num ].demandItem.Length; i < 3; i++ )
			{
				재료명[ i ].text = "";
				소유[ i ].text = "";
				조건[ i ].text = "";
			}
		}

		보상아이템명.text = queList.addQue[ num ].rewardItem;
		보상경험치.text = queList.addQue[ num ].rewardExp.ToString();
		보상골드.text = queList.addQue[ num ].rewardGold.ToString();
		보상재료명.text = queList.addQue[ num ].numberOfRewardItem.ToString();
	}

	void UISetting()
	{
		queList = GameObject.Find( "sc" ).GetComponent<QuestList>();

		캐릭터이미지 = GameObject.Find( "캐릭터image" ).GetComponent<Image>();
		주제 = GameObject.Find( "주제text" ).GetComponent<Text>();
		대사 = GameObject.Find( "캐릭터대사text" ).GetComponent<Text>();

		for( int i = 0; i < 3; i++ )
		{
			재료명[ i ] = GameObject.Find( "재료text" + i.ToString() ).GetComponent<Text>();
			소유[ i ] = GameObject.Find( "소유text" + i.ToString() ).GetComponent<Text>();
			조건[ i ] = GameObject.Find( "조건text" + i.ToString() ).GetComponent<Text>();
		}

		보상아이템명 = GameObject.Find( "보상재료명_text" ).GetComponent<Text>();
		보상경험치 = GameObject.Find( "exp_text" ).GetComponent<Text>();
		보상골드 = GameObject.Find( "gold_text" ).GetComponent<Text>();
		보상재료명 = GameObject.Find( "mat_text" ).GetComponent<Text>();

        questBtnSize = GameObject.Find("QuestScrollView").GetComponent<RectTransform>();
        Debug.Log("가로 : "+questBtnSize.rect.width+" / 세로 : "+ questBtnSize.rect.height);
        width = questBtnSize.rect.width;
        height = questBtnSize.rect.height;
        interval = height / 8;
    }

	public void QuestSuccessBtn()
	{
        if(queList.addQue[num].amount[0] <= inven.inventory[0])
        {
            uiManager.questPopup.SetActive(false);
            DeleteQuestIcon();
            IconArrangement();
        }
		else
        {
            Debug.Log("재료 부족합니다.");
        }
	}

    void DeleteQuestIcon()
    {
        //클리어한 아이콘 지웁니다
        for (int i = 0; i < clones.Count; i++)
        {
            if (GameObject.Find(num.ToString()).name == i.ToString())
            {
                temp = i;
                break;
            }
        }
        Destroy(clones[temp]);
        clones.Remove(clones[temp]);
        cloneNum = clones.Count;
    }


	void IconArrangement()
	{
        //아이콘 정렬
		iconNum = 1;
		Debug.Log( "Icon Arrangement" );
		foreach( GameObject clone in clones )
		{
			clone.GetComponent<RectTransform>().transform.localPosition = new Vector3(width / 2, height / -4.5f * iconNum++, 1f);
        }
	}
    
}

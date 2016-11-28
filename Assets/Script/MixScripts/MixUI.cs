using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MixUI : MonoBehaviour
{

	public MixManager mixManager;
	public PlayerData player;

	public GameObject mixView;
	public GameObject resultView;

	public Image productionItem;
	//목록에서 선택한 아이템
	public Text productionName;
	//그 이름

	public GameObject matIsOneAmount;
	public GameObject matIsTwoAmount;
	//재료2개
	public GameObject matISThreeAmount;
	//재료3개
	public Image[] materials;
	public Text[] materialTexts;
	public int[] matSlotNumber;

	public bool terms1; //필요한 재료가 1개 인데 , 있는지 없는지에 대한 bool
	public bool terms2; // 필요한 재료가 2개 인데, 2개 다 있는지 없는지 〃
	public bool terms3; // 필요한 재료가 3개 〃 3개 〃

    public Text countText;
	public Text makeText;

	public GameObject compose;
	//btn
	public Image composeImage;

	public Image resultImage;
	public Text resultName;
	public Text resultCount;

	public ItemData itemData; //mixSelected 스크립트로부터,  클릭된 itemData를 받아놓음

	public int currentCount = 0; //제작할 것. 현재 갯수                            
    public int fabricableCount = 0; //제작할 수 있는 재료의 최대갯수, 인벤토리의 갯수에 따라..

	public int currentPage = 0; //page
	public int lastPage = 3;

	void Awake()
	{
		mixView = GameObject.Find( "Mix" );
		resultView = GameObject.Find( "MixResult" );

		productionItem = GameObject.Find( "productionItem" ).GetComponent<Image>();
		productionName = GameObject.Find( "productionName" ).GetComponent<Text>();

		matIsOneAmount = GameObject.Find( "One" );
		matIsTwoAmount = GameObject.Find( "Two" );
		matISThreeAmount = GameObject.Find( "Three" );

		materials = new Image[6];
		materialTexts = new Text[6];
		matSlotNumber = new int[6];

		for( int i = 0; i < 6; i++ )
		{
			materials[ i ] = GameObject.Find( "materials" + i.ToString() ).GetComponent<Image>();
			materialTexts[ i ] = materials[ i ].transform.GetChild( 0 ).GetComponent<Text>();
			matSlotNumber[ i ] = -1;
		}

		countText = GameObject.Find( "positiveCountText" ).GetComponent<Text>();
		makeText = GameObject.Find( "makeCountText" ).GetComponent<Text>();

		compose = GameObject.Find( "compose" );
		composeImage = compose.GetComponent<Image>();

		resultImage = GameObject.Find( "resultItemImage" ).GetComponent<Image>();
		resultName = GameObject.Find( "resultItemName" ).GetComponent<Text>();
		resultCount = GameObject.Find( "resultItemCount" ).GetComponent<Text>();
	}


	void Start()
	{
		mixManager = GameObject.Find( "MixUI" ).GetComponent<MixManager>();
		player = GameObject.Find( "GameLogic" ).GetComponent<GameManager>().GamePlayer;

		mixView.SetActive( false );
		resultView.SetActive( false );
		matIsOneAmount.SetActive( false );
		matIsTwoAmount.SetActive( false );
		matISThreeAmount.SetActive( false );
	}

    void Update()
    {
        if(currentCount != 0)
        {
            composeImage.sprite = Resources.Load<Sprite>("Image/UI/Mix/composeTrue") as Sprite;
        }
        else
        {
            composeImage.sprite = Resources.Load<Sprite>("Image/UI/Mix/composeFalse") as Sprite;
        }
    }

	public void MixViewButton()
	{
		if( !mixView.activeSelf )
		{
			mixView.SetActive( true );
			mixManager.SecondCreateList();
		}        
	}

	public void MixCancelButton()
	{
		if( mixView.activeSelf )
		{
			mixView.SetActive( false );

            valueInitialization();

        }
	}

	public void ComposeButton()
	{
		if( !resultView.activeSelf )
		{


			//-1이면 아이템 없는거, 인벤토리 0번부터 15번까지 확인합니다!
			//둘다 아이템이 있어야지 각각 true가 되서 합성을 할 수 있다.
			//배열[0],[1] <-재료가 2개짜리
			//배열[2],[3],[4] <-재료가 3개짜리
			terms1 = matSlotNumber[ 0 ] != -1;
			terms2 = ( ( matSlotNumber[ 1 ] != -1 ) && ( matSlotNumber[ 2 ] != -1 ) );
			terms3 = ( ( matSlotNumber[ 3 ] != -1 ) && ( matSlotNumber[ 4 ] != -1 ) && ( matSlotNumber[ 5 ] != -1 ) );

			if( ( terms1 || terms2 || terms3 ) && ( currentCount != 0 ) )
			{
				Debug.Log( "재료다있음" );
				resultImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + itemData.File ) as Sprite;
				resultName.text = itemData.Name;
				resultCount.text = currentCount.ToString() + "개 획득!";

				//Player 아이템 창고에서 1개씩 감소
				if( terms1 )
				{
					ItemSetCheck( 0 );
				}
				else if( terms2 )
				{
					ItemSetCheck( 1 );
				}
				else if( terms3 )
				{
					ItemSetCheck( 3 );

				}

				//조합 성공한 것에 하나 추가
				player.AddItemData( itemData.ID, currentCount );

				//결과창 오픈
				resultView.SetActive( true );
				mixView.SetActive( false );

                //내용 초기화
                valueInitialization();
                for ( int i = 0; i < 6; i++ )
				{
					matSlotNumber[ i ] = -1;
				}
				terms1 = false;
				terms2 = false;
				terms3 = false;
			}
			else
			{
				Debug.Log( "재료부족" );
				resultImage.sprite = null;
				resultName.text = null;
				resultCount.text = null;
			}

		}
	}

    void valueInitialization()
    {
        currentCount = 0;
        fabricableCount = 0;
        makeText.text = currentCount.ToString();
        countText.text = "  /  " + fabricableCount.ToString();

        productionItem.sprite = Resources.Load<Sprite>("Image/UI/ItemIcon/none") as Sprite;
        productionName.text = "";

        if (matIsOneAmount.activeSelf)
        {
            matIsOneAmount.SetActive(false);
        }
        else if (matIsTwoAmount.activeSelf)
        {
            matIsTwoAmount.SetActive(false);
        }
        else if (matISThreeAmount.activeSelf)
        {
            matISThreeAmount.SetActive(false);
        }
    }

    void ItemSetCheck( int num )
	{
		for( int z = 0; z < currentCount; z++ )
		{
			int j = 0; //resource Count 0번째부터,

			//갯수가 1개인지 그 이상인지 확인해서 감소. 또는 제거.
			//matSlotNumber[num] <- 인벤토리 번호.
			for( int i = num; i < num + itemData.ResourceIDSet.Length; i++ )
			{
				player.ItemSet[ matSlotNumber[ i ] ].Count -= itemData.ResourceCountSet[ j++ ];

				if( player.ItemSet[ matSlotNumber[ i ] ].Count <= 0 )
				{
					player.ItemSet[ matSlotNumber[ i ] ] = new ItemInstance( );
				}
			}
		}
	}

    public void confirmButton()
	{
		if( resultView.activeSelf )
		{
			resultView.SetActive( false );
		}
	}

	public void MaterialCheck( int j, ItemData id )
	{
		int minItem = 999;
		bool noItem = false;

        //배열자리
		//0 -> 재료가 1개
		//1~2 -> 재료가 2개짜리
		//3~5 -> 재료가 3개짜리
		for( int i = j; i < j + id.ResourceIDSet.Length; i++ )
		{
			//받는 ItemData 정보에서 material 정보를 하나씩 뽑음.
			ItemData temp = mixManager.ID( id.ResourceIDSet[ i - j ] );

			matSlotNumber[ i ] = player.SearchItem( temp, id.ResourceCountSet[ i - j ] ); //SearchItem : PlayerData 에 있음.(재료 아이템이 있는 인벤토리 슬롯번호 반환)
			materials[ i ].sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + temp.File ) as Sprite;
			materialTexts[ i ].text = id.ResourceCountSet[ i - j ].ToString();


			//최대 만들수 있는 수량 체크
			if( ( matSlotNumber[ i ] != -1 ) )
			{
				if( ( player.ItemSet[ matSlotNumber[ i ] ].Count / id.ResourceCountSet[ i - j ] ) <= minItem )
				{       
					minItem = player.ItemSet[ matSlotNumber[ i ] ].Count / id.ResourceCountSet[ i - j ];
				}
			}
			else
			{
				Debug.Log( "ㄴㄴ템없움" );
				noItem = true;
			}
		}

		if( noItem )
		{
			fabricableCount = 0;
		}
		else
		{
			fabricableCount = minItem;
			noItem = false;
		}

		//선택된 아이템 정보
		itemData = id;  
	}


    public void CurrentCountManager()
	{
		makeText.text = currentCount.ToString();
		countText.text = "  /  " + fabricableCount.ToString();

		if( currentCount > fabricableCount )
		{
			currentCount = fabricableCount;
		}
		else if( currentCount < 1 )
		{
			currentCount = 0;
		}
	}

    public void currentPageManager( int page )
	{
		switch( page )
		{
			case 0:
				for( int i = 0; i < 9; i++ )
				{
					mixManager.DataSetting( i, i + 7 );
				}
				mixManager.DataSetting( 9, 18 );
				break;

			case 1:
				for( int i = 0; i < 3; i++ )
				{
					Debug.Log( i );
					mixManager.DataSetting( i, i + 19 );
				}
				for( int i = 3; i < 10; i++ )
				{
					Debug.Log( i );
					mixManager.DataSetting( i, i + 21 );
				}
				break;
			case 2:
				for( int i = 0; i < 10; i++ )
				{
					mixManager.DataSetting( i, i + 31 );
				}
				break;
			case 3:
				for( int i = 0; i < 6; i++ )
				{
					mixManager.DataSetting( i, i + 41 );
				}
				for( int i = 6; i < 10; i++ )
				{
					Debug.Log( i );
					mixManager.DataSetting( i, 0 );
				}
				break;
			default:
				Debug.Log( "오류가 있으니 확인부탁드립니다." );
				break;
		}

	}

	public void NextButton()
	{
		if( currentPage < lastPage )
		{
			currentPage++;
			currentPageManager( currentPage );
		}
	}

	public void PrevButton()
	{
		if( currentPage > 0 )
		{
			currentPage--;
			currentPageManager( currentPage );
		}
	}

	public void Plus1()
	{
		if( currentCount >= fabricableCount )
		{
			currentCount = fabricableCount;
		}
		else
		{
			currentCount += 1;
		}
	}

	public void Minus1()
	{
		if( currentCount <= 0 )
		{
			currentCount = 0;
		}
		else
		{
			currentCount -= 1;
		}
	}

	public void Plus10()
	{
		if( currentCount + 10 >= fabricableCount )
		{
			currentCount = fabricableCount;
		}
		else
		{
			currentCount += 10;
		}
	}

	public void Minus10()
	{
		if( currentCount - 10 <= 0 )
		{
			currentCount = 0;
		}
		else
		{
			currentCount -= 10;
		}
	}
}

/*
<guide>
    public Image[] materials; //재료창에서 이미지 로딩을 위해서
	public Text[] materialTexts; //재료창에서 재료의 이름을 로딩을 위해서
	public int[] matSlotNumber; //player 인벤토리에서 "몇번째"에 재료가 있는지 확인해서 그 인벤토리 번호 넣음.

    제작재료가 1,2,3개로 각자 다양해서, 위 세개의 배열을 전부 길이 6으로 잡고,
    배열의 0번째 자리는 재료 1개일때 검사,
    배열의 1,2번째는 재료 2개일때의 검사,
    배열의 3,4,5번째는 재료 3개일때 검사 및 사용합니다.
 */

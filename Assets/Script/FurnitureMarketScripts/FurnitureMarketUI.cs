using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FurnitureMarketUI : MonoBehaviour
{

	public PlayerData player;

	public GameObject marketList;
	public GameObject purchaseConfirm;

	public GameObject furnitureMould;
	//Resource -> Prefabs -> furnitureMould

	//MarketList ----------- (에 필요한 것들)
	public GameObject[] furnitures;
	public Image[] furnitureImage;
	public Text[] furnitureName;

	public Transform firstPos;
	public Transform lastPos;
	public float interval;

	public int blankNum = 4;
	//칸 갯수
	public bool loading = false;

	public int currentPage = 0;
	public int lastPage = 5;
	//MarketList ------------

	//PurchaseConfirm ----------
	public FurnitureData furnitureData;

	public Image selectImage;
	public Text selectName;
	public Text selectPrice;
	//PurchaseConfirm ----------

	void Start()
	{
		furnitureMarketLoad();
	}

	void Update()
	{
	
	}

	public void LinkComponentElement()
	{
		marketList = GameObject.Find( "MarketList" );
		purchaseConfirm = GameObject.Find( "PurchaseConfirm" );

		firstPos = GameObject.Find( "furnitureFirstPosition" ).GetComponent<Transform>();
		lastPos = GameObject.Find( "furnitureLastPosition" ).GetComponent<Transform>();

		selectImage = GameObject.Find( "selectedFurnitureImage" ).GetComponent<Image>();
		selectName = GameObject.Find( "selectedFurnitureName" ).GetComponent<Text>();
		selectPrice = GameObject.Find( "selectedFurniturePrice" ).GetComponent<Text>();
	}

	public void UpdateComponentElement()
	{

	}

	void furnitureMarketLoad()
	{
		player = GameObject.Find( "GameLogic" ).GetComponent<GameManager>().GamePlayer;

		marketList.SetActive( false );
		purchaseConfirm.SetActive( false );

		interval = lastPos.position.x - firstPos.position.x;

		FurnitureListCreate();

	}

	void FurnitureListCreate()
	{
		furnitures = new GameObject[blankNum];
		furnitureImage = new Image[blankNum];
		furnitureName = new Text[blankNum];

		for( int i = 0; i < blankNum; i++ )
		{
			//오류나면 9번째 줄, 프리팹이 연결되어 있는지 확인하기.
			GameObject temp = Instantiate( furnitureMould, transform.position, Quaternion.identity ) as GameObject;
			temp.transform.SetParent( marketList.transform, false );
			temp.transform.position = firstPos.position + new Vector3( interval * i, 0f, 0f );
			temp.name = "furnitureObject" + i.ToString();

			furnitures[ i ] = temp.transform.GetChild( 0 ).gameObject;
			furnitureImage[ i ] = temp.transform.GetChild( 0 ).gameObject.GetComponent<Image>();
			furnitureName[ i ] = temp.transform.GetChild( 1 ).gameObject.GetComponent<Text>();

		}
	}

	public void TempPopupOpenButton()
	{
		if( !marketList.activeSelf )
			marketList.SetActive( true );

		if( !loading )
		{
			for( int i = 0; i < blankNum; i++ )
			{
				DataSetting( i, i + 1 );
			}
			loading = true;
		}
	}

	void DataSetting( int i, int id )
	{
		if( id != 0 )
		{
			furnitureImage[ i ].sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/" + ID( id ).File ) as Sprite;
			furnitureName[ i ].text = ID( id ).Name;

			selectedFurniture data;
			data = furnitures[ i ].GetComponent<selectedFurniture>();
			data.furniture = ID( id );

			if( loading && !data.btn.isActiveAndEnabled )
			{
				data.btn.enabled = true;
			}

		}
		else
		{
			furnitureImage[ i ].sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/none" ) as Sprite;
			furnitureName[ i ].text = "";

			selectedFurniture data;
			data = furnitures[ i ].GetComponent<selectedFurniture>();
			data.furniture = null;

            
			data.btn.enabled = false;
            
		}
	}

	void ListPageManager( int page )
	{
		switch( page )
		{
			case 0:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 1 );
				}
				break;

			case 1:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 5 );
				}
				break;
			case 2:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 9 );
				}
				break;
			case 3:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 13 );
				}
				break;
			case 4:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 17 );
				}
				break;
			case 5:
				for( int i = 0; i < 4; i++ )
				{
					DataSetting( i, i + 21 );
				}
				break;
			default:
				Debug.Log( "오류가 있으니 확인부탁드립니다." );
				break;
		}
	}


	//Buttons

	public void MarketListClose()
	{
		if( marketList.activeSelf )
			marketList.SetActive( false );
	}

	public void PurchasePopup( FurnitureData data )
	{
		if( !purchaseConfirm.activeSelf )
			purchaseConfirm.SetActive( true );

		furnitureData = data;

		selectImage.sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/" + data.File ) as Sprite;
		selectName.text = data.Name;
		//selectPrice.text = data.Price;
		Debug.Log( "돈 표시 넣어야함" );
	}

	public void Purchase()
	{
		PurchaseClose();
		MarketListClose();


		//소지금 마이너스
		//└─▶아직없음
		Debug.Log( "플레이어 돈 줄어드는 것 추가 할 부분" );


		//PlayerData -> Furniture -> 아이템넣기
		player.AddFurnitureData( furnitureData );
	}

	public void PurchaseClose()
	{
		if( purchaseConfirm.activeSelf )
			purchaseConfirm.SetActive( false );
	}


	public FurnitureData ID( int id )
	{
		FurnitureData data = DataManager.FindFurnitureDataByID( id );
		return data;
	}

	public void NextButton()
	{
		if( currentPage < lastPage )
		{
			currentPage++;
			ListPageManager( currentPage );
		}
	}

	public void PrevButton()
	{
		if( currentPage > 0 )
		{
			currentPage--;
			ListPageManager( currentPage );
		}
	}
}

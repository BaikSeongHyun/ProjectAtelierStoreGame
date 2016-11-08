using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

	public GameObject shop;

	public Image[] shopList;
	public Text[] text;

	//ShopList에 필요한 것들
	public GameObject shopContent;
	//awake
	public Transform contentStart;
	//awake
	public Transform contentEnd;
	//awake
	public float interval;
	//start
	public GameObject mould;
	//Resource ->Prefabs -> shopMould

	public int num = 15;

	void Awake()
	{
		shopContent = GameObject.Find( "ShopContent" );
		contentStart = GameObject.Find( "ShopStartPosition" ).GetComponent<Transform>();
		contentEnd = GameObject.Find( "ShopEndPosition" ).GetComponent<Transform>();
	}

	void Start()
	{
		shop = GameObject.Find( "Shop" );
		shop.SetActive( false );

		interval = contentEnd.position.x - contentStart.position.x;
	}

	void Update()
	{

	}

	void 연습용()
	{
		text = new Text[num];

		for( int i = 0; i < num; i++ )
		{
			GameObject temp = Instantiate( mould, transform.position, Quaternion.identity ) as GameObject;
			temp.transform.SetParent( shopContent.transform, false );
			temp.transform.position = contentStart.position + new Vector3( interval * i, 0f, 0f );
			temp.name = "shopList" + i.ToString();

			text[ i ] = temp.transform.GetChild( 0 ).GetComponent<Text>();
		}
		Setting();
	}


	public void ShopBtn()
	{
		if( shop.activeSelf )
		{
			shop.SetActive( false );
		}
		else
		{
			shop.SetActive( true );
			연습용();
			shopImageLoad();
		}

	}

	void Setting()
	{
		shopList = new Image[num];
		for( int i = 0; i < num; i++ )
		{
			shopList[ i ] = GameObject.Find( "shopList" + i.ToString() ).GetComponent<Image>();
		}

		RectTransform content = shopContent.GetComponent<RectTransform>();
		content.sizeDelta = new Vector2( 201f * num, content.sizeDelta.y );

		Debug.Log( content.sizeDelta );

	}

	void shopImageLoad()
	{
		shopList[ 0 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 1 ).File ) as Sprite;
		text[ 0 ].text = ID( 1 ).Name;
		shopList[ 1 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 7 ).File ) as Sprite;
		text[ 1 ].text = ID( 7 ).Name;
		shopList[ 2 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 10 ).File ) as Sprite;
		text[ 2 ].text = ID( 10 ).Name;
		shopList[ 3 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 17 ).File ) as Sprite;
		text[ 3 ].text = ID( 17 ).Name;
		shopList[ 4 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 20 ).File ) as Sprite;
		text[ 4 ].text = ID( 20 ).Name;
		shopList[ 5 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 2 ).File ) as Sprite;
		text[ 5 ].text = ID( 2 ).Name;
		shopList[ 6 ].sprite = Resources.Load<Sprite>( "Image/Shop/" + ID( 3 ).File ) as Sprite;
		text[ 6 ].text = ID( 3 ).Name;
	}

	FurnitureData ID( int _id )
	{
		// id reading in XML => Item Data All Load
		FurnitureData fd = DataManager.FindFurnitureDataByID( _id );        
		return fd;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectOnField : MonoBehaviour
{

	// hight structure
	[SerializeField] GameManager manager;
	[SerializeField] FieldManager fieldManager;

	//상위
	public GameObject body;

	//Item List 에서 갖고있는 아이템이름
	public int id;
	public int count;
	public int min, max;
	public float time;
	public float clickTime;
	public bool click;

	public GameObject cloud;
	public Image cloudBackground;
	public int position = 0;


	void Awake()
	{
		manager = GameObject.Find( "GameLogic" ).GetComponent<GameManager>();
		fieldManager = manager.GetComponent<FieldManager>();

		count = Random.Range( min, max );
		time = Random.Range( 3f, 10f );

		cloud = transform.Find( "cloud" ).gameObject;
		cloud.SetActive( false );

		cloudBackground = transform.Find( "cloudBackground" ).GetComponent<Image>();

		StartCoroutine( "CollectTime" );

		clickTime = 0;
	}

	void Update()
	{
		transform.forward = -Camera.main.transform.forward;

		if( click )
		{
			ClickCloud();
		}

		if( manager.PresentMode == GameManager.GameMode.Store )
		{
			cloud.SetActive( true );
		}
		else
		{
			cloud.SetActive( false );
		}
	}


	IEnumerator CollectTime()
	{
		yield return new WaitForSeconds( time );
		cloud.SetActive( true );
	}

	public void CloudCooltime()
	{
		if( !click )
		{
			clickTime = Time.time;
		}
		click = true;
	}

	void ClickCloud()
	{
		cloudBackground.fillAmount = ( Time.time - clickTime ) / time;

		if( cloudBackground.fillAmount >= 1 )
		{
			manager.GamePlayer.AddItemData( id, count );
			manager.GamePlayer.StoreData.AddExperience( 100 );
			fieldManager.stepLocation[ position ] = false;
			fieldManager.currentObjectNumber--;
			Destroy( body );
		}
	}
}

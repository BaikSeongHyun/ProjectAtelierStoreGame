using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectOnField : MonoBehaviour
{
    //이 스크립트는 오브젝트 프리팹[4종류] 각각이 가지고 있다.
	[SerializeField] PlayerOnField player; //My character
	[SerializeField] SendData sendData; //field-> [data send] ->store
	public ObjectDataBuffer databf; // object -> [data send] -> temp data save script

	public Renderer rend; //change color
    public Color myColor; // myColor -> gray -> myColor
    public bool objectOnMouse = false;
	public bool getObject = false;

	//Item List 에서 갖고있는 아이템이름
	public int id;
	public int count;
	public int min, max;

	public float cooltime;

	void Start()
	{
		player = GameObject.Find( "Player" ).GetComponent<PlayerOnField>();
		databf = GameObject.Find( "ObjectManager" ).GetComponent<ObjectDataBuffer>();
		sendData = GameObject.Find( "Data" ).GetComponent<SendData>();

		rend = GetComponent<Renderer>();
		myColor = rend.material.color;

		count = Random.Range( min, max );
	}

	void Update()
	{
		GetMaterials();
	}

	void OnMouseEnter()
	{
		if( rend.material.color != null )
		{
			rend.material.color = Color.gray;
			objectOnMouse = true;
		}
	}

	void OnMouseExit()
	{
		rend.material.color = myColor;
		objectOnMouse = false;
	}

	void GetMaterials()
	{
		if( objectOnMouse )
		{
			if( Input.GetMouseButtonDown( 0 ) )
			{
				Debug.Log( this.name + " 오브젝트를 클릭했다." );
				getObject = true;
			}
		}

		if( getObject )
		{
			if( player.frontOfObject )
			{
				Debug.Log( "플레이어가 물체앞" );
				StartCoroutine( "StartAction" );
				getObject = false;
				player.frontOfObject = false;
			}
		}
	}

	public IEnumerator StartAction()
	{
		databf.receiveData = true;
		databf.cooltime = cooltime;

		yield return new WaitForSeconds( cooltime );

        //임시 데이터 저장소에 차곡차곡 저장시킨다.
		sendData.data.Add( DataManager.FindItemDataByID( id ) );
		sendData.count.Add( count );

		ObjectRegeneration.curObjects--;
		Destroy( this.gameObject );
	}
}

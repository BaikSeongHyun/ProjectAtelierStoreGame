using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemObject : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	
	// field - data
	[SerializeField] ItemInstance data;
	
	// field - component
	[SerializeField] GameObject infoBubble;
	[SerializeField] Image itemImage;
	[SerializeField] Text itemCount;

	// property
	public ItemInstance Instance { get { return data; } }
	
	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	void Update()
	{
		infoBubble.transform.forward = -Camera.main.transform.forward;
		infoBubble.transform.position = transform.position + new Vector3( 0f, 1.5f, 0f );
	}
	
	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// set Info Bubble
		infoBubble = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/ItemObject/InfoBubble" ),
		                                         transform.position + new Vector3( 0f, 1.5f, 0f ),
		                                         Quaternion.identity );
		infoBubble.transform.SetParent( this.transform );
		itemImage = infoBubble.transform.Find( "ItemImage" ).GetComponent<Image>();
		itemCount = infoBubble.transform.Find( "CountText" ).GetComponent<Text>();
	}

	// set component data
	public void SetComponentElement()
	{
		if( ( data == null ) || ( data.Count <= 0 ) )
			Destroy( this.gameObject );

		itemImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + data.Item.File );
		itemCount.text = "x " + data.Count;
	}

	// data setting
	public void SetItemInstanceData( ItemInstance instanceData )
	{
		data = instanceData;
		SetComponentElement();
	}
}

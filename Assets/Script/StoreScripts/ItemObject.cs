using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemObject : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	
	// field - data
	[SerializeField] ItemInstance data;
	[SerializeField] bool isField;
	
	// field - component
	[SerializeField] Rigidbody rigidbody;
	[SerializeField] GameObject infoBubble;
	[SerializeField] Image bubbleImage;
	[SerializeField] Image itemImage;
	[SerializeField] Text itemCount;

	// property
	public ItemInstance Instance { get { return data; } }

	public bool IsField { get { return isField; } set { isField = value; } }
	
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
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// data 
		isField = false;

		// set Info Bubble
		infoBubble = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/ItemObject/InfoBubble" ),
		                                         transform.position + new Vector3( 0f, 1.5f, 0f ),
		                                         Quaternion.identity );
		rigidbody = GetComponent<Rigidbody>();
		infoBubble.transform.SetParent( this.transform );
		bubbleImage = infoBubble.transform.Find( "BubbleImage" ).GetComponent<Image>();
		itemImage = infoBubble.transform.Find( "ItemImage" ).GetComponent<Image>();
		itemCount = infoBubble.transform.Find( "CountText" ).GetComponent<Text>();
	}

	// set component data
	public void SetComponentElement()
	{
		if( ( data == null ) || ( data.Count <= 0 ) )
		{
			Destroy( this.gameObject );
		}

		itemImage.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + data.Item.File );
		itemCount.text = "x " + data.Count;
	}

	// data setting
	public void SetItemInstanceData( ItemInstance instanceData )
	{
		data = instanceData;
		SetComponentElement();
	}

	// destroy game object
	public void DestroyGameObject()
	{
		Destroy( this.gameObject );
	}

	// throwing item
	public void ThrowItem()
	{
		// set force direction
		Vector3 force = new Vector3( Random.Range( -1, 2 ), 1f, Random.Range( -1, 2 ) ) * 200f;

		bubbleImage.sprite = Resources.Load<Sprite>( "StoreObject/ItemObject/ItemObjectRoot/InfoBubbleThrow" );

		// throw!
		rigidbody.AddForce( force );
		isField = true;
	}

	// acquire items
	public void AcquireItems()
	{
		manager.GamePlayer.AddItemData( data.Item.ID, data.Count );
		Destroy( this.gameObject );
	}
}

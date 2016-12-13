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
	[SerializeField] Image itemImage;

	// property
	public ItemInstance Instance { get { return data; } }
	
	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	void Update()
	{
		itemImage.transform.forward = -Camera.main.transform.forward;
	}
	
	// public method
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		
		itemImage = transform.Find( "InfoBubble" ).GetComponent<Image>();		
	}

	// data setting
	public void SetItemInstanceData( ItemInstance instanceData )
	{
		data = instanceData;
	}
}

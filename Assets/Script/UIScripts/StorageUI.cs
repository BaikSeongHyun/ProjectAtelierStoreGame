using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StorageUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;

	// component element
	[SerializeField] Image tap1;
	[SerializeField] Image tap2;
	[SerializeField] Image tap3;
	[SerializeField] DataElement[] slots;

	// field - image data
	[SerializeField] Sprite tap1On;
	[SerializeField] Sprite tap1Off;
	[SerializeField] Sprite tap2On;
	[SerializeField] Sprite tap2Off;
	[SerializeField] Sprite tap3On;
	[SerializeField] Sprite tap3Off;

	// field - logic data
	[SerializeField] int presentStepIndex;

	// property
	public int PresentStepIndex { get { return presentStepIndex; } }

	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();	
		LinkComponentElement();
	}

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();	
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();	 

		tap1 = transform.Find( "TapStep1Button" ).GetComponent<Image>();
		tap2 = transform.Find( "TapStep2Button" ).GetComponent<Image>();
		tap3 = transform.Find( "TapStep3Button" ).GetComponent<Image>();
		slots = GetComponentsInChildren<DataElement>();
		foreach ( DataElement element in slots )
			element.LinkComponentElement();

		presentStepIndex = 0;
	}

	// update component element
	public void UpdateComponentElement()
	{
//		for( int i = 0; i < slots.Length; i++ )
//		{
//			try
//			{
//				if( this.gameObject.name == "StorageUI" )
//					slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i ] );
//				else if( this.gameObject.name == "FurnitureSetUI" )
//					slots[ i ].UpdateComponentElement( manager.GamePlayer.FurnitureSet[ i ] );
//			}
//			catch( IndexOutOfRangeException e )
//			{
//				Debug.Log( e.StackTrace );
//				Debug.Log( e.Message );
//				slots[ i ].UpdateComponentElement();
//			}
//		}
		if ( this.gameObject.name == "StorageUI" )
		{
			for ( int i = 0; i < slots.Length; i++ )
			{
				slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i + (presentStepIndex * (manager.GamePlayer.ItemSet.Length / 3)) ] );
			}
		}
		else if ( this.gameObject.name == "FurnitureSetUI" )
		{
			for ( int i = 0; i < slots.Length; i++ )
			{
				slots[ i ].UpdateComponentElement( manager.GamePlayer.FurnitureSet[ i + (presentStepIndex * (manager.GamePlayer.FurnitureSet.Length / 3)) ] );
			}
		}
	}

	// on click method
	// on click tab process
	public void OnClickStepTapProcess( int index )
	{
		// set button color & present see step change

		switch ( index )
		{
			case 1:	
				tap1.sprite = tap1On;
				tap2.sprite = tap2Off;
				tap3.sprite = tap3Off;
				presentStepIndex = 0;
				break;
			case 2:
				tap1.sprite = tap1Off;
				tap2.sprite = tap2On;
				tap3.sprite = tap3Off;
				presentStepIndex = 1;
				break;
			case 3:
				tap1.sprite = tap1Off;
				tap2.sprite = tap2Off;
				tap3.sprite = tap3On;
				presentStepIndex = 2;
				break;
		}
	}

	// on click item inventory element
	public void OnClickItemStorageElement( int index )
	{
		// view item information popup
	}

	public void OnCilckExitStorageUI()
	{
		this.gameObject.SetActive( false );
	}
}
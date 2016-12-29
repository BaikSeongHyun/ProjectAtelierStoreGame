using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StorageUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
    [SerializeField] SoundManager soundManager;

	// component element
	[SerializeField] Image tap1;
	[SerializeField] Image tap2;
	[SerializeField] Image tap3;
	[SerializeField] DataElement[] slots;

	// field - logic data
	[SerializeField] int presentStepIndex;

	// property
	public int PresentStepIndex { get { return presentStepIndex; } }

	void Awake()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
        soundManager = GameObject.FindWithTag("GameLogic").GetComponent<SoundManager>();
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
		foreach( DataElement element in slots )
			element.LinkComponentElement();

		presentStepIndex = 0;
	}

	// update component element
	public void UpdateComponentElement()
	{
		if( this.gameObject.name == "StorageUI" )
		{
			for( int i = 0; i < slots.Length; i++ )
			{
				slots[ i ].UpdateComponentElement( manager.GamePlayer.ItemSet[ i + ( presentStepIndex * ( manager.GamePlayer.ItemSet.Length / 3 ) ) ] );
			}
			storeManager.StorageIndex = presentStepIndex;
		}
		else if( this.gameObject.name == "FurnitureSetUI" )
		{
			for( int i = 0; i < slots.Length; i++ )
			{
				slots[ i ].UpdateComponentElement( manager.GamePlayer.FurnitureSet[ i + ( presentStepIndex * ( manager.GamePlayer.FurnitureSet.Length / 3 ) ) ] );
			}
		}
	}

	// on click method
	// on click tab process
	public void OnClickStepTapProcess( int index )
	{
        soundManager.PlayUISoundPlayer(4);
        // set button color & present see step change
        switch ( index )
		{
			case 1:	
				tap1.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab1On" );
				tap2.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab2Off" );
				tap3.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab3Off" );
				presentStepIndex = 0;
				break;
			case 2:
				tap1.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab1Off" );
				tap2.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab2On" );
				tap3.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab3Off" );
				presentStepIndex = 1;
				break;
			case 3:
				tap1.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab1Off" );
				tap2.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab2Off" );
				tap3.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/Tab3On" );
				presentStepIndex = 2;
				break;
		}
	}

	// on click item inventory element
	public void OnClickItemStorageElement( int index )
	{
        soundManager.PlayUISoundPlayer(4);
		// furniture item view
		if( this.gameObject.name == "FurnitureSetUI" )
			storeManager.AllocateStartFurnitureInstance( index, presentStepIndex );

		// view item information popup

	}

	public void OnCilckExitStorageUI()
	{
		this.gameObject.SetActive( false );
        soundManager.PlayUISoundPlayer(4);
	}
}
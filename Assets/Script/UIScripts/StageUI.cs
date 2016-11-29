using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StageManager stageManager;

	// component element
	[SerializeField] Image timeBar;
	[SerializeField] Image stageStateButton;

	// property

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component
	public void LinkComponentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();

		// component element
//		timeBar = transform.Find( "TimeBar" ).GetComponent<Image>();
		stageStateButton = transform.Find( "StageStateButton" ).GetComponent<Image>();			
	}

	// call event has occured
	public void SetComponentElement()
	{
		if( manager.PresentMode == GameManager.GameMode.StoreOpenPreprocess )
			stageStateButton.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/StoreOff" );
	}

	// call per on frame
	public void UpdateComponentElement()
	{
		// set TimeBar
	}

	// on click method
	public void OnClickStoreOpen()
	{
		if( manager.PresentMode == GameManager.GameMode.StoreOpenPreprocess )
		{
			stageStateButton.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/StoreOn" );
			manager.SetStoreOpenMode();
		}
	}

}

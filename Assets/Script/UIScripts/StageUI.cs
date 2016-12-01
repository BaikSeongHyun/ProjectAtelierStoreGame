using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StageManager stageManager;

	// field logic data
	[SerializeField] bool isRight;

	// component element
	[SerializeField] Image timeBarIcon;
	[SerializeField] Image timeBarFill;
	[SerializeField] Image characterImage;
	[SerializeField] Text goldText;
	[SerializeField] Text stepText;
	[SerializeField] Text nameText;
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
		timeBarIcon = transform.Find( "TimeBar" ).Find( "TimeBarIcon" ).GetComponent<Image>();
		timeBarFill = transform.Find( "TimeBar" ).Find( "TimeBarFill" ).GetComponent<Image>();
		goldText = transform.Find( "Gold" ).Find( "GoldText" ).GetComponent<Text>();
		stepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();
		//stepText = transform.Find( "PlayerStatus" ).Find( "NameText" ).GetComponent<Text>();
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
		// set text
		goldText.text = manager.GamePlayer.Gold.ToString();
		stepText.text = manager.GamePlayer.StoreData.StoreStep.ToString();
//		nameText.text = manager.GamePlayer.Name;

		// set character image

	}

	public void ResetComponent()
	{
		timeBarFill.fillAmount = 1;
		timeBarFill.enabled = true;
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


	// coroutine section
	// ui activate drive
	public IEnumerator ActivateDrive()
	{
		while( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			// set TimeBar
			timeBarFill.fillAmount = stageManager.TimeFill;

			// flash time bar fill
			if( stageManager.FreeTime <= 10f )
				timeBarFill.enabled = !timeBarFill.enabled;

			yield return new WaitForSeconds( 0.1f );
		}
	}

}

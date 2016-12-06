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
	[SerializeField] Image charHead;
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
		charHead = transform.Find( "PlayerStatus" ).Find( "CharHead" ).GetComponent<Image>();
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

		// set character image
		if( manager.GamePlayer.CharacterType == "Berry" )
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/BerryHead" );
		else
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/ChouHead" );
	}

	public void ResetComponent()
	{
		stageStateButton.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/StoreOff" );
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

	public void OnClickGoBack()
	{
		if( manager.PresentMode == GameManager.GameMode.StoreOpenPreprocess )
		{			
			ResetComponent();

			// set items reset
			stageManager.StagePreprocessReturn();
		}
		else if( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			// time short 
			ResetComponent();
			// return result

			// set item reset
			stageManager.StageProcessReturn();
		}
	}


	// coroutine section
	// ui activate drive
	public IEnumerator ActivateDrive()
	{
		stageStateButton.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/StoreOn" );
		timeBarFill.enabled = true;
		timeBarFill.fillAmount = 1;

		yield return new WaitForSeconds( 1f );
	
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

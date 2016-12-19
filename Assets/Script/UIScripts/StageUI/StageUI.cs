using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StageUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StageManager stageManager;
	[SerializeField] CharacterManager charManager;

	// field logic data
	[SerializeField] bool isRight;

	// component element - normal items
	[SerializeField] Image timeBarFill;
	[SerializeField] Image charHead;
	[SerializeField] Text goldText;
	[SerializeField] Text stepText;
	[SerializeField] Text nameText;
	[SerializeField] Image stageStateButton;

	// component element - log element
	[SerializeField] LogElement[] logData;

	// component element - kakao information
	[SerializeField] GameObject kakaoInformation;
	[SerializeField] Text probabilityGroupText;
	[SerializeField] Text probabilityScaleText;
	[SerializeField] Text groupText;
	[SerializeField] Text scaleText;

	// low structure - use marshmello pinky
	[SerializeField] PinkyActivateUI pinkActivateUI;

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
		charManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();

		// component element - normal
		timeBarFill = transform.Find( "TimeBar" ).Find( "TimeBarFill" ).GetComponent<Image>();
		nameText = transform.Find( "PlayerStatus" ).Find( "NameText" ).GetComponent<Text>();
		goldText = transform.Find( "Gold" ).Find( "GoldText" ).GetComponent<Text>();
		stepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();
		charHead = transform.Find( "PlayerStatus" ).Find( "CharHead" ).GetComponent<Image>();
		stageStateButton = transform.Find( "StageStateButton" ).GetComponent<Image>();	

		// component element - log element
		logData = GetComponentsInChildren<LogElement>();

		// component element - kakao information
		kakaoInformation = transform.Find( "KakaoInformation" ).gameObject;
		probabilityGroupText = kakaoInformation.transform.Find( "ProbabilityGroup" ).GetComponent<Text>();
		probabilityScaleText = kakaoInformation.transform.Find( "ProbabilityScale" ).GetComponent<Text>();
		groupText = kakaoInformation.transform.Find( "GroupText" ).GetComponent<Text>();
		scaleText = kakaoInformation.transform.Find( "ScaleText" ).GetComponent<Text>();

		kakaoInformation.SetActive( false );

	}

	// call event has occured
	public void SetComponentElement()
	{
		// set text
		nameText.text = manager.GamePlayer.Name;
		goldText.text = manager.GamePlayer.Gold.ToString();
		stepText.text = manager.GamePlayer.StoreData.StoreStep.ToString();

		// set character image
		if( manager.GamePlayer.CharacterType == "Berry" )
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/BerryHead" );
		else
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/ChouHead" );

		if( manager.PresentMode == GameManager.GameMode.StoreOpenPreprocess )
			stageStateButton.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/StoreOff" );

		// log data
		for( int i = 0; i < logData.Length; i++ )
		{
			try
			{
				logData[ i ].UpdateComponentElement( stageManager.SellItem[ i ], stageManager.SellGold[ i ] );
			}
			catch( ArgumentOutOfRangeException e )
			{
				// no data in list
			}
		}

		// kakao information
		if( charManager.MrKakao.IsSendMessage )
		{
			kakaoInformation.SetActive( true );
			probabilityGroupText.text = stageManager.ProFavor.ToString() + "%";
			probabilityScaleText.text = stageManager.ProScale.ToString() + "%";
			groupText.text = ItemData.ReturnTypeString( stageManager.FavoriteGroup );
			scaleText.text = CustomerAgent.ReturnBuyScaleString( stageManager.BuyScale );
		}
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

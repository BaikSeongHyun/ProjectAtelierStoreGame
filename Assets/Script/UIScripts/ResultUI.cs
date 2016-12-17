using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultUI : MonoBehaviour
{
	// high structure
	[SerializeField] StageManager stageManager;
	[SerializeField] UIManager mainUI;

	// field - component
	[SerializeField] Image rankImage;
	[SerializeField] Text goldText;
	[SerializeField] Text expText;
	[SerializeField] Text playTimeText;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		// high structure
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// field  - component
		rankImage = transform.Find( "RankImage" ).GetComponent<Image>();
		goldText = transform.Find( "GoldReward" ).Find( "GoldText" ).GetComponent<Text>();
		expText = transform.Find( "ExpReward" ).Find( "ExpText" ).GetComponent<Text>();
		playTimeText = transform.Find( "PlayTime" ).Find( "PlayTimeText" ).GetComponent<Text>();
	}

	// set element
	public void SetComponentElement()
	{
		rankImage.sprite = Resources.Load<Sprite>( "Image/UI/ResultUI/Rank" + stageManager.ResultData.Rank );
		goldText.text = stageManager.ResultData.RewardGold.ToString();
		expText.text = stageManager.ResultData.RewardExp.ToString();

		// set minute
		playTimeText.text = ( ( int ) ( stageManager.PlayTime / 60 ) ).ToString() + " : ";

		// set second
		if( ( stageManager.PlayTime % 60 ) == 0 )
			playTimeText.text += "00";
		else
			playTimeText.text += ( ( int ) ( stageManager.PlayTime % 60 ) ).ToString();
	}

	// on click method
	// check information & reward window open
	public void OnClickConfirmResult()
	{
		mainUI.ActivateResultRewardUI();
		this.gameObject.SetActive( false );
	}
}

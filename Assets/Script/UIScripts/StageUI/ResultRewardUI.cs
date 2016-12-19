using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultRewardUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StageManager stageManager;

	// component element
	[SerializeField] Text timeText;
	[SerializeField] Text touchCountText;
	[SerializeField] CardElement[] cardSet;

	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();

		cardSet = GetComponentsInChildren<CardElement>();
	}

	// update component element
	public void UpdateComponentElement()
	{
		timeText.text = "선택하세요";

		touchCountText.text = "횟수: " + stageManager.TouchCount.ToString();
	}

	// reset card
	public void ResetCard()
	{
		foreach( CardElement element in cardSet )
		{
			element.UpdateComponentElement();
		}

		stageManager.ResetResultRewardData();
	}


	// on click method
	// on click card
	public void OnClickDataCard( int index )
	{
		if( stageManager.TouchCount > 0 && !stageManager.IsOpened[ index ] )
		{
			int itemID = 0;
			int itemCount = 1;
			int rare = 0;
			stageManager.SelectCard( index, out itemID, out itemCount, out rare );
			cardSet[ index ].UpdateComponentElement( itemID, itemCount, rare );
		}
		else
		{
			return;
		}
	}

	// exit field
	public void OnClickExitField()
	{
		manager.SetStoreMode();
		manager.SoundManager.PlayUISoundPlayer( 12 );
	}
}

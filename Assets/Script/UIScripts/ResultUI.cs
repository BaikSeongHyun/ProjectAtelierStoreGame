using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StageManager stageManager;

	// component element
	[SerializeField] Text timeText;
	[SerializeField] Text touchCountText;
	[SerializeField] Image[] cardSet;
	[SerializeField] Sprite cardRear;
	[SerializeField] Sprite[] itemSet;

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
		foreach ( Image element in cardSet )
		{
			element.sprite = cardRear;
		}

		stageManager.ResetData();
	}


	// on click method
	// on click card
	public void OnClickDataCard( int index )
	{
		if ( stageManager.TouchCount > 0 && !stageManager.IsOpened[ index ] )
		{
			int cardIndex;
			stageManager.SelectCard( index, out cardIndex );

			cardSet[ index ].sprite = itemSet[ cardIndex ];
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
	}
}

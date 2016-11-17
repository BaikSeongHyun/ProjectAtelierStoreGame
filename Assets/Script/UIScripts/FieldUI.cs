using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FieldUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] FieldManager fieldManager;

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
		fieldManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<FieldManager>();
	}

	// update component element
	public void UpdateComponentElement()
	{
		if( fieldManager.CheckActivateUseTime() )
			ResetCard();
		
		if( ( fieldManager.CalculateWaitingTime() >= 0f ) && ( fieldManager.TouchCount >= 0 ) )
		{			
			timeText.text = fieldManager.WaitingTimeForm();
		}
		else
			timeText.text = "선택하세요";

		touchCountText.text = "횟수: " + fieldManager.TouchCount.ToString();
	}

	// reset card
	public void ResetCard()
	{
		if( fieldManager.CheckActivateUseTime() )
		{
			foreach( Image element in cardSet )
			{
				element.sprite = cardRear;
			}

			fieldManager.ResetData();
		}
	}


	// on click method
	// on click card
	public void OnClickDataCard( int index )
	{
		if( fieldManager.TouchCount > 0 && !fieldManager.IsOpened[ index ] )
		{
			int cardIndex;
			fieldManager.SelectCard( index, out cardIndex );

			cardSet[ index ].sprite = itemSet[ cardIndex ];
		}
		else
		{
			return;
		}
	}

	public void OnClickExitField()
	{

	}
}

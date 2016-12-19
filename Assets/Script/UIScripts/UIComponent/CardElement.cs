using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CardElement : MonoBehaviour
{
	// field - component element
	[SerializeField] Image cardImage;
	[SerializeField] Image itemIcon;
	[SerializeField] Text countText;

	// unity method
	// awake
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		cardImage = GetComponent<Image>();
		itemIcon = transform.Find( "ItemIcon" ).GetComponent<Image>();
		countText = transform.Find( "CountText" ).GetComponent<Text>();

		// set default
		UpdateComponentElement();
	}

	// set default
	public void UpdateComponentElement()
	{
		cardImage.sprite = Resources.Load<Sprite>( "Image/UI/ResultUI/ResultRewardCardBack" );
		itemIcon.enabled = false;
		countText.enabled = false;
	}

	// Update component element
	public void UpdateComponentElement( int id, int count, int rare )
	{
		// card image
		switch( rare )
		{
			case 1:
				cardImage.sprite = Resources.Load<Sprite>( "Image/UI/ResultUI/ResultCardNormal" );
				break;
			case 2:
				cardImage.sprite = Resources.Load<Sprite>( "Image/UI/ResultUI/ResultCardHigh" );
				break;
			case 3:
				cardImage.sprite = Resources.Load<Sprite>( "Image/UI/ResultUI/ResultCardRare" );
				break;
		}

		// item icon setting
		itemIcon.enabled = true;
		itemIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + DataManager.FindItemDataByID( id ).File );

		// count setting
		countText.enabled = true;
		countText.text = "x" + count;
				
	}
}

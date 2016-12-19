using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogElement : MonoBehaviour
{
	// field data
	[SerializeField] Image itemIcon;
	[SerializeField] Image goldImage;
	[SerializeField] Text goldText;

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
		// field data
		itemIcon = transform.Find( "ItemImage" ).GetComponent<Image>();
		goldImage = transform.Find( "GoldImage" ).GetComponent<Image>();
		goldText = transform.Find( "GoldText" ).GetComponent<Text>();

		// set default 
		UpdateComponentElement();
	}

	// update component element -> no data
	public void UpdateComponentElement()
	{
		// shutdown component
		itemIcon.enabled = false;
		goldImage.enabled = false;
		goldText.enabled = false;
	}

	// update component element -> all data
	public void UpdateComponentElement( int id, int gold )
	{
		// activate component
		itemIcon.enabled = true;
		goldImage.enabled = true;
		goldText.enabled = true;

		// data setting
		itemIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + ( DataManager.FindItemDataByID( id ).File ) );
		goldText.text = gold.ToString();
	}



}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryElement : MonoBehaviour
{
	// component element
	[SerializeField] Image elementIcon;
	[SerializeField] Text count;

	// public method
	// link element
	public void LinkComponentElement()
	{
		elementIcon = GetComponent<Image>();
		count = GetComponentInChildren<Text>();
	}

	public void UpdateComponentElement()
	{
		// set image default
		Debug.Log( "Update instance element : default" );

		// count off
		count.enabled = false;
	}

	// update component element -> use item instance
	public void UpdateComponentElement( ItemInstance data )
	{
		// set default
		if( ( data.Item == null ) || ( data.Item.ID == 0 ) )
		{
			// set image default
			elementIcon.sprite = Resources.Load<Sprite>( "Image/ItemIcon/ItemDefault" );

			// count off
			count.enabled = false;
		}
		else
		{
			// set image use icon
			elementIcon.sprite = Resources.Load<Sprite>( "Image/ItemIcon/Item" + data.Item.ID );

			// count on -> renew count
			count.enabled = true;
			count.text = data.Count.ToString();

			// set count color
			if( data.Count == data.Item.CountLimit )
			{
				count.color = Color.red;
			}
			else
				count.color = Color.black;
		}
	}

	// update component element -> use furniture instance
	public void UpdateComponentElement( FurnitureInstance data )
	{
		if( count.enabled )
			count.enabled = false;

		// set default
		if( data.Furniture == null )
		{
			
		}
		// set image icon
		else
		{

		}
	}
}

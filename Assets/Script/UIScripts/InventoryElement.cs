using UnityEngine;
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

	public void UpdateComponentElement( ItemInstance data )
	{
		// set default
		if( data == null )
		{
			// set image default

			// count off
			count.enabled = false;
		}
		else
		{
			// set image use icon

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

	public void UpdateComponentElement( FurnitureInstance data )
	{
		// furniture type don't use count text
		count.enabled = false;

		// set default
		if( data == null )
			;
		// set image icon
		else
			;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataElement : MonoBehaviour
{
	// component element
	[SerializeField] Image elementIcon;
	[SerializeField] Text count;
	[SerializeField] Button button;
	[SerializeField] bool isLocked;

	// property
	public Text Count { get { return count; } set { count = value; } }

	public Image ElementIcon { get { return elementIcon; } set { elementIcon = value; } }

	public bool IsLocked { get { return isLocked; } set { isLocked = value; } }

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link element
	public void LinkComponentElement()
	{
		button = GetComponent<Button>();

		elementIcon = transform.Find( "ElementIcon" ).GetComponent<Image>();
		count = GetComponentInChildren<Text>();
		isLocked = false;
	}

	public void UpdateComponentElement()
	{
		// set image default
		elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/none" );

		// count off
		count.enabled = false;
	}

	// update component element -> use furniture data
	public void UpdateComponentElement( FurnitureData data )
	{
		count.enabled = false;

		if( ( data == null ) || ( data.ID == 0 ) )
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/none" );
		else
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/" + data.File );
	}

	// update component element -> use item data
	public void UpdateComponentElement( ItemData data )
	{
		count.enabled = false;

		if( ( data == null ) || ( data.ID == 0 ) )
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );
		else
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + data.File );
	}


	// update component element -> use item instance
	public void UpdateComponentElement( ItemInstance data )
	{
		// set default

		if( ( data.Item == null ) || ( data.Item.ID == 0 ) )
		{
			// set image default
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );

			// count off
			count.enabled = false;
		}
		else
		{
			// set image use icon
			//elementIcon.sprite = Resources.Load<Sprite>( "Image/ItemIcon/Item" + data.Item.ID );
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + data.Item.File );

			// count on -> renew count
			count.enabled = true;
			count.text = data.Count.ToString();

			// set count color
			if( data.Count == data.Item.CountLimit )
			{
				count.color = Color.red;
			}
			else
				count.color = Color.white;
		}
	}

	// update component element -> use resource item instance
	public void UpdateComponentElement( ItemInstance data, int resourceCount )
	{
		// set default

		if( ( data.Item == null ) || ( data.Item.ID == 0 ) )
		{
			// set image default
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );

			// count off
			count.enabled = false;
		}
		else
		{
			// set image use icon
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/" + data.Item.File );

			// count on -> renew count
			count.enabled = true;
			count.text = data.Count.ToString() + " / " + resourceCount.ToString();
		}
	}

	// update component element -> use furniture instance
	public void UpdateComponentElement( FurnitureInstance data )
	{
		if( count.enabled )
			count.enabled = false;

		// set default
		if( ( data == null ) || ( data.Furniture == null ) || ( data.Furniture.ID == 0 ) )
		{
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/ItemIcon/EmptySpace" );
		}
		// set image icon
		else
		{
			elementIcon.sprite = Resources.Load<Sprite>( "Image/UI/FurnitureIcon/" + data.Furniture.File );
		}
	}

	public void SetActive()
	{
		button.enabled = true;
		isLocked = false;
	}

	public void LockSlot()
	{
		button.enabled = false;
		isLocked = true;
	}
}

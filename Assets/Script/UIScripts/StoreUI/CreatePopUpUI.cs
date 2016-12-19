using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatePopUpUI : MonoBehaviour
{
	// field - component element
	[SerializeField] DataElement slot;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link data
	public void LinkComponentElement()
	{
		slot = GetComponentInChildren<DataElement>();
	}

	// set data
	public void SetComponentElement( ItemInstance data )
	{
		slot.UpdateComponentElement( data );
	}

	// on click method
	// exit ui
	public void OnClickExitCreatePopUpUI()
	{
		this.gameObject.SetActive( false );
	}


}

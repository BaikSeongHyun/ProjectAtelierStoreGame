using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateResultUI : MonoBehaviour
{
	[SerializeField] DataElement createdItem;
	[SerializeField] Text resultText;

	// unity method
	void Awake()
	{

	}

	// public method
	//
	public void LinkComponentElement()
	{
		createdItem = GetComponentInChildren<DataElement>();
		resultText = GetComponentInChildren<Text>();
	}

	//
	public void SetComponentElement()
	{

	}
}

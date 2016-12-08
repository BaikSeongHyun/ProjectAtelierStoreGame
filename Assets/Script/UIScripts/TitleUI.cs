using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleUI : MonoBehaviour, IPointerDownHandler
{
	// field - component element
	[SerializeField] Image clickImage;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component
	public void LinkComponentElement()
	{
		clickImage = transform.Find( "ClickImage" ).GetComponent<Image>();
	}

	// title image click
	public void OnPointerDown( PointerEventData eventData )
	{
		SceneManager.LoadScene( "GameMainScene" );
	}
}

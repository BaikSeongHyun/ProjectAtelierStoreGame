using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DownsideBar : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// logic data field
	[SerializeField] float buttonMoveSpeed = 150f;
	[SerializeField] bool menuOpened;

	// component element
	[SerializeField] Button storage;
	[SerializeField] Button store;
	[SerializeField] Button customizing;
	[SerializeField] Button achievement;
	[SerializeField] Button setting;
	[SerializeField] Button menu;

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.Find( "GameLogic" ).GetComponent<GameManager>();

		storage = transform.Find( "StorageButton" ).GetComponent<Button>();
		store = transform.Find( "StoreButton" ).GetComponent<Button>();
		customizing = transform.Find( "CustomizingButton" ).GetComponent<Button>();
		achievement = transform.Find( "AchievementButton" ).GetComponent<Button>();
		setting = transform.Find( "SettingButton" ).GetComponent<Button>();
		menu = transform.Find( "MenuButton" ).GetComponent<Button>();
		menuOpened = false;
	}

	// update component
	public void UpdateComponentElement( PlayerData data )
	{
		
	}

	// on click method
	// menu silde on / off
	public void OnClickMenuButton()
	{
		if( !menuOpened )
		{
			StartCoroutine( "MenuOpen" );
			menuOpened = true;
		}
		else
		{
			StartCoroutine( "MenuClose" );
			menuOpened = false;
		}
	}

	// store open
	public void OnClickStoreButton()
	{
		Debug.Log( "상점오픈" );
	}

	// open storage
	public void OnClickStorageButton()
	{
		Debug.Log( "창고오픈" );
	}

	// start customizing
	public void OnClickCustomizingButton()
	{
		Debug.Log( "상점 오브젝트 편집" );
		manager.SetCutomizeingMode();
	}

	// open achievement pop up
	public void OnClickAchievementButton()
	{
		Debug.Log( "업적" );
	}

	// open setting pop up
	public void OnClickSettingButton()
	{
		Debug.Log( "설정" );
	}

	// coroutine section
	// menu item silde open motion
	IEnumerator MenuOpen()
	{
		yield return new WaitForSeconds( 0.001f );

		if( customizing.transform.position.x <= 300 )
		{
			customizing.transform.Translate( Vector3.right * buttonMoveSpeed );
		}
		if( achievement.transform.position.x <= 600 )
		{
			achievement.transform.Translate( Vector3.right * buttonMoveSpeed );
		}
		if( setting.transform.position.x <= 900 )
		{
			setting.transform.Translate( Vector3.right * buttonMoveSpeed );
			StartCoroutine( "MenuOpen" );
		}

	}
	// menu item silde closemotion
	IEnumerator MenuClose()
	{
		yield return new WaitForSeconds( 0.001f );

		if( customizing.transform.position.x >= 150 )
		{
			customizing.transform.Translate( Vector3.left * buttonMoveSpeed );
		}
		if( achievement.transform.position.x >= 150 )
		{
			achievement.transform.Translate( Vector3.left * buttonMoveSpeed );
		}
		if( setting.transform.position.x >= 150 )
		{
			setting.transform.Translate( Vector3.left * buttonMoveSpeed );
			StartCoroutine( "MenuClose" );
		}
	}
}

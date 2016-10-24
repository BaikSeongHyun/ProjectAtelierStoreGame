using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DownsideBar : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// logic data field
	[SerializeField] float buttonMoveSpeed = 150f;

	// component element
	[SerializeField] Button storage;
	[SerializeField] Button store;
    [SerializeField] GameObject customizing;
    [SerializeField] GameObject achievement;
    [SerializeField] GameObject setting;
	[SerializeField] Button menu;
	// public method
	// link component


    void Start()
    {

    }

	public void LinkComponentElement()
	{
		manager = GameObject.Find( "GameLogic" ).GetComponent<GameManager>();

		storage = transform.Find( "StorageButton" ).GetComponent<Button>();
		//store = transform.Find( "StoreButton" ).GetComponent<Button>();

		customizing = transform.Find( "CustomizingButton" ).gameObject;
		achievement = transform.Find( "AchievementButton" ).gameObject;
		setting = transform.Find( "SettingButton" ).gameObject;
		menu = transform.Find( "MenuButton" ).GetComponent<Button>();

        customizing.SetActive(false);
        achievement.SetActive(false);
        setting.SetActive(false);
    }

	// update component
	public void UpdateComponentElement( PlayerData data )
	{
		
	}

	// on click method
	// menu silde on / off
	public void OnClickMenuButton()
	{
        if(!customizing.activeSelf)
        {
            customizing.SetActive(true);
            achievement.SetActive(true);
            setting.SetActive(true);
        }
        else
        {
            customizing.SetActive(false);
            achievement.SetActive(false);
            setting.SetActive(false);
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

	// menu item silde closemotion

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterCreateUI : MonoBehaviour
{
	// high structrue
	[SerializeField] GameManager manager;
	[SerializeField] CharacterManager characterManager;

	// field - component element
	[SerializeField] GameObject nickNameSettingUI;
	[SerializeField] InputField nickNameInputField;
	[SerializeField] Text setCharacter;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	public void LinkComponentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		characterManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<CharacterManager>();

		// component element
		nickNameSettingUI = transform.Find( "NickNameInputBackground" ).gameObject;
		nickNameInputField = nickNameSettingUI.transform.Find( "NickNameInputField" ).GetComponent<InputField>();
		setCharacter = nickNameSettingUI.transform.Find( "SelectText" ).GetComponent<Text>();

		nickNameSettingUI.SetActive( false );
	}


		


	// on click method
	// select character
	public void OnClickSelectCharcterType( int type )
	{
		
		manager.SoundManager.PlayUISoundPlayer( 4 );
		characterManager.SetCharacterInformation( type );

		if( type == 0 )
			setCharacter.text = "슈";
		else if( type == 1 )
			setCharacter.text = "베리";

		nickNameSettingUI.SetActive( true );
	}

	// confirm create character
	public void OnClickConfirmCharacter()
	{
		manager.SoundManager.PlayUISoundPlayer( 4 );
		characterManager.SetInformation( nickNameInputField.text );
		manager.GameStart();
	}

	// exit nickname space
	public void OnClickExitNickNameSpace()
	{
		manager.SoundManager.PlayUISoundPlayer( 4 );
		nickNameInputField.text = "";
		nickNameSettingUI.SetActive( false );
	}
}

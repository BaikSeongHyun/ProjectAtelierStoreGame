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

		nickNameSettingUI.SetActive( false );
	}

	// on click method
	// select character
	public void OnClickSelectCharcterType( int type )
	{
		characterManager.SetCharacterInformation( type );
		nickNameSettingUI.SetActive( true );
	}

	// confirm create character
	public void OnClickConfirmCharacter()
	{
		characterManager.SetInformation( nickNameInputField.text );
		manager.GameStart();

	}
}

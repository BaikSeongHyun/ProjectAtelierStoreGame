using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginForm : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] InputField inputID;
	[SerializeField] InputField inputPassword;
	[SerializeField] Button login;
	[SerializeField] Button join;

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		inputID = transform.Find( "InputID" ).GetComponent<InputField>();
		inputPassword = transform.Find( "InputPassword" ).GetComponent<InputField>();
		login = transform.Find( "LoginButton" ).GetComponent<Button>();
		join = transform.Find( "JoinButton" ).GetComponent<Button>();
	}

	// on click method
	// login -> game start
	public void OnClickLogin()
	{
		Debug.Log( "Game start" );
		manager.GameStart();
	}

	// join -> register game
	public void OnClickJoin()
	{
		Debug.Log( "Join game" );
	}
}

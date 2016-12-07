using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store mode game ui
public class StoreUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] UIManager mainUI;

	// component element
	// - text set
	[SerializeField] Text goldText;
	[SerializeField] Text nameText;
	[SerializeField] Text storeStepText;
	[SerializeField] Image charHead;
	[SerializeField] GameObject stepUpButton;

	// - object set -> child ui object
	[SerializeField] GameObject questUI;

	// - button set -> make on click method
	// achivement, storage, field, customize

	// public method
	// link component
	public void LinkComponentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// text element
		goldText = transform.Find( "Gold" ).Find( "GoldText" ).GetComponent<Text>();
		nameText = transform.Find( "PlayerStatus" ).Find( "NameText" ).GetComponent<Text>();
		storeStepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();
		charHead = transform.Find( "PlayerStatus" ).Find( "CharHead" ).GetComponent<Image>();

		// object element
		questUI = transform.Find( "QuestUI" ).gameObject;
		stepUpButton = transform.Find( "StepUpButton" ).gameObject;

		// object element off
		ClearChildUI();
	}

	// update element
	public void UpdateComponentElement()
	{
		goldText.text = manager.GamePlayer.Gold.ToString();
		nameText.text = manager.GamePlayer.Name.ToString();
		storeStepText.text = manager.GamePlayer.StoreData.StoreStep.ToString();
		if( manager.GamePlayer.CharacterType == "Berry" )
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/BerryHead" );
		else
			charHead.sprite = Resources.Load<Sprite>( "Image/UI/StoreUI/ChouHead" );

		stepUpButton.SetActive( manager.GamePlayer.StoreData.StepUpAlready );
	}

	// clear child ui
	public void ClearChildUI()
	{		
		questUI.SetActive( false );
	}

	// on click method
	// on click store open button
	public void OnClickStoreOpenButton()
	{
		manager.SetStoreOpenPreprocessMode();
	}

	// on click storage button
	public void OnClickStorageButton()
	{
		mainUI.StorageUI.SetActive( true );
	}

	// on click furniture inventory button
	public void OnClickFieldButton()
	{
		Debug.Log( "OnClick Field" );
	}

	// on click mode customizing button
	public void OnClickCustomizingButton()
	{
		ClearChildUI();
		manager.SetCutomizeingMode();
	}

	// on click quest button
	public void OnClickQuestButton()
	{
		ClearChildUI();
		questUI.SetActive( true );
	}

	// on click step up button
	// recreate game field
	public void OnClickStepUpButton()
	{
		manager.RecreateStoreField();
	}
}

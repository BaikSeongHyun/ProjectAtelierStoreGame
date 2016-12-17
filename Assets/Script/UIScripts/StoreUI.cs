using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store mode game ui
public class StoreUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] StoreManager storeManager;
	[SerializeField] UIManager mainUI;

	// component element
	// - text set
	[SerializeField] Text goldText;
	[SerializeField] Text nameText;
	[SerializeField] Text storeStepText;
	[SerializeField] Image charHead;
	[SerializeField] GameObject stepUpButton;


	// - button set -> make on click method
	// achivement, storage, field, customize

	// public method
	// link component
	public void LinkComponentElement()
	{
		// high structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();
		storeManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StoreManager>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();

		// text element
		goldText = transform.Find( "Gold" ).Find( "GoldText" ).GetComponent<Text>();
		nameText = transform.Find( "PlayerStatus" ).Find( "NameText" ).GetComponent<Text>();
		storeStepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();
		charHead = transform.Find( "PlayerStatus" ).Find( "CharHead" ).GetComponent<Image>();

		// object element
		stepUpButton = transform.Find( "StepUpButton" ).gameObject;
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

	// on click open furniture market button
	public void OnClickFunitureMarketButton()
	{
		storeManager.PullFurnitureData();
		mainUI.FurnitureMarketUI.SetActive( true );
		mainUI.FurnitureMarketUILogic.SetComponentElement();
	}

	// on click mode customizing button
	public void OnClickCustomizingButton()
	{
		manager.SetCutomizeingMode();
	}

	// on click step up button
	// recreate game field
	public void OnClickStepUpButton()
	{
		manager.RecreateStoreField();
	}
}

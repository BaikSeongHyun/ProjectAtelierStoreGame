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
	[SerializeField] Text levelText;
	[SerializeField] Text storeStepText;


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
		levelText = transform.Find( "PlayerStatus" ).Find( "LevelText" ).GetComponent<Text>();
		storeStepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();

		// object element
		questUI = transform.Find( "QuestUI" ).gameObject;

		// object element off
		ClearChildUI();
	}

	// update element
	public void UpdateComponentElement()
	{
		goldText.text = manager.GamePlayer.Gold.ToString();
		levelText.text = "Lv." + manager.GamePlayer.Level.ToString();
		storeStepText.text = manager.GamePlayer.StoreData.StoreStep.ToString();
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
		ClearChildUI();
		manager.SetFieldMode();
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

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store mode game ui
public class StoreUI : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	// - text set
	[SerializeField] Text goldText;
	[SerializeField] Text levelText;
	[SerializeField] Text storeStepText;


	// - object set -> child ui object
	[SerializeField] GameObject achievementUI;
	[SerializeField] GameObject storageUI;
	[SerializeField] StorageUI storageUILogic;
	[SerializeField] GameObject questUI;

	// - button set -> make on click method
	// achivement, storage, field, customize
		 
	// property
	public StorageUI StorageUILogic { get { return storageUILogic; } }

	// public method
	// link component
	public void LinkComponentElement()
	{
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// text element
		goldText = transform.Find( "Gold" ).Find( "GoldText" ).GetComponent<Text>();
		levelText = transform.Find( "PlayerStatus" ).Find( "LevelText" ).GetComponent<Text>();
		storeStepText = transform.Find( "PlayerStatus" ).Find( "StoreStepText" ).GetComponent<Text>();

		// object element
		achievementUI = transform.Find( "AchievementUI" ).gameObject;
		storageUI = transform.Find( "StorageUI" ).gameObject;
		storageUILogic = storageUI.GetComponent<StorageUI>();
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

		if( storageUI.activeSelf )
		{
			storageUILogic.UpdateComponentElement();
		}
	}

	// clear child ui
	public void ClearChildUI()
	{
		achievementUI.SetActive( false );
		storageUI.SetActive( false );
		questUI.SetActive( false );
	}

	// on click method
	// on click achievement button
	public void OnClickAchievementButton()
	{
		ClearChildUI();
		achievementUI.SetActive( true );
	}

	// on click storage button
	public void OnClickStorageButton()
	{
		ClearChildUI();
		storageUI.SetActive( true );
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// store & field up side component
public class UpsideBar : MonoBehaviour
{
	// component element
	[SerializeField] Text nameLevel;
	[SerializeField] Text fame;
	[SerializeField] Text charm;
	[SerializeField] Text gold;

	// public method
	// link component
	public void LinkComponentElement()
	{
		nameLevel = transform.Find( "NameAndLevelText" ).GetComponent<Text>();
		fame = transform.Find( "FameText" ).GetComponent<Text>();
		charm = transform.Find( "CharmText" ).GetComponent<Text>();
		gold = transform.Find( "MoneyText" ).GetComponent<Text>();
	}

	// update component
	public void UpdateComponentElement( PlayerData data )
	{
		nameLevel.text = data.Name + " Lv." + data.Level.ToString();
		fame.text = "Fame " + data.Fame.ToString();
		charm.text = "Charm " + data.Charm.ToString();
		gold.text = "Gold " + data.Gold.ToString();
	}

}

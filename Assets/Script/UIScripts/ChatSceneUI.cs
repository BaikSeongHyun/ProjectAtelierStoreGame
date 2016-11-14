using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatSceneUI : MonoBehaviour
{
	[SerializeField] Image npcCutScene;
	[SerializeField] Text chatTextField;

	// public method
	// link component element
	public void LinkComponentElement()
	{
		//link
	}

	// update component element
	public void UpdateComponentElement()
	{
		// update by chat file & npc name
	}


	// on click method
	// exit chat
	public void OnClickExitChat()
	{
		this.gameObject.SetActive( false );
	}
}

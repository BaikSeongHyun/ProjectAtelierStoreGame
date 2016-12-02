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


	// set kakao information
	public void SetKakaoInformation()
	{

	}

	// on click method
	// exit chat
	public void OnClickExitChat()
	{
		this.gameObject.SetActive( false );
	}
}

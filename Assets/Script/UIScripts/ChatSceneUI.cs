using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatSceneUI : MonoBehaviour
{
	// high structure
	[SerializeField] StageManager stageManager;
    [SerializeField] FieldManager fieldManager;
	// field - component
    [SerializeField] GameObject pieMode;
	[SerializeField] Image kakaoImage;
    [SerializeField] Text chatText;

    // field - mini data
    [SerializeField] bool firstPayment;

	// unity method
	void Awake()
	{
		LinkComponentElement();
	}

	// public method
	// link component element
	public void LinkComponentElement()
	{
		// high structure
		stageManager = GameObject.FindWithTag( "GameLogic" ).GetComponent<StageManager>();
        fieldManager = GameObject.FindWithTag("GameLogic").GetComponent<FieldManager>();

        // field - component
        kakaoImage = transform.Find( "KakaoImage" ).GetComponent<Image>();
        chatText = transform.Find( "ChatBack" ).Find( "ChatText" ).GetComponent<Text>();
	}

	// set kakao information
	public void SetKakaoInformation()
	{
		kakaoImage.enabled = true;
		chatText.text = "오늘의 정보 \n\n" + "손님들이 " + stageManager.ProFavor + "%의 확률로 " + ItemData.ReturnTypeString( stageManager.FavoriteGroup ) + "을(를) 선호합니다.\n"
		+ "손님들이 " + stageManager.ProFavor + "%의 확률로 " + CustomerAgent.ReturnBuyScaleString( stageManager.BuyScale ) + "삽니다.";
	}

    // on click method
    // exit chat
    public void OnClickExitChat()
	{
        if (kakaoImage.enabled)
        {
            kakaoImage.enabled = false;
        }
      
        this.gameObject.SetActive( false );
	}
}

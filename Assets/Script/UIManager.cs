using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    private Text nickname;
    private Text level;
    private Text fame;
    private Text charm;
    private Text gold;

    private GameObject edit;
    private GameObject achievement;
    private GameObject set;
    public GameObject questPopup;

    private GameObject expbar;

    private PlayerInformation playerInfo;

    private float speed = 50f;
    private bool menuOpen = false;

    void Start () {

        nickname = GameObject.Find("nickname").GetComponent<Text>();
        level = GameObject.Find("level").GetComponent<Text>();
        fame = GameObject.Find("fame").GetComponent<Text>();
        charm = GameObject.Find("charm").GetComponent<Text>();
        gold = GameObject.Find("gold").GetComponent<Text>();

        edit = GameObject.Find("Edit");
        achievement = GameObject.Find("Achievement");
        set = GameObject.Find("Set");
        //menu.SetActive(false);


        questPopup = GameObject.Find("qPopup");
        questPopup.SetActive(false);

        expbar = GameObject.Find("exp");

        playerInfo = GameObject.Find("sc").GetComponent<PlayerInformation>();
        nickname.text = playerInfo.nickname;
        fame.text = playerInfo.fame.ToString();
        charm.text = playerInfo.charm.ToString();
        gold.text = playerInfo.gold.ToString();

    }
	
	void Update () {
        InfoRenew();

    }


    //퀘스트에서 불러쓰는 함수.
    public void InfoRenew()
    {
        level.text = playerInfo.level.ToString();
        fame.text = playerInfo.fame.ToString();
        charm.text = playerInfo.charm.ToString();
        gold.text = playerInfo.gold.ToString();

        float temp = playerInfo.expRenew;

        expbar.transform.localScale = new Vector3(temp , expbar.transform.localScale.y, expbar.transform.localScale.z);
    }





    // ↓버튼 스크립트↓

    public void MenuBtn()
    {

        //if (!menu.activeSelf)
        //{
        //    menu.SetActive(true);
        //}
        //else
        //{
        //    menu.SetActive(false);
        //}

        if (!menuOpen)
        {
            StartCoroutine("MenuMoving");
            menuOpen = true;
        }
        else
        {
            StartCoroutine("MenuBack");
            menuOpen = false;
        }
    }

    IEnumerator MenuMoving()
    {
        yield return new WaitForSeconds(0.001f);

        if (edit.transform.position.x <= 450)
        {
            edit.transform.Translate(Vector3.right * 50 * speed * Time.deltaTime);
        }
        if(achievement.transform.position.x <= 750)
        {
            achievement.transform.Translate(Vector3.right * 50 * speed * Time.deltaTime);
        }
        if(set.transform.position.x <= 1050)
        {
            set.transform.Translate(Vector3.right * 50 * speed * Time.deltaTime);
            StartCoroutine("MenuMoving");
        }

    }
    IEnumerator MenuBack()
    {
        yield return new WaitForSeconds(0.001f);

        if (edit.transform.position.x >= 0)
        {
            edit.transform.Translate(Vector3.left * 50 * speed * Time.deltaTime);
        }
        if(achievement.transform.position.x >=0)
        {
            achievement.transform.Translate(Vector3.left * 50 * speed * Time.deltaTime);
        }
        if(set.transform.position.x >= 0)
        {
            set.transform.Translate(Vector3.left * 50 * speed * Time.deltaTime);
            StartCoroutine("MenuBack");
        }
    }

    public void StoreBtn()
    {
        Debug.Log("상점오픈");
    }

    public void StorageBtn()
    {
        Debug.Log("창고오픈");
    }


    public void EditBtn()
    {
        Debug.Log("편집");
    }

    public void AchievementBtn()
    {
        Debug.Log("업적");
    }
    public void SetBtn()
    {
        Debug.Log("설정");
    }

    

}

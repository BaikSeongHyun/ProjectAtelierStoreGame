using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FieldUIManager : MonoBehaviour {

    public ObjectDataBuffer databf;

    public GameObject cooltimeImage;
    public Image img;
    public float cooltime;
    public bool disableOnStart = true;
    public float leftTime;

	void Start () {
        databf = GameObject.Find("ObjectManager").GetComponent<ObjectDataBuffer>();


        if (img == null)
            img = gameObject.GetComponent<Image>();         
        if (disableOnStart)
            ResetCooltime();
        
	}
	
	void Update () {
        if(databf.sendData)
        {
            Debug.Log("UIManager");
            ResetCooltime();
            cooltime = databf.cooltime;
            databf.sendData = false;
            cooltimeImage.SetActive(true);
        }
	    if(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0)
            {
                leftTime = 0;
                cooltimeImage.SetActive(false);
            }
        }
        float ratio = 1.0f - (leftTime / cooltime);
        if (img)
            img.fillAmount = ratio;

	}

    public void ResetCooltime()
    {
        leftTime = databf.cooltime;

    }
}

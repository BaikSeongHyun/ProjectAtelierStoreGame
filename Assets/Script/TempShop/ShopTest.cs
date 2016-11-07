using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopTest : MonoBehaviour
{

    public GameObject shop;

    public Image[] shopList;

    void Awake()
    {
        shopSetting();
    }

    void Start()
    {
        shop = GameObject.Find("ShopTest");
        shop.SetActive(false);
    }

    void Update()
    {

    }

    public void ShopBtn()
    {
        if (shop.activeSelf)
        {
            shop.SetActive(false);
        }
        else
        {
            shopImageLoad();
            shop.SetActive(true);
        }

    }

    public void shopSetting()
    {
        shopList = new Image[7];
        for (int i = 0; i < shopList.Length; i++)
        {
            shopList[i] = GameObject.Find("shopList" + i.ToString()).GetComponent<Image>();
        }
    }

    void shopImageLoad()
    {
        Debug.Log("Load");
        //for (int i = 0; i < shopList.Length; i++)
        //{
        //    shopList[i].sprite = Resources.Load<Sprite>("Image/Shop/" + idl.furnitures[i].note) as Sprite;
        //}

        shopList[0].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(1)) as Sprite;
        shopList[1].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(3)) as Sprite;
        shopList[2].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(6)) as Sprite;
        shopList[3].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(9)) as Sprite;
        shopList[4].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(16)) as Sprite;
        shopList[5].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(19)) as Sprite;
        shopList[6].sprite = Resources.Load<Sprite>("Image/Shop/" + FromCodeToFilename(23)) as Sprite;
    }

    string FromCodeToFilename(int _id)
    {
        FurnitureData fd = DataManager.FindFurnitureDataByUID(_id);
        return fd.File;
    }
}

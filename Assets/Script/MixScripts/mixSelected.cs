using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mixSelected : MonoBehaviour
{
    public MixManager mixManager;

    public MixUI ui;

    public Button btn;

    public ItemData info;

    void Awake()
    {

    }

    void Start()
    {
        mixManager = GameObject.Find("MixUI").GetComponent<MixManager>();

        ui = GameObject.Find("MixUI").GetComponent<MixUI>();
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(FromListToMixUI);
    }

    void Update()
    {

    }

    public void FromListToMixUI()
    {
        ui.productionItem.sprite = Resources.Load<Sprite>("Image/UI/ItemIcon/" + info.File) as Sprite;
        ui.productionName.text = info.Name;
        ui.currentCount = 0;
        if (ui.matIsOneAmount.activeSelf)
        {
            ui.matIsOneAmount.SetActive(false);
        }
        else if (ui.matIsTwoAmount.activeSelf)
        {
            ui.matIsTwoAmount.SetActive(false);
        }
        else if (ui.matISThreeAmount.activeSelf)
        {
            ui.matISThreeAmount.SetActive(false);
        }


        if (info.ResourceIDSet.Length == 1)
        {
            ui.matIsOneAmount.SetActive(true);
            ui.MaterialCheck(0, info);
        }
        else if(info.ResourceIDSet.Length == 2)
        {
            ui.matIsTwoAmount.SetActive(true);
            ui.MaterialCheck(1,info); //배열 0,1 <-재료 2개 / 0번째부터 시작해서
        }
        else if(info.ResourceIDSet.Length == 3)
        {
            ui.matISThreeAmount.SetActive(true);
            ui.MaterialCheck(3,info); //배열 2,3,4 <-재료 3개 / 2번째부터 시작해서
        }
        else
        {
            Debug.Log("!");
        }
    }

}

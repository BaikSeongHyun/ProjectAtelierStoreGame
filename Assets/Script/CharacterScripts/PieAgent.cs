using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAgent : MonoBehaviour {

    // high structure
    [SerializeField] UIManager mainUI;

    // animation & ani. cycle time
    [SerializeField] Animator ani;
    [SerializeField] float time;
    [SerializeField] float min, max;


    void Awake()
    {
        DataInitialize();
    }

    void Start () {
        StartCoroutine("UpdateAnimation");
        Debug.Log("pie animation coroutine");
    }
	
	void Update () {
        PieClickEvent();

    }

    public void DataInitialize()
    {
        mainUI = GameObject.FindWithTag("MainUI").GetComponent<UIManager>();

        ani = GetComponent<Animator>();

        min = 2f;
        max = 10f;

        time = Random.Range(min, max);
    }

    private void PieClickEvent()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            target = hit.collider.gameObject;

            if(target.name == "Pie")
            {
                Debug.Log("Pie");
                SendPieEvent();
            }
        }
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
    }

    IEnumerator UpdateAnimation()
    {
        yield return new WaitForSeconds(time);
        ani.SetBool("pieAni", true);
        yield return new WaitForSeconds(0.1f);
        ani.SetBool("pieAni", false);

        time = Random.Range(min, max);
        StartCoroutine("UpdateAnimation");
    }


    public void SendPieEvent()
    {
        mainUI.ChatSceneUI.SetActive(true);        
    }
}

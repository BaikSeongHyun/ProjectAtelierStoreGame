using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectOnField : MonoBehaviour
{
    //상위
    public GameObject body;

	//Item List 에서 갖고있는 아이템이름
	public int id;
	public int count;
	public int min, max;
	public float time ;

    public GameObject cloud;
    public int position = 0 ;


	void Start()
	{
		count = Random.Range( min, max );
        time = Random.Range(3f, 10f);

        cloud = transform.Find("cloud").gameObject;
        cloud.SetActive(false);



        StartCoroutine("CollectTime");
    }

	void Update()
	{

        transform.forward = -Camera.main.transform.forward;
    }


    IEnumerator CollectTime()
    {
        yield return new WaitForSeconds(time);
        cloud.SetActive(true);
    }
}

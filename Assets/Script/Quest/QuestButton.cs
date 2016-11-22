using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{

	private UIManager uiManager;
	private Quest quest;
	private QuestManager questMgr;

	public static int questNumber = 0;

	public int myNumber;
	public bool success = false;


	void Start()
	{
        
		uiManager = GameObject.Find( "MainUI" ).GetComponent<UIManager>();
		quest = GameObject.Find( "퀘스트영역" ).GetComponent<Quest>();
		questMgr = GameObject.Find( "퀘스트영역" ).GetComponent<QuestManager>();

		myNumber = questNumber;

		this.name = questNumber++.ToString();
	}

	void Update()
	{
	}

	public void QuestBtn()
	{

	}
}

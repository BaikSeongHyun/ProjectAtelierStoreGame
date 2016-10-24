using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {

    private Quest quest;
    private QuestList questList;

    void Start()
    {
        quest = GameObject.Find("퀘스트영역").GetComponent<Quest>();

    }
	
	void Update ()
    {
	
	}

    public int ClearQuestNumber(int _num)
    {
        int num = _num;

        return num;
    }
}

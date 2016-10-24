using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestList : MonoBehaviour {

    public List<AddQuest> addQue;

    void Start() {
        addQue = new List<AddQuest>();
        addQue.Add(new AddQuest("제목", "이름", "첫번째 퀘스트", new string[1] { "red" }, 
            new int[1] { 3 },  5, 400, "red", 10));
        addQue.Add(new AddQuest("제목2", "이름2", "두번째 퀘스트", new string[2] { "blue", "green" }, 
            new int[2] { 5, 5 },  10, 1000, "보상2", 30));
        addQue.Add(new AddQuest("제목3", "이름3", "세번째 퀘스트", new string[3] { "재료", "종이", "가루" },
            new int[3] { 50, 25,5 },  8, 1050, "보상3", 5));
        addQue.Add(new AddQuest("4444", "이름3", "4 퀘스트", new string[3] { "재료", "종이", "가루" },
            new int[3] { 50, 25, 5 }, 8, 1050, "보상3", 5));
        addQue.Add(new AddQuest("555", "이름3", "5 퀘스트", new string[3] { "재료", "종이", "가루" },
            new int[3] { 50, 25, 5 }, 8, 1050, "보상3", 5));
        addQue.Add(new AddQuest("6666", "이름3", "6 퀘스트", new string[3] { "재료", "종이", "가루" },
            new int[3] { 50, 25, 5 }, 8, 1050, "보상3", 5));
        addQue.Add(new AddQuest("77777", "이름3", "7 퀘스트", new string[2] { "재료", "종이" },
            new int[2] { 50, 25}, 8, 1050, "보상3", 5));
    }
	
	void Update () {
	
	}

    public class AddQuest
    {
        public int level;
        public string title;
        public string character;
        public string ment;

        //요구 재료 최대 3개
        public string[] demandItem;
        public int[] amount;

        //보상 최대 3개
        public int rewardExp;
        public int rewardGold;
        public string rewardItem;
        public int numberOfRewardItem;

        public AddQuest (
            string _title, //제목
            string _character, //퀘스트를 준 캐릭터 이름
            string _ment, //캐릭터 멘트
            string[] _demandItem, //퀘스트 요구 아이템, 플레이어가 수집해야하는 아이템
            int[] _amount, //demandItem의 갯수
            int _rewardExp,  //보상경험치
            int _rewardGold, //보상골드
            string _rewardItem, //보상 아이템
            int _numberOfRewardItem)  //보상아이템갯수         
        {
            title = _title;
            character = _character;
            ment = _ment;
            demandItem = _demandItem;
            amount = _amount;
            rewardExp = _rewardExp;
            rewardGold = _rewardGold;
            rewardItem = _rewardItem;
            numberOfRewardItem = _numberOfRewardItem;
        }
    }

    /*
    AddQuest addQuestFrame(string title, string character, string ment, string[] demandItem, 
        int[] amount, string rewardItem, int rewardExp, int rewardGold, int numberOfRewardItem)
    {
        return new AddQuest(title, character, ment, demandItem, amount, rewardItem, rewardExp, rewardGold, numberOfRewardItem);
    }
    */
}
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData{

    //데이터 저장 파일명
    public static string fileName = "/dataasdf";

    //UI State
    //로그인 화면 최초 한번만 뜨게 상태를 저장해둬야할 것 같다.

    //Item Inventory Informaition
    public List<ItemData> item ;
    public List<int> count;

    public GameData()
    {
        item = new List<ItemData>();
        count = new List<int>();
    }

}

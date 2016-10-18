using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {

    public string nickname;
    public int level;

    public int fame;
    public int charm;
    public int gold;

    public int experience;
    public int maxExp;
    public float expRenew;

    void Start () {

	}

	void Update () {
    	if(experience >= maxExp)
        {
            level++;
            experience -= maxExp;
            maxExp += 20;
        }

        expRenew = (float)experience / (float)maxExp;
    }
}

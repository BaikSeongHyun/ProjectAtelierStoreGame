using UnityEngine;
using System.Collections;

[System.Serializable]
public class StageResultData
{
	// data field
	[SerializeField] int step;
	[SerializeField] string rank;

	// reward
	[SerializeField] int rewardExp;
	[SerializeField] int rewardGold;
	[SerializeField] int rewardTouchCount;

	// condition
	[SerializeField] int rankProfitMoney;
	[SerializeField] int rankProfitCount;

	// property
	public int Step { get { return step; } }

	public string Rank { get { return rank; } }

	public int RewardExp { get { return rewardExp; } }

	public int RewardGold { get { return rewardGold; } }

	public int RewardTouchCount { get { return rewardTouchCount; } }

	public int RankProfitMoney { get { return rankProfitMoney; } }

	public int RankProfitCount { get { return rankProfitCount; } }

	// constructor - default
	public StageResultData()
	{
		step = 0;
		rank = "F";

		rewardExp = 10;
		rewardGold = 10;
		rewardTouchCount = 0;

		rankProfitMoney = 0;			
		rankProfitCount = 0;
	}

	// counstructor - all parameter
	public StageResultData( int _step, int _rank, int _rewardExp, int _rewardGold, int _rewardTouchCount, int _rankProfitMoney, int _rankProfitCount )
	{
		step = _step;
		rank = ReturnRank( _rank );

		rewardExp = _rewardExp;
		rewardGold = _rewardGold;
		rewardTouchCount = _rewardTouchCount;

		rankProfitMoney = _rankProfitMoney;
		rankProfitCount = _rankProfitCount;
	}


	// static method
	// return rank
	public static string ReturnRank( int rankIndex )
	{
		switch( rankIndex )
		{
			case 1:
				return "A";			
			case 2:
				return "B";			
			case 3:
				return "C";			
			case 4:
				return "F";
		}

		return null;
	}

}
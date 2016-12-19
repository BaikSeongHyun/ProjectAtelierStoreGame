using UnityEngine;
using System.Collections;

[System.Serializable]
public class StoreData
{
	// logic data field
	[SerializeField] bool stepUpAlready;

	// game data field
	[SerializeField] int storeStep;
	[SerializeField] int presentExperience;
	[SerializeField] int requireExperience;
	[SerializeField] float stageTime;


	// property
	public bool StepUpAlready { get { return stepUpAlready; } }

	public int StoreStep { get { return storeStep; } set { storeStep = value; } }

	public int PresentExperience { get { return presentExperience; } set { presentExperience = value; } }

	public int RequireExperience { get { return requireExperience; } }

	public float FillExp { get { return ( ( float ) presentExperience / ( float ) requireExperience ); } }

	public float StageTime { get { return stageTime; } }

	// constructor - default
	public StoreData()
	{
		storeStep = 1;
		stepUpAlready = false;
		stageTime = 0.0f;
	}

	// constructor - step number
	public StoreData( int _step )
	{
		storeStep = _step;
		stepUpAlready = false;
		stageTime = 0.0f;
	}

	// constructor - step & exp data
	public StoreData( int _step, int _requireExperience, float _stageTime )
	{
		storeStep = _step;
		requireExperience = _requireExperience;
		stepUpAlready = false;
		stageTime = _stageTime;
	}

	public StoreData( StoreData stepInfor, int _presentExperience )
	{
		storeStep = stepInfor.storeStep;
		requireExperience = stepInfor.requireExperience;
		stageTime = stepInfor.stageTime;
		presentExperience = _presentExperience;

		CheckStepUpAlready();
	}

	// public method
	// add experience
	public void AddExperience( int exp )
	{
		presentExperience += exp;

		CheckStepUpAlready();
	}

	// check step up
	public bool CheckStepUpAlready()
	{
		if( presentExperience >= requireExperience )
		{
			stepUpAlready = true;
		}
		else
		{
			stepUpAlready = false;
		}

		return stepUpAlready;
	}

	// rank up
	public void RankUp()
	{
		presentExperience = 0;
		storeStep++;
	}

}

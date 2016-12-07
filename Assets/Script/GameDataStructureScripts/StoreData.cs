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


	// property
	public bool StepUpAlready { get { return stepUpAlready; } }

	public int StoreStep { get { return storeStep; } set { storeStep = value; } }

	public int PresentExperience { get { return presentExperience; } set { presentExperience = value; } }

	public int RequireExperience { get { return requireExperience; } }

	// constructor - default
	public StoreData()
	{
		storeStep = 1;
		stepUpAlready = false;
	}

	// constructor - step number
	public StoreData( int _step )
	{
		storeStep = _step;
		stepUpAlready = false;
	}

	// constructor - step & exp data
	public StoreData( int _step, int _requireExperience )
	{
		storeStep = _step;
		requireExperience = _requireExperience;
		stepUpAlready = false;
	}

	public StoreData( StoreData stepInfor, int _presentExperience )
	{
		storeStep = stepInfor.storeStep;
		requireExperience = stepInfor.requireExperience;
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

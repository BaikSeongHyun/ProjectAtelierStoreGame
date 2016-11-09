using UnityEngine;
using System.Collections;

public abstract class AIAgent : MonoBehaviour
{
	// field - protected
	[SerializeField] protected Animator agentAnimator;
	[SerializeField] protected NavMeshAgent moveAgent;

	// unity standard method
	// awake -> set end ponint & data initialize
	void Awake()
	{
		DataInitialize();
	}



	// public method
	// set animation state machine
	public virtual void DataInitialize()
	{
		agentAnimator = GetComponent<Animator>();
		moveAgent = GetComponent<NavMeshAgent>();
	}

	public virtual void SetAnimator()
	{
	}


	// ai agent behaviour -> set ai agent policy
	public virtual void AIAgentBehaviour()
	{
	}
}

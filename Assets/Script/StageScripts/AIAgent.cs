using UnityEngine;
using System.Collections;

public abstract class AIAgent : MonoBehaviour
{
	// field - protected
	[SerializeField] protected Animator agentAnimator;
	[SerializeField] protected NavMeshAgent moveAgent;
	[SerializeField] protected AgentState presentState;

	// enum type -> agent movement state
	public enum AgentState : int
	{
		Idle = 0,
		Walk = 1,
		Greeting = 2,
		Crafting = 3,
		Cheering = 4,
		Setting = 5}

	;
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

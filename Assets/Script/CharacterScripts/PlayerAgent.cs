using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerAgent : AIAgent
{
	[SerializeField] Vector3 destination;
	[SerializeField] AnimatorStateInfo aniInfor;
	[SerializeField] float frame;

	// unity stand method
	// awake
	void Awake()
	{
		DataInitialize();
	}

	void Update()
	{
		CharacterMove();
//
//		switch( presentState )
//		{
//			case AgentState.Idle:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Idle );
//				break;
//			case AgentState.Walk:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Walk );
//				break;
//			case AgentState.Greeting:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Greeting );
//				break;
//			case AgentState.Crafting:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Crafting );
//				break;
//			case AgentState.Cheering:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Cheering );
//				break;
//			case AgentState.Setting:
//				agentAnimator.SetInteger( "State", ( int ) AgentState.Setting );
//				break;			
//		}		       
	}

	public void CharacterMove()
	{
		
	}
}
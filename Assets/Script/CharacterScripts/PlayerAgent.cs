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

	public Vector3 LimitPosition( Vector3 inputPos )
	{
		Vector3 outputPos = new Vector3( Mathf.Clamp( inputPos.x, -40f, 40f ),
		                                 Mathf.Clamp( inputPos.y, 0f, 0f ),
		                                 Mathf.Clamp( inputPos.z, -40f, 40f )
		                    );

		return outputPos;
	}

	public void CharacterMove()
	{
		// do not cast overlay ui event point
		if( !EventSystem.current.IsPointerOverGameObject() )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hitInfo;


			if( Input.GetButtonDown( "LeftClick" ) )
			{
				if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, ( 1 << LayerMask.NameToLayer( "StoreField" ) ) ) )
				{
					destination = LimitPosition( hitInfo.point );
					moveAgent.SetDestination( destination );
					agentAnimator.SetInteger( "State", ( int ) AgentState.Walk );
				}
			}

			if( Vector3.Distance( transform.position, destination ) <= 0.1f )
			{
				moveAgent.ResetPath();
				agentAnimator.SetInteger( "State", ( int ) AgentState.Idle );

			}
		}
	}
}
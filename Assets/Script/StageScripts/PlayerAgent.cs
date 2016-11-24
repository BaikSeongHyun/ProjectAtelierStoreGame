using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerAgent : AIAgent
{
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] Vector3 destination;
	[SerializeField] AnimatorStateInfo aniInfor;
	[SerializeField] float frame;
	[SerializeField] PlayerState presentState;

	// enum type
	public enum PlayerState : int
	{
		Idle = 0,
		Walk = 1,
		Greeting = 2,
		Crafting = 3,
		Cheering = 4,
		Setting = 5}

	;

	// unity stand method
	// awake
	void Awake()
	{
		DataInitialize();
	}

	void Update()
	{
		switch( presentState )
		{
			case PlayerState.Idle:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Idle );
				break;
			case PlayerState.Walk:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Walk );
				break;
			case PlayerState.Greeting:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Greeting );
				break;
			case PlayerState.Crafting:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Crafting );
				break;
			case PlayerState.Cheering:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Cheering );
				break;
			case PlayerState.Setting:
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Setting );
				break;			
		}		       
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
					agentAnimator.SetInteger( "State", ( int ) PlayerState.Walk );
				}
			}

			if( Vector3.Distance( transform.position, destination ) <= 0.1f )
			{
				moveAgent.ResetPath();
				agentAnimator.SetInteger( "State", ( int ) PlayerState.Idle );

			}
		}
	}
}
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerAgent : AIAgent
{
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] Vector3 destination;
	[SerializeField] AnimatorStateInfo aniInfor;
	[SerializeField] float frame;

	// enum type
	public enum PlayerState : int
	{
		idle = 0,
		walk = 1}

	;

	// unity stand method
	// awake
	void Awake()
	{
		DataInitialize();
	}

	void Update()
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
					agentAnimator.SetInteger( "State", ( int ) PlayerState.walk );
				}
			}

			if( Vector3.Distance( transform.position, destination ) <= 0.1f )
			{
				moveAgent.ResetPath();
				agentAnimator.SetInteger( "State", ( int ) PlayerState.idle );

			}
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
}
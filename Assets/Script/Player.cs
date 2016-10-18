using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// field
	[SerializeField] NavMeshAgent moveAgent;

	// awake this script
	void Awake()
	{
		moveAgent = GetComponent<NavMeshAgent>();
	}

	// public method
	// move agent to destination
	public void MovePosition( Vector3 destination )
	{
		Debug.Log( destination );
		moveAgent.destination = destination;
	}
}

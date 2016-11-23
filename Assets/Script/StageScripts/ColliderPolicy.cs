using UnityEngine;
using System.Collections;

public class ColliderPolicy : MonoBehaviour
{
	[SerializeField] string name;

	// set collider name
	void Awake()
	{
		name = gameObject.name;
	}

	//on trigger enter -> customer policy
	void OnTriggerEnter( Collider col )
	{
		CustomerAgent temp = col.gameObject.GetComponent<CustomerAgent>();
		if( temp != null )
		{
			switch( name )
			{
				case "CustomerStoreEnterPoint":	
					if( temp.PresentSequence == CustomerAgent.Sequence.MoveStoreEnter )
						temp.CheckStoreItem();				
					if( temp.PresentSequence == CustomerAgent.Sequence.GoToStore )
						temp.GoToStore();				
					break;

				case "CustomerEndPoint":				
					temp.ResetCustomerAgent();
					break;				
//				case "DoorToIn":
//					if( temp.PresentSequence == CustomerAgent.Sequence.GoToStore )
//						temp.WarpInStore();
					break;
				case "DoorToOut":
					if( temp.PresentSequence == CustomerAgent.Sequence.GoToHome )
						temp.WarpOutStore();
					break;
			}
		}
	}
}

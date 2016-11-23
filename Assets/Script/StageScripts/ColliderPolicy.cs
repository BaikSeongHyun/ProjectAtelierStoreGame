using UnityEngine;
using System.Collections;

public class ColliderPolicy : MonoBehaviour
{

	// set collider name
	void Awake()
	{
		
	}

	//on trigger enter -> customer policy
	void OnTriggerEnter( Collider col )
	{
		CustomerAgent temp = col.gameObject.GetComponent<CustomerAgent>();
		if ( temp != null )
		{
			switch ( this.gameObject.name )
			{
				case "CustomerStoreEnterPoint":	
					if ( temp.PresentSequence == CustomerAgent.Sequence.MoveStoreEnter )
						temp.CheckStoreItem();				
					if ( temp.PresentSequence == CustomerAgent.Sequence.GoToStore )
						temp.GoToStore();				
					break;

				case "CustomerEndPoint":				
					temp.ResetCustomerAgent();
					break;				
//				case "DoorToIn":
//					if( temp.PresentSequence == CustomerAgent.Sequence.GoToStore )
//						temp.WarpInStore();
//					break;
				case "DoorToOut":
					if ( temp.PresentSequence == CustomerAgent.Sequence.GoToHome )
						temp.WarpOutStore();
					break;
			}
		}
	}
}

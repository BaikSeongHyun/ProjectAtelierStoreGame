using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FieldManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// logic data field
	[SerializeField] System.DateTime eventTime;
	[SerializeField] int touchCount;
	[SerializeField] bool eventStart;
	[SerializeField] bool[] isOpened;
	[SerializeField] System.TimeSpan timeDiffer;


	// property
	public int TouchCount { get { return touchCount; } }

	public bool EventStart { get { return eventStart; } }

	public bool[] IsOpened { get { return isOpened; } }

	public System.TimeSpan TimeDifference { get { return timeDiffer; } }

	void Awake()
	{		
		manager = GetComponent<GameManager>();
	}

	// public method
	// check activate
	public bool CheckActivateUseTime()
	{
		System.DateTime temp = System.Convert.ToDateTime( System.DateTime.Now );
		timeDiffer = temp - eventTime;

		if( timeDiffer.TotalSeconds >= DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).WaitingTime )
		{			
			return true;
		}
		else
			return false;
	}

	public float CalculateWaitingTime()
	{
		return ( float ) ( DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).WaitingTime - timeDiffer.TotalSeconds );
	}

	public string WaitingTimeForm()
	{
		string result = "";
		int hours = ( int ) ( CalculateWaitingTime() / 3600 );
		int min = ( int ) ( CalculateWaitingTime() / 60 );
		int sec = ( int ) ( CalculateWaitingTime() % 60 );

		if( hours != 0 )
			result += hours.ToString( "00" ) + ":";
		if( min != 0 )
			result += min.ToString( "00" ) + ":";

		result += sec.ToString( "00" );		

		return result;
	}

	// forced reset use gold
	public bool ForceResetData()
	{
		if( manager.GamePlayer.Gold >= DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).ResetCost )
		{
			manager.GamePlayer.Gold -= DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).ResetCost;

			for( int i = 0; i < isOpened.Length; i++ )
				isOpened[ i ] = false;

			touchCount = DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).CheckNumber;
			touchCount = 3;
			eventStart = false;
			return true;
		}

		return false;
	}


	// reset data
	public void ResetData()
	{
		CheckActivateUseTime();

		for( int i = 0; i < isOpened.Length; i++ )
			isOpened[ i ] = false;

		touchCount = DataManager.FindFieldDataByID( manager.GamePlayer.StoreData.StoreStep ).CheckNumber;
		touchCount = 3;
		eventStart = false;
	}

	// select card
	public void SelectCard( int index, out int cardIndex )
	{
		touchCount--;
		isOpened[ index ] = true;
		cardIndex = Random.Range( 0, 12 );
		if( !eventStart )
		{
			eventStart = true;
			eventTime = System.Convert.ToDateTime( System.DateTime.Now );
		}

		int itemID = 0;
		int itemCount = 0;
		switch( ( cardIndex + 1 ) % 4 )
		{
			case 1:
				// blue powder
				itemID = 22;
				break;
			case 2:
				// red powder
				itemID = 23;
				break;
			case 3:
				// yellow herb
				itemID = 16;
				break;
			case 0:
				// purple herb
				itemID = 17;
				break;
		}

		switch( cardIndex / 4 )
		{
			case 0:
				itemCount = 1;
				break;
			case 1:
				itemCount = 4;
				break;
			case 2:
				itemCount = 9;
				break;			
		}

		manager.GamePlayer.AddItemData( itemID, itemCount );
	}



}



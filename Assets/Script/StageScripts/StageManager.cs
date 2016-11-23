﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;
	[SerializeField] UIManager mainUI;

	// field - store open data
	[SerializeField] float customerCycle;
	[SerializeField] int customerIndex;

	// field - information data
	[SerializeField] int probabilityOfBuyScale;
	[SerializeField] int probabilityOfFavoriteGroup;
	[SerializeField] ItemData.ItemType favoriteGroup;
	[SerializeField] CustomerAgent.BuyScale buyScale;

	// field - element object
	[SerializeField] PlayerAgent playerAgent;
	[SerializeField] CustomerAgent[] customerAgentSet;

	// field selected object
	[SerializeField] Ray ray;
	[SerializeField] RaycastHit hitInfo;
	[SerializeField] CustomerAgent presentSelectedCustomer;
	[SerializeField] FurnitureObject presentSelectedFurniture;

	// field - ui process temp data
	[SerializeField] ItemInstance tempItemInstance;

	// property
	public FurnitureObject PresentSelectedFurniture { get { return presentSelectedFurniture; } }

	// unity method
	// awake -> data initialize
	void Awake()
	{
		DataInitialize();
	}

	// public method
	// create game information
	public void DataInitialize()
	{
		manager = GetComponent<GameManager>();
		customerAgentSet = GameObject.FindWithTag( "CustomerSet" ).GetComponentsInChildren<CustomerAgent>();
		mainUI = GameObject.FindWithTag( "MainUI" ).GetComponent<UIManager>();
	}

	// stage pre process
	// set item price & count
	public void StagePreprocessPolicy()
	{
		// ray cast set furniture target
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// target function type -> create : use create ui(object type)
		// target function type -> storage : use storage ui(object type)
		if( Input.GetButtonDown( "LeftClick" ) )
		{
			if( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer( "Furniture" ) ) )
			{
				GameObject tempSearch = hitInfo.collider.gameObject;
				presentSelectedFurniture = tempSearch.GetComponent<FurnitureObject>();

				if( presentSelectedFurniture.InstanceData.Furniture.Function == FurnitureData.FunctionType.SellObject )
				{
					Debug.Log( "Open sell ui" );
					mainUI.ActivateSellItemSettingUI();				
				}
			}
		}
	}

	// 


	// create customer setting information
	public void CreateGameInformation()
	{
		// allocate probability
		switch( manager.GamePlayer.StoreData.StoreStep )
		{
			case 1:
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 100;
				break;
			case 2:				
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 90;
				break;
			case 3:				
				probabilityOfBuyScale = probabilityOfFavoriteGroup = 80;				
				break;
		}

		// allocate data
		buyScale = CustomerAgent.ReturnBuyScale( Random.Range( 1, 6 ) );
		favoriteGroup = ItemData.ReturnType( Random.Range( 1, 7 ) );
	}

	// stage process
	// custmer policy activate -> use coroutine
	public void StoreOpen()
	{
		StartCoroutine( CustomerGoStore() );
	}

	public void StoreClose()
	{

	}

	// all customer go to store
	public void RushAllCustomer()
	{
		foreach( CustomerAgent element in customerAgentSet )
		{
			element.GoToStore();
		}
	}





	// coroutine section
	// customger section -> use cycle / customer go to store
	IEnumerator CustomerGoStore()
	{
		while( manager.PresentMode == GameManager.GameMode.StoreOpen )
		{
			customerAgentSet[ customerIndex ].MoveStart();
			customerIndex++;
			if( customerIndex >= customerAgentSet.Length )
				customerIndex = 0;
			yield return new WaitForSeconds( customerCycle ); 
		}
	}

}
using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] GameObject objRegen;
	[SerializeField] PlayerOnField playerLogic;
	[SerializeField] ObjectRegeneration objRegenLogic;

	void Awake()
	{
		FieldSettingStart();
	}

	void Start()
	{
		objRegenLogic.SetObjRegeneration();
	}

	public void FieldSettingStart()
	{
		player = GameObject.Find( "Player" );
		playerLogic = player.GetComponent<PlayerOnField>();
		objRegen = GameObject.Find( "ObjectManager" );
		objRegenLogic = objRegen.GetComponent<ObjectRegeneration>();
	}

	// 필드 만들기
	public bool CreateField()
	{
		return true;
	}

	// 필드 내 처하기 -> game manager에 업데이트
	public void FieldProcess()
	{
		// 채취할거 만들기 -> 랜덤위치
		// 갯수가 일정이상일때
	}
}



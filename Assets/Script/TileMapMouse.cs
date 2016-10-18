using UnityEngine;
using System.Collections;

public class TileMapMouse : MonoBehaviour
{
	[SerializeField] TileMap tileMap;

	[SerializeField] Vector3 currentTileCoordinate;

	[SerializeField] Transform selectionCube;

	void Start()
	{
		tileMap = GetComponent<TileMap>();
	}

	// Update is called once per frame
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfor;

		if( Physics.Raycast( ray, out hitInfor, Mathf.Infinity, 1 << LayerMask.NameToLayer( "StoreField" ) ) )
		{
			int x = Mathf.FloorToInt( hitInfor.point.x / tileMap.TileSize );
			int z = Mathf.FloorToInt( hitInfor.point.z / tileMap.TileSize );
			Debug.Log( "Tile :" + x + ", " + z );

			currentTileCoordinate.x = x;
			currentTileCoordinate.z = z;

			selectionCube.transform.position = currentTileCoordinate;
		}
		else
		{

		}

	}
}

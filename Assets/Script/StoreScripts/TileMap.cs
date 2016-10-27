using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent( typeof( MeshFilter ) )]
[RequireComponent( typeof( MeshRenderer ) )]
[RequireComponent( typeof( MeshCollider ) )]                

public class TileMap : MonoBehaviour
{
	// field
	[SerializeField] bool isCustomizing;
	[SerializeField] int size = 10;
	[SerializeField] float tileSize = 1.0f;
	[SerializeField] Image reticleLine;

	// property
	public bool IsCustomizing { get { return isCustomizing; } set { isCustomizing = reticleLine.enabled = value; } }

	public float TileSize { get { return tileSize; } }

	public int Size { get { return size; } }

	// Use this for initialization
	void Start()
	{
		BuildMesh();
	}

	// public method
	public void BuildMesh()
	{
		int tileNumber = size * size;
		int triangleNumber = tileNumber * 2;
		int vertexSizeX = size + 1;
		int vertexSizeY = size + 1;
		int vertexNumber = vertexSizeX * vertexSizeY;
		
		// generate mesh data
		Vector3[] vertices = new Vector3[vertexNumber];
		Vector3[] normals = new Vector3[vertexNumber];
		Vector2[] uv = new Vector2[vertexNumber];

		int[] triangles = new int[triangleNumber * 3];

		int x, y;
		for( y = 0; y < vertexSizeY; y++ )
		{
			for( x = 0; x < vertexSizeX; x++ )
			{
				vertices[ y * vertexSizeX + x ] = new Vector3( x * tileSize, 0, y * tileSize );
				normals[ y * vertexSizeX + x ] = Vector3.up;
				uv[ y * vertexSizeX + x ] = new Vector2( ( float ) x / vertexSizeX, ( float ) y / vertexSizeY );
			}
		}

		for( y = 0; y < size; y++ )
		{
			for( x = 0; x < size; x++ )
			{
				int squareIndex = y * size + x;
				int triangleOffset = squareIndex * 6;
				triangles[ triangleOffset + 0 ] = y * vertexSizeX + x + 0;
				triangles[ triangleOffset + 2 ] = y * vertexSizeX + x + vertexSizeX + 1;
				triangles[ triangleOffset + 1 ] = y * vertexSizeX + x + vertexSizeX + 0;

				triangles[ triangleOffset + 3 ] = y * vertexSizeX + x + 0;
				triangles[ triangleOffset + 5 ] = y * vertexSizeX + x + 1;
				triangles[ triangleOffset + 4 ] = y * vertexSizeX + x + vertexSizeX + 1;
			}
		}

		// create a new mesh & populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		// assign our mesh to our filter / rendeerer / collider
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>(); 

		// set mesh data
		meshCollider.sharedMesh = mesh;
		meshFilter.mesh = mesh;

		// set layer
		gameObject.layer = LayerMask.NameToLayer( "StoreField" ); 

		// set reticle line image object
		reticleLine = GetComponentInChildren<Image>();
		reticleLine.enabled = false;
	}

	public void SetSize( int storeStep )
	{
		switch( storeStep )
		{
			case 1:
				size = 10;
				break;
			case 2:
				size = 15;
				break;
			case 3:
				size = 20;
				break;
		}
	}

}

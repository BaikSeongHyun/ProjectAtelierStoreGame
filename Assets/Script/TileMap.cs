using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent( typeof( MeshFilter ) )]
[RequireComponent( typeof( MeshRenderer ) )]
[RequireComponent( typeof( MeshCollider ) )]                

public class TileMap : MonoBehaviour
{
	// field
	[SerializeField] int sizeX = 10;
	[SerializeField] int sizeY = 10;
	[SerializeField] float tileSize = 1.0f;
	[SerializeField] Texture2D reticleLine;
	[SerializeField] int tileResoultion;
	// property
	public float TileSize { get { return tileSize; } }

	// Use this for initialization
	void Start()
	{
		BuildMesh();
	}

	// public method
	public void BuildMesh()
	{
		int tileNumber = sizeX * sizeY;
		int triangleNumber = tileNumber * 2;
		int vertexSizeX = sizeX + 1;
		int vertexSizeY = sizeY + 1;
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

		for( y = 0; y < sizeY; y++ )
		{
			for( x = 0; x < sizeX; x++ )
			{
				int squareIndex = y * sizeX + x;
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

		meshCollider.sharedMesh = mesh;
		meshFilter.mesh = mesh;
	}

}

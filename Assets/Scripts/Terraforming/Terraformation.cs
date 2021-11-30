using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Terraformation : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;

    private Mesh mesh;


    public int xSize = 100;
    public int zSize = 100;

    public float scale = 10f; 
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 
        
        CreateShape();
        UpdateMesh(); 
    }

    // TODO: implementar con alguna funcion piola 
    private float GenerateHeight(float x, float z)
    {
        return 2*(Mathf.PerlinNoise(x * .3f, z  * .3f) + Mathf.PerlinNoise(x * .1f, z  * .1f))-1; 
    }
    private void CreateShape()
    {
        vertices = new Vector3[(xSize+1)*(zSize+1)];

        for (int z=0, i=0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = GenerateHeight(x, z); 
                vertices[i++] = new Vector3(x*scale, scale*y, z*scale);
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0; 
        
        for (int z=0; z < zSize; z++, vert ++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0; 
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6; 
            }
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles; 
        mesh.RecalculateNormals();
    }

    private void OnMouseDown()
    {
        UpdateMesh();
    }
}

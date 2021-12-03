using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Terraformation : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;
    
    // Colouring of mountains
    private Color[] colors = new [] {new Color(121/255f, 181/255f, 103/255f), new Color(92/255f, 73/255f, 39/255f)};
    
    // Max and min 
    private float maxTerrainHeight = -Mathf.Infinity;
    private float minTerrainHeight = Mathf.Infinity;

    public float MAXTerrainHeight => maxTerrainHeight;
    public float MINTerrainHeight => minTerrainHeight;

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
        transform.position = Vector3.zero;
        UpdateMeshHeights();
        UpdateMesh(); 
        DestroyImmediate(GetComponent<MeshCollider>());
        var newCollider = gameObject.AddComponent<MeshCollider>();
        newCollider.sharedMesh = mesh;
        
    }

    // TODO: implementar con alguna funcion piola 
    public static float PerlinValue(float x, float z, float amplitude, float frequency)
    {
        return (Mathf.PerlinNoise(x * frequency, z  * frequency)); 
    }

    public static float GenerateHeight(float x, float z)
    {
        var flattener = 0.035f; 
        var div = 2; 
        var oct1 = PerlinValue(x, z, 5, 0.1f/div);
        var oct2 = PerlinValue(x, z, 3, 0.25f/div); 
        var oct3 = PerlinValue(x, z, 1, 0.5f/div);
        return (2 * (oct1 + oct2 + oct3) - 1);  //*Mathf.Exp(flattener*(-Mathf.Pow(x, 2) - Mathf.Pow(z, 2)));
    }
    private void CreateShape()
    {
        vertices = new Vector3[(xSize+1)*(zSize+1)];

        for (int z=-zSize/2, i=0; z <= zSize/2; z++)
        {
            for (int x = -xSize/2; x <= xSize/2; x++)
            {
                float y = GenerateHeight(x, z) * scale;
                maxTerrainHeight = (y > maxTerrainHeight ? y : maxTerrainHeight);
                minTerrainHeight = (y < minTerrainHeight ? y : minTerrainHeight); 
                vertices[i++] = new Vector3(x*scale, y, z*scale);
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
        // mesh.colors = colors; 
    }

    private void UpdateMeshHeights()
    {
        GetComponent<Renderer>().material.SetFloat("minHeight", minTerrainHeight);
        GetComponent<Renderer>().material.SetFloat("maxHeight", maxTerrainHeight);
        GetComponent<Renderer>().material.SetColorArray("ascendingColors", colors);
    }

    private void OnMouseDown()
    {
        UpdateMesh();
    }
}

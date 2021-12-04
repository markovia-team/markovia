using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CircularTerraformation : MonoBehaviour
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


    public int rSize = 100;
    public int thetaSize = 100;

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
        return amplitude*Mathf.PerlinNoise(x * frequency, z  * frequency); 
    }

    public static float GenerateHeight(float x, float z)
    {
        var oct1 = PerlinValue(x, z, 1, 1);
        var oct2 = PerlinValue(x, z, 0.5f, 2); 
        var oct3 = PerlinValue(x, z, 0.25f, 4);
       // return Mathf.Pow((oct1 + oct2 + oct3) / (1 + 0.5f + 0.25f), 10f); 
        // return Mathf.Sin();  //oct1 + oct2 + oct3;
        return 100f+Mathf.Exp(-0.01f*(Mathf.Pow(x, 2) + Mathf.Pow(z, 2))); 
    }
    
    
    private void CreateShape()
    {
        vertices = new Vector3[(rSize+1)*(thetaSize+1)];
        
        for (int r= 0, i=0; r<= rSize; r++)
        {
            for (int theta = 0; theta <= thetaSize; theta++)
            {
                var thetaAngle = Mathf.PI * 2f * theta / (1.0f * thetaSize); 
                var x = r * scale *  Mathf.Sin(thetaAngle);
                var z = r * scale * Mathf.Cos(thetaAngle); 
                
                float y = GenerateHeight(x, z)*scale;
                maxTerrainHeight = (y > maxTerrainHeight ? y : maxTerrainHeight);
                minTerrainHeight = (y < minTerrainHeight ? y : minTerrainHeight); 

                vertices[i++] = new Vector3(x, y, z);
            }
        }

        triangles = new int[rSize * thetaSize * 6];
        int vert = 0;
        int tris = 0; 
        
        for (int z=0; z < thetaSize; z++, vert ++)
        {
            for (int x = 0; x < rSize; x++)
            {
                triangles[tris + 0] = vert + 0; 
                triangles[tris + 1] = vert + rSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + rSize + 1;
                triangles[tris + 5] = vert + rSize + 2;

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
    
}

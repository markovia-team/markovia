using UnityEngine;

public class CircularTerraformation : MonoBehaviour {
    public int rSize = 100;
    public int thetaSize = 100;
    public float scale = 10f;

    // Colouring of mountains
    private readonly Color[] colors =
        {new Color(121 / 255f, 181 / 255f, 103 / 255f), new Color(92 / 255f, 73 / 255f, 39 / 255f)};

    // Max and min 
    private Mesh mesh;
    private int[] triangles;
    private Vector3[] vertices;

    public float MAXTerrainHeight { get; private set; } = -Mathf.Infinity;
    public float MINTerrainHeight { get; private set; } = Mathf.Infinity;

    private void Start() {
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
    public static float PerlinValue(float x, float z, float amplitude, float frequency) {
        return amplitude * Mathf.PerlinNoise(x * frequency, z * frequency);
    }

    public static float GenerateHeight(float x, float z) {
        var oct1 = PerlinValue(x, z, 1, 1);
        var oct2 = PerlinValue(x, z, 0.5f, 2);
        var oct3 = PerlinValue(x, z, 0.25f, 4);
        // return Mathf.Pow((oct1 + oct2 + oct3) / (1 + 0.5f + 0.25f), 10f); 
        // return Mathf.Sin();  //oct1 + oct2 + oct3;
        return 100f + Mathf.Exp(-0.01f * (Mathf.Pow(x, 2) + Mathf.Pow(z, 2)));
    }


    private void CreateShape() {
        vertices = new Vector3[(rSize + 1) * (thetaSize + 1)];

        for (int r = 0, i = 0; r <= rSize; r++)
        for (var theta = 0; theta <= thetaSize; theta++) {
            var thetaAngle = Mathf.PI * 2f * theta / (1.0f * thetaSize);
            var x = r * scale * Mathf.Sin(thetaAngle);
            var z = r * scale * Mathf.Cos(thetaAngle);

            var y = GenerateHeight(x, z) * scale;
            MAXTerrainHeight = y > MAXTerrainHeight ? y : MAXTerrainHeight;
            MINTerrainHeight = y < MINTerrainHeight ? y : MINTerrainHeight;

            vertices[i++] = new Vector3(x, y, z);
        }

        triangles = new int[rSize * thetaSize * 6];
        var vert = 0;
        var tris = 0;

        for (var z = 0; z < thetaSize; z++, vert++)
        for (var x = 0; x < rSize; x++) {
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

    private void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        // mesh.colors = colors; 
    }

    private void UpdateMeshHeights() {
        GetComponent<Renderer>().material.SetFloat("minHeight", MINTerrainHeight);
        GetComponent<Renderer>().material.SetFloat("maxHeight", MAXTerrainHeight);
        GetComponent<Renderer>().material.SetColorArray("ascendingColors", colors);
    }
}
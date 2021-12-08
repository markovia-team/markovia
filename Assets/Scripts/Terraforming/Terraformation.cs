using UnityEngine;
using UnityEngine.AI;

public class Terraformation : MonoBehaviour {
    // Limit smoke 
    [SerializeField] private GameObject smoke;

    public int xSize = 100;
    public int zSize = 100;
    public float scale = 10f;

    // Colouring of mountains
    private readonly Color[] colors = {
        new Color(121 / 255f, 181 / 255f, 103 / 255f),
        // new Color(121/255f, 181/255f, 103/255f), 
        // new Color(121/255f, 181/255f, 103/255f), 
        // new Color(121/255f, 181/255f, 103/255f), 

        // new Color(255/255f, 247/255f, 0/255f),
        new Color(92 / 255f, 73 / 255f, 39 / 255f)
        // Color.blue, 
    };

    // Max and min 
    private Mesh mesh;
    private int[] triangles;
    private Vector3[] vertices;

    public float MAXTerrainHeight { get; private set; } = -Mathf.Infinity;
    public float MINTerrainHeight { get; private set; } = Mathf.Infinity;

    // Start is called before the first frame update
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
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void OnMouseDown() {
        UpdateMesh();
    }

    // TODO: implementar con alguna funcion piola 
    public static float PerlinValue(float x, float z, float amplitude, float frequency) {
        x += 0.01f;
        z += 0.01f;
        return Mathf.PerlinNoise(x * frequency, z * frequency);
    }

    public static float GenerateHeight(float x, float z) {
        var oct1 = PerlinValue(x, z, 4, 0.25f);
        var oct2 = PerlinValue(x, z, 2, 0.5f);
        var oct3 = PerlinValue(x, z, 1, 1f);
        return 2 * (oct1 + oct2 + oct3) - 1;
    }

    private void CreateShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int z = -zSize / 2, i = 0; z <= zSize / 2; z++) {
            for (var x = -xSize / 2; x <= xSize / 2; x++) {
                var y = GenerateHeight(x, z) * scale;
                MAXTerrainHeight = y > MAXTerrainHeight ? y : MAXTerrainHeight;
                MINTerrainHeight = y < MINTerrainHeight ? y : MINTerrainHeight;

                if (x == -xSize / 2 || x == xSize / 2 || z == zSize / 2 || z == -zSize / 2) {
                    y = 0;
                    var mult = 1.35f;
                    if (x % 4 == 0 || z % 4 == 0) {
                        Instantiate(smoke, new Vector3(x * scale * mult, 0, z * scale * mult), smoke.transform.rotation, transform);
                        Instantiate(smoke, new Vector3(x * scale * mult * mult, 0, z * scale * mult * mult), smoke.transform.rotation, transform);
                    }
                }

                vertices[i++] = new Vector3(x * scale, y, z * scale);
            }
        }

        triangles = new int[xSize * zSize * 6];
        var vert = 0;
        var tris = 0;

        for (var z = 0; z < zSize; z++, vert++)
        for (var x = 0; x < xSize; x++) {
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
        GetComponent<Renderer>().material.SetInt("q_colors", colors.Length);
        GetComponent<Renderer>().material.SetColorArray("ascendingColors", colors);
    }
}
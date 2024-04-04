using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleQuadGenerator : MonoBehaviour
{
    public int segments = 36; // Number of segments (quads) to create the circle.
    public float radius = 1f; // Radius of the circle.

    void Start()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateCircleMesh(segments, radius);
    }

    Mesh GenerateCircleMesh(int segments, float radius)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        int[] triangles = new int[segments * 6];

        float angleStep = 360.0f / segments;
        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            // Outer vertex
            vertices[vertexIndex] = new Vector3(x, y, 0);
            // Inner vertex (we're not doing anything to make it "inner" for now, it's aligned with the outer)
            vertices[vertexIndex + 1] = new Vector3(x, y, 0); // For a true "quad" effect, you'd adjust these positions.

            if (i < segments)
            {
                triangles[triangleIndex] = vertexIndex;
                triangles[triangleIndex + 1] = vertexIndex + 3;
                triangles[triangleIndex + 2] = vertexIndex + 1;

                triangles[triangleIndex + 3] = vertexIndex;
                triangles[triangleIndex + 4] = vertexIndex + 2;
                triangles[triangleIndex + 5] = vertexIndex + 3;

                triangleIndex += 6;
            }

            vertexIndex += 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // This updates the normals of the vertices.

        return mesh;
    }
}

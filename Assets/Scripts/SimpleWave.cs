using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SimpleWave : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float wavelength = 2f;
    public float speed = 1f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] vertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        vertices = new Vector3[baseVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            float wave = GetWaveHeight(vertex);
            vertex.y = wave;
            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeight(Vector3 worldPosition)
    {
        float wave = Mathf.Sin(
                (worldPosition.x + Time.time * speed) / wavelength
            ) * amplitude;
        return wave;
    }
}
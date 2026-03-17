using UnityEngine;

[System.Serializable]
public struct GerstnerWave
{
    public float amplitude;
    public float wavelength;
    public float speed;
    public Vector2 direction;
}

[RequireComponent(typeof(MeshFilter))]
public class SimpleWave : MonoBehaviour
{

    [SerializeField]
    private GerstnerWave[] waves;

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
        float h = 0;

        foreach (GerstnerWave wave in waves)
        {
            h += GerstnerWave(new Vector2(worldPosition.x, worldPosition.z), wave);
        }

        return h;
    }

    float GerstnerWave(Vector2 position, GerstnerWave wave)
    {
        float k = 2 * Mathf.PI / wave.wavelength;
        float phase = k * Vector2.Dot(wave.direction.normalized, position) - wave.speed * Time.time;

        return wave.amplitude * Mathf.Sin(phase);
    }
}
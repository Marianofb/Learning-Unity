using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshes : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangulos = new int[3];

        vertices[0] = new Vector3(0,0);
        vertices[1] = new Vector3(50,0);
        vertices[2] = new Vector3(0,-50);

        uv[0] = new Vector2(0,0);
        uv[1] = new Vector2(0,1);
        uv[2] = new Vector2(1,1);

        triangulos[0] = 0;
        triangulos[1] = 1;
        triangulos[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangulos;
    }
}

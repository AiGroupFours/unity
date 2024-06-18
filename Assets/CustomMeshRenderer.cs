using System.Collections.Generic;
using UnityEngine;

public class CustomMeshRenderer : MonoBehaviour
{
    public string objFilePath;
    private List<Vector3> vertices;
    private List<int> triangles;

    void Start()
    {
        List<int[]> faces;
        OBJLoader.LoadOBJ(objFilePath, out vertices, out faces);

        triangles = new List<int>();
        foreach (var face in faces)
        {
            List<int> faceIndices = Triangulator.Triangulate(vertices, face);
            triangles.AddRange(faceIndices);
        }
    }

    void OnRenderObject()
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetPass(0);

        GL.PushMatrix();
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.white);

        for (int i = 0; i < triangles.Count; i += 3)
        {
            GL.Vertex(vertices[triangles[i]]);
            GL.Vertex(vertices[triangles[i + 1]]);
            GL.Vertex(vertices[triangles[i + 2]]);
        }

        GL.End();
        GL.PopMatrix();
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OBJLoader
{
    public static void LoadOBJ(string path, out List<Vector3> vertices, out List<int[]> faces)
    {
        vertices = new List<Vector3>();
        faces = new List<int[]>();

        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("v "))
                {
                    string[] parts = line.Split(' ');
                    float x = float.Parse(parts[1]);
                    float y = float.Parse(parts[2]);
                    float z = float.Parse(parts[3]);
                    vertices.Add(new Vector3(x, y, z));
                }
                else if (line.StartsWith("f "))
                {
                    string[] parts = line.Split(' ');
                    int[] face = new int[parts.Length - 1];
                    for (int i = 1; i < parts.Length; i++)
                    {
                        face[i - 1] = int.Parse(parts[i].Split('/')[0]) - 1;
                    }
                    faces.Add(face);
                }
            }
        }
    }
}
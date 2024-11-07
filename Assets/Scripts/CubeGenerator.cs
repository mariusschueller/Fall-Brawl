using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGridGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // assign your cube prefab in the inspector
    public int rows = 4;
    public int cols = 8;
    public float spacing = 1.0f; // adjust if your cubes are bigger or smaller than 1 unit

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 position = new Vector3(i * spacing, -1, j * spacing);
                Instantiate(cubePrefab, position, Quaternion.identity);
            }
        }
    }
}

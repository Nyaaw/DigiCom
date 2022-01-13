using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const float SCALE_MULTIPLIER = 3.5f;
        const float DIRECTORIES_HEIGHT = 1.3f;
        const float EDGE_OF_TABLE = 0.4325f;
        const float GAP_BETWEEN_ELEMENTS = 0.2171f;
        const int GAP_BETWEEN_TABLES = 4;
        List<string> directoryList = new List<string>(System.IO.Directory.EnumerateDirectories("C:"));
        int i = 0;
        int sizeOfTable = 8;
        float x;
        float z;
        Vector3 scale;
        int xInTable;
        int yInTable;
        foreach (string directory in directoryList)
        {
            i+= (sizeOfTable + GAP_BETWEEN_TABLES);
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(0, 1, i);
            List<string> subDirectoriesList = new List<string>(System.IO.Directory.EnumerateDirectories(directory.ToString()));
            List<string> subFilesList = new List<string>(System.IO.Directory.EnumerateFiles(directory.ToString()));
            List<string> subElementsList = new List<string>(subDirectoriesList);
            subElementsList.AddRange(subFilesList);

            xInTable = 0;
            yInTable = 0;
            foreach (string subElement in subElementsList)
            {               
                GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                subCube.transform.SetParent(table.transform);

                scale = subCube.transform.localScale;
                x = scale.x;
                z = scale.z;

                if ((-SCALE_MULTIPLIER * z) + xInTable * GAP_BETWEEN_ELEMENTS >= EDGE_OF_TABLE)
                {
                    xInTable = 0;
                    yInTable++;
                }

                x = (-SCALE_MULTIPLIER * x) + yInTable * GAP_BETWEEN_ELEMENTS;
                z = (-SCALE_MULTIPLIER * z) + xInTable * GAP_BETWEEN_ELEMENTS;

                subCube.transform.localPosition = new Vector3(x, DIRECTORIES_HEIGHT, z);
                xInTable++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const float SCALEMULTIPLIER = 3.5f;
        const float DIRECTORIESHEIGHT = 1.3f;
        const float EDGEOFTABLE = 0.4325f;
        const float GAPBETWEENELEMENTS = 0.2171f;
        List<string> directoryList = new List<string>(System.IO.Directory.EnumerateDirectories("C:/Users/Admin/Documents/Dolphin Emulator"));
        int i = 0;
        int sizeOfTable = 8;
        float x;
        float z;
        Vector3 scale;
        int xInTable;
        int yInTable;
        foreach (string directory in directoryList)
        {
            i+= sizeOfTable;
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(0, 1, i++);
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

                if ((-SCALEMULTIPLIER * z) + xInTable * GAPBETWEENELEMENTS >= EDGEOFTABLE)
                {
                    xInTable = 0;
                    yInTable++;
                }

                x = (-SCALEMULTIPLIER * x) + yInTable * GAPBETWEENELEMENTS;
                z = (-SCALEMULTIPLIER * z) + xInTable * GAPBETWEENELEMENTS;

                subCube.transform.localPosition = new Vector3(x, DIRECTORIESHEIGHT, z);
                xInTable++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

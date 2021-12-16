using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> directoryList = new List<string>(System.IO.Directory.EnumerateDirectories("C:/Users/Admin/Documents/Dolphin Emulator"));
        int i = 0;
        int sizeOfTable = 8;
        foreach (string directory in directoryList)
        {
            i+= sizeOfTable;
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(0, 0, i++);
            Debug.Log(directory.ToString());
            List<string> subDirectoriesList = new List<string>(System.IO.Directory.EnumerateDirectories(directory.ToString()));
            foreach (string subDir in subDirectoriesList)
            {               
                GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                subCube.transform.SetParent(table.transform);

                Vector3 scale = subCube.transform.localScale;
                float x = scale.x;
                float z = scale.z;

                i += 2;
                subCube.transform.position = new Vector3(0, 1, i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

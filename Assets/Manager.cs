using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> directoryList = new List<string>(System.IO.Directory.EnumerateDirectories("C:/Users/Admin/Documents/Dolphin Emulator"));
        int y = 10;
        int sizeOfTable = 8;
        foreach (string directory in directoryList)
        {
            y+= sizeOfTable;
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(y, 0, y);
            Debug.Log(directory.ToString());
            List<string> subDirectoriesList = new List<string>(System.IO.Directory.EnumerateDirectories(directory.ToString()));
            foreach (string subDir in subDirectoriesList)
            {
                GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                subCube.transform.position = new Vector3(y++, 0.5f, y++);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

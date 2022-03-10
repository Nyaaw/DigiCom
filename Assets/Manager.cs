using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;



public class Manager : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update

    const float SCALE_MULTIPLIER = 3.5f;
    const float DIRECTORIES_HEIGHT = 1.3f;
    const float EDGE_OF_TABLE = 0.4325f;
    const float GAP_BETWEEN_ELEMENTS = 0.2171f;
    const int GAP_BETWEEN_TABLES = 4;
    int i = 0;
    int sizeOfTable = 8;
    float x;
    float z;
    Vector3 scale;
    int xInTable;
    int yInTable;
    private static DirectoryInfo sourceDirectory = new DirectoryInfo("C:\\Users\\Admin\\Documents\\Dolphin Emulator");
    IEnumerable<DirectoryInfo> directoryList = sourceDirectory.EnumerateDirectories();
    void generateSubElement(object typeOfElement, GameObject tableParam)
    {
        GameObject subElement;
        if (typeOfElement is DirectoryInfo)
        {
            subElement = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else if (typeOfElement is FileInfo)
        {
            subElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        else
        {
            return;
        }
        subElement.transform.SetParent(tableParam.transform);

        scale = subElement.transform.localScale;
        x = scale.x;
        z = scale.z;

        if ((-SCALE_MULTIPLIER * z) + xInTable * GAP_BETWEEN_ELEMENTS >= EDGE_OF_TABLE)
        {
            xInTable = 0;
            yInTable++;
        }

        x = (-SCALE_MULTIPLIER * x) + yInTable * GAP_BETWEEN_ELEMENTS;
        z = (-SCALE_MULTIPLIER * z) + xInTable * GAP_BETWEEN_ELEMENTS;

        subElement.transform.localPosition = new Vector3(x, DIRECTORIES_HEIGHT, z);
        xInTable++;
    }

    void Start()
    {
        TextMeshPro tModel = GameObject.FindObjectOfType<TextMeshPro>();

        TextMeshPro tClone = Object.Instantiate(tModel);

        
        foreach (DirectoryInfo directory in directoryList)
        {
            i += (sizeOfTable + GAP_BETWEEN_TABLES);
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(0, 1, i);
            

            IEnumerable<DirectoryInfo> subDirectoriesList = directory.EnumerateDirectories();

            xInTable = 0;
            yInTable = 0;

            foreach (DirectoryInfo subDirectory in subDirectoriesList)
            {
                generateSubElement(subDirectory, table);
            }
            IEnumerable<FileInfo> subFilesList = directory.EnumerateFiles();
            foreach (FileInfo subFile in subFilesList)
            {
                generateSubElement(subFile, table);
            }
            Debug.Log(tModel.text);
            tModel.text = "Test";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


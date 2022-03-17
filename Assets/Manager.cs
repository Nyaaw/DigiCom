using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;



public class Manager : MonoBehaviour
{
    [SerializeField]

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

    void deleteElement(object elementToDelete)
    {
        if (elementToDelete is DirectoryInfo)
        {
            (elementToDelete as DirectoryInfo).Delete();
        }
        else if (elementToDelete is FileInfo)
        {
            (elementToDelete as FileInfo).Delete();
        }
    }

    void generateSubElement(object typeOfElement, GameObject tableParam, TextMeshPro tModel)
    {
        GameObject subElement;
        TextMeshPro tElementName = Object.Instantiate(tModel);
        tElementName.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 5);
        tElementName.alignment = TextAlignmentOptions.Center;
        tElementName.transform.localScale = new Vector3(0.08f, 0.2f, 0.1f);
        tElementName.transform.position = new Vector3(0, 1, 0);
        tElementName.transform.Rotate(new Vector3(0, -90, 0));
        tElementName.color = Color.black;
        if (typeOfElement is DirectoryInfo)
        {
            subElement = GameObject.CreatePrimitive(PrimitiveType.Cube);
            subElement.AddComponent<PathManager>();
            subElement.GetComponent<PathManager>().FullPath = (typeOfElement as DirectoryInfo).FullName;
            tElementName.text = (typeOfElement as DirectoryInfo).Name;
        }
        else if (typeOfElement is FileInfo)
        {
            subElement = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            subElement.AddComponent<PathManager>();
            subElement.GetComponent<PathManager>().FullPath = (typeOfElement as FileInfo).FullName;
            tElementName.text = (typeOfElement as FileInfo).Name;
        }
        else
        {
            return;
        }

        subElement.transform.SetParent(tableParam.transform);
        tElementName.transform.SetParent(subElement.transform);

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

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro tModel = GameObject.FindObjectOfType<TextMeshPro>();
        foreach (DirectoryInfo directory in directoryList)
        {
            i += (sizeOfTable + GAP_BETWEEN_TABLES);
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            table.AddComponent<PathManager>();
            table.GetComponent<PathManager>().FullPath = directory.FullName;
            table.transform.localScale = new Vector3(sizeOfTable, 1, sizeOfTable);
            table.transform.position = new Vector3(0, 1, i);

            TextMeshPro tDirectoryName = Object.Instantiate(tModel);
            tDirectoryName.transform.SetParent(table.transform);
            tDirectoryName.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 5);
            tDirectoryName.alignment = TextAlignmentOptions.Center;
            tDirectoryName.transform.localScale = new Vector3(0.03f, 0.3f, 0.1f);
            tDirectoryName.transform.position = new Vector3(4.5f, 1, i);
            tDirectoryName.transform.Rotate(new Vector3(0, -90, 0));
            tDirectoryName.color = Color.black;
            tDirectoryName.text = directory.Name;
            

            IEnumerable<DirectoryInfo> subDirectoriesList = directory.EnumerateDirectories();

            xInTable = 0;
            yInTable = 0;

            foreach (DirectoryInfo subDirectory in subDirectoriesList)
            {
                generateSubElement(subDirectory, table,tModel);
            }
            IEnumerable<FileInfo> subFilesList = directory.EnumerateFiles();
            foreach (FileInfo subFile in subFilesList)
            {
                generateSubElement(subFile, table, tModel);
            }
        }

        // Update is called once per frame
        void Update()
        {
            tModel.enabled = false;
        }
    }
}


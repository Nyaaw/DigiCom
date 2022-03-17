using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;



public class Manager : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update

    public const float GLOBAL_SCALE_MULTIPLIER = 0.2f;

    float SCALE_MULTIPLIER = 3.5f;
    float DIRECTORIES_HEIGHT = 1.3f * GLOBAL_SCALE_MULTIPLIER;
    float EDGE_OF_TABLE = 0.4325f;
    float GAP_BETWEEN_ELEMENTS = 0.2171f;
    float GAP_BETWEEN_TABLES = 4 * GLOBAL_SCALE_MULTIPLIER;
    float GAP_HEIGHT_ELEMENT_OVER_TABLES = 1.1f;
    float i = 0;
    float sizeOfTable = 8;
    float x;
    float z;
    Vector3 scale;
    int xInTable;
    int yInTable;
    private static DirectoryInfo sourceDirectory = new DirectoryInfo("C:\\Users\\Rip\\Documents\\Code\\SpaceChess\\");
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
        subElement.transform.localPosition = Vector3.zero;

        subElement.transform.localScale = new Vector3(GLOBAL_SCALE_MULTIPLIER, GLOBAL_SCALE_MULTIPLIER, GLOBAL_SCALE_MULTIPLIER);
        

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

        subElement.transform.localPosition = new Vector3(x, GAP_HEIGHT_ELEMENT_OVER_TABLES, z);
        xInTable++;

        subElement.AddComponent<XRGrabInteractable>();
    }

    void Start()
    {
        foreach (DirectoryInfo directory in directoryList)
        {
            GameObject newParent = new GameObject("tableParent");
            i += (sizeOfTable + GAP_BETWEEN_TABLES);
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newParent.transform.position = new Vector3(0, DIRECTORIES_HEIGHT, i * GLOBAL_SCALE_MULTIPLIER);

            table.transform.SetParent(newParent.transform);
            table.transform.localPosition = Vector3.zero;
            table.transform.localScale = new Vector3(sizeOfTable * GLOBAL_SCALE_MULTIPLIER, GLOBAL_SCALE_MULTIPLIER, sizeOfTable * GLOBAL_SCALE_MULTIPLIER);


            IEnumerable<DirectoryInfo> subDirectoriesList = directory.EnumerateDirectories();

            xInTable = 0;
            yInTable = 0;

            foreach (DirectoryInfo subDirectory in subDirectoriesList)
            {
                generateSubElement(subDirectory, newParent);
            }
            IEnumerable<FileInfo> subFilesList = directory.EnumerateFiles();
            foreach (FileInfo subFile in subFilesList)
            {
                generateSubElement(subFile, newParent);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


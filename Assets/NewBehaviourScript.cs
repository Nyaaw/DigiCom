using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_Text clockTest;
    // Update is called once per frame
    void Update()
    {
        clockTest.text = DateTime.Now.ToString();
    }
}

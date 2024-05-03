using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prikol : MonoBehaviour
{
    private int _counter = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _counter++;
            if (_counter >= 15)
            {
                Application.Quit();
                Debug.Log("Quit");
            }
            
        }
    }
}

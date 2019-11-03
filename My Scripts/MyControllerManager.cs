using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyControllerManager : MonoBehaviour
{
    private List<MyController> controllers;

    private int index;

    private void Awake()
    {
        controllers = FindObjectsOfType<MyController>().ToList();
        index = 1;
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }
}

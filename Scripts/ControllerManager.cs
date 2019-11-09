using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private List<Controller> controllers;

    private void Awake()
    {
        controllers = FindObjectsOfType<Controller>().ToList(); //what's the difference between this and GetComponent?

        int index = 1;
        foreach (var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            if (controller.IsAssigned == false && controller.AnyButtonDown())
            {
                AssignController(controller);
            }
        }
    }

    private void AssignController(Controller controller) //we need this parameter to reference each individual controller in the list
    {
        controller.IsAssigned = true;
        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller);
    }
}

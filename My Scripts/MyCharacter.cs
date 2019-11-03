using System;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    private MyController controller;

    internal void SetController(MyController controller)
    {
        this.controller = controller;
    }
}

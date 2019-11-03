using System;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    [SerializeField]
    private int playerNumber;

    private MyUIPlayerText uiPlayerText;

    public MyController Controller { get; private set; }
    public MyCharacter CharacterPrefab { get; set; }

    public int PlayerNumber { get { return playerNumber; } }
    public bool HasController { get { return Controller != null; } }

    private void Awake()
    {
        Controller = GetComponent<MyController>();
        uiPlayerText = GetComponentInChildren<MyUIPlayerText>();
    }

    internal void InitializePlayer(MyController controller)
    {
        Controller = controller;
        gameObject.name = string.Format("Player {0} - {1}", PlayerNumber, controller.gameObject.name);
        uiPlayerText.HandleInitialization();
    }

    public void SpawnCharacter()
    {
        var character = Instantiate(CharacterPrefab, Vector3.zero, Quaternion.identity);
        character.SetController(Controller);
    }
}
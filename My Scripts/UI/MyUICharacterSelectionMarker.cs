using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MyUICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField]
    private Image markerImage;
    [SerializeField]
    private Image lockImage;
    [SerializeField]
    private MyPlayer player;

    private bool initializing;
    private bool initialized;

    private MyUICharacterSelectionMenu menu;

    public bool IsPlayerIn { get { return player.HasController; } }

    public bool IsLockedIn { get; private set; }

    private void Awake()
    {
        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
        menu = GetComponentInParent<MyUICharacterSelectionMenu>();
    }

    private void Update()
    {
        if (!initializing)
            return;

        if (IsPlayerIn)
            StartCoroutine(Initialize());

        if (!initialized)
            return;

        if (!IsLockedIn)
        {
            if (player.Controller.horizontalAxis <= -0.5f)
                MoveToCharacterPanel(menu.LeftPanel);
            else if (player.Controller.horizontalAxis >= 0.5f)
                MoveToCharacterPanel(menu.RightPanel);

            if (player.Controller.attackPressed)
                StartCoroutine(LockCharacter());
        }
        else
        {
            if (player.Controller.attackPressed)
                menu.TryToStartGame();
        }
        
    }

    private IEnumerator LockCharacter()
    {
        lockImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        IsLockedIn = true;
    }

    private void MoveToCharacterPanel(MyUICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
        player.CharacterPrefab = panel.CharacterPrefab;
    }

    private IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);
        markerImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        initialized = true;
    }
}

using UnityEngine;

public class MyUICharacterSelectionMenu : MonoBehaviour
{
    [SerializeField]
    private MyUICharacterSelectionPanel leftPanel;
    [SerializeField]
    private MyUICharacterSelectionPanel rightPanel;

    public MyUICharacterSelectionPanel LeftPanel { get { return leftPanel; } }
    public MyUICharacterSelectionPanel RightPanel { get { return rightPanel; } }

    private MyUICharacterSelectionMarker[] markers;

    private int playerCount;
    private int lockedCount;
    private bool startEnabled;

    private void Awake()
    {
        markers = GetComponentsInChildren<MyUICharacterSelectionMarker>();
    }

    private void Update()
    {
        foreach (var marker in markers)
        {
            if (marker.IsPlayerIn)
                playerCount++;
            if (marker.IsLockedIn)
                lockedCount++;

            startEnabled = playerCount > 0 && playerCount == lockedCount;

            if (startEnabled)
                TryToStartGame();
        }
    }

    internal void TryToStartGame()
    {
        GameManager.Instance.Begin();
    }
}

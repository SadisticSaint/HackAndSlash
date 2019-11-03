using System.Linq;
using UnityEngine;

public class MyPlayerManager : MonoBehaviour
{
    public static MyPlayerManager Instance { get; private set; }

    private MyPlayer[] players;

    private void Awake()
    {
        Instance = this;
        players = GetComponentsInChildren<MyPlayer>();
    }

    public void AddPlayerToGame(MyController controller)
    {
        foreach (var player in players)
        {
            var firstNonActiveplayer = players
                .OrderBy(t => t.PlayerNumber)
                .FirstOrDefault(t => t.HasController == false);
            firstNonActiveplayer.InitializePlayer(controller);

        }
    }

    public void SpawnPlayerCharacter()
    {
        foreach (var player in players)
        {
            if (player.HasController && player.CharacterPrefab != null)
                player.SpawnCharacter();
        }
    }
}

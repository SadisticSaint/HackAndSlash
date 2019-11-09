using System.Collections;
using TMPro;
using UnityEngine;

public class UIPlayerText : MonoBehaviour
{
    private TextMeshProUGUI tmText;
    private Player player;

    private void Awake()
    {
        tmText = GetComponent<TextMeshProUGUI>();
        player = GetComponent<Player>();
    }

    internal void HandlePlayerInitialized()
    {
        tmText.text = "Player Joined";
        //tmText.text = string.Format("Player {0} Joined", player.PlayerNumber); //show player number when adding to game
        StartCoroutine(ClearTextAfterDelay());
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        tmText.text = string.Empty;
    }
}

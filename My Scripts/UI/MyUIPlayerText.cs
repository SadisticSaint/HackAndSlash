using System.Collections;
using UnityEngine;
using TMPro;

public class MyUIPlayerText : MonoBehaviour
{
    private TextMeshProUGUI tmText;
    private MyPlayer player;

    private void Awake()
    {
        tmText = GetComponent<TextMeshProUGUI>();
    }

    internal void HandleInitialization()
    {
        tmText.text = "Player Joined";
        StartCoroutine(ClearTextAfterDelay());
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        tmText.text = string.Empty;
    }
}

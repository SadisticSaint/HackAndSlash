using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Begin()
    {
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (operation.isDone == false)
            yield return null;

        MyPlayerManager.Instance.SpawnPlayerCharacter();
    }
}

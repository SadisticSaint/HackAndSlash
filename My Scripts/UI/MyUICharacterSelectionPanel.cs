using UnityEngine;

public class MyUICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField]
    private MyCharacter characterPrefab;

    public MyCharacter CharacterPrefab { get { return characterPrefab; } }
}

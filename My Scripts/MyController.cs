using UnityEngine;

public class MyController : MonoBehaviour
{
    //1.) Set basic controls
    //2.) Add functionality for multiple controllers
    //3.) Assign controllers to players
    //4.) Add players to game
    //5.) Show confirmation that player has joined

    public bool attackButton;
    public float horizontalAxis;
    public float verticalAxis;

    private string attack;
    private string horizontal;
    private string vertical;
    internal bool attackPressed;

    public int Index { get; private set; }

    private void Update()
    {
        attackButton = Input.GetButton(attack);
        attackPressed = Input.GetButtonDown(attack);
        horizontalAxis = Input.GetAxis(horizontal);
        verticalAxis = Input.GetAxis(vertical);
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attack = "Attack" + Index;
        horizontal = "Horizontal" + Index;
        vertical = "Vertical" + Index;

        gameObject.name = "Controller " + Index;
    }
}

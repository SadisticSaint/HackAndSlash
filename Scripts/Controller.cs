using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; } //this is a property
    public bool IsAssigned { get; set; }

    public bool attackPressed;
    public float horizontal;
    public float vertical;

    private bool attack;
    private string attackButton;
    private string horizontalAxis;
    private string verticalAxis;

    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton)) //why is this needed?
        {
            attack = Input.GetButton(attackButton);

            attackPressed = Input.GetButtonDown(attackButton);
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        horizontalAxis = "Horizontal" + Index;
        verticalAxis = "Vertical" + Index;

        gameObject.name = "Controller " + index;
    }

    internal bool AnyButtonDown() //does this need to be a method?
    {
        return attack;
    }

    internal Vector3 GetDirection()
    {
        return new Vector3(horizontal, 0, vertical);
    }
}

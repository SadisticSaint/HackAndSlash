using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    private new Rigidbody rigidbody;

    [SerializeField]
    private float forceAmount = 10f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeHit(IAttack hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);
        //difference between current position and position of thing that's hitting it
        //Normalize gives values from 0 to 1?
        rigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}

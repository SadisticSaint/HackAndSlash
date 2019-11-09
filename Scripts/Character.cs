using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Character : MonoBehaviour, ITakeHit
{
    public static List<Character> All = new List<Character>(); //static?
    //making this a property with a private setter doesn't mean it can't be modified outside this class
        //e.g. All.Remove would still be accessible
    //static means that it's shared across all of the Character class instead of there being an All for each character

    private Controller controller;
    private Attacker attacker;
    private Animator animator;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private int damage = 1;


    private int currentHealth;
    private int maxHealth;

    public int Damage { get { return damage; } }

    private void Awake()
    {
        attacker = GetComponent<Attacker>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        if (All.Contains(this) == false)
            All.Add(this);
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection(); //added to simplify code
        if (direction.magnitude > 0.01f) //makes sure character doesn't move without input
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = direction * 360; //fix rotation

            animator.SetFloat("Speed", direction.magnitude);
        }
        else
            animator.SetFloat("Speed", 0);

        if (controller.attackPressed) //make character stop moving when attacking
        {
            if (attacker.CanAttack)
            {
                animator.SetTrigger("Attack"); //add cooldown for attack time
            }
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    public void TakeHit(IAttack hitBy)
    {
        currentHealth -= hitBy.Damage;
    }
}

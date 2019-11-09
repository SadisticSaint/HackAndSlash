using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField]
    private float attackRefreshSpeed = 1.5f;
    [SerializeField]
    private float attackOffset = 1f;
    [SerializeField]
    private float attackRadius = 1f;

    private float attackTimer;
    private Collider[] attackResults;

    public int Damage { get { return 1; } }

    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    private void Awake()
    {
        attackResults = new Collider[10]; //revisit why this is needed (15D)
        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact; //alternative to putting this in AnimationImpactWatcher
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public void Attack(ITakeHit target) //why not Character or Enemy parameter?
    {
        attackTimer = 0;
        target.TakeHit(this);
    }

    /// <summary>
    /// called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * attackOffset; //in AnimationImpactWatcher, transform.position would have to be retrieved on every Impact event
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults); //and it's best to set the position on the character instead of a script that only handles one event

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>(); //is it best to GetComponent so frequently?
                                                                     //How does GetComponent work if it's not a component of the current GameObject?
                                                                     //returns components from other colliders that populate attackResults
                                                                     //can ITakeHit be passed in OnImpact instead? Would it perform better?
                                                                     //How is ITakeHit a component?
                                                                     //OnColliderEnter/OnTriggerEnter alternative?
                                                                     //No becase then anytime this collider interacts with  the other collider, this would be called
            if (takeHit != null) //if something was hit...
                takeHit.TakeHit(this); //call ITakeHit
        }
    }
}
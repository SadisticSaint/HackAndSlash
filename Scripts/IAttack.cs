using UnityEngine;

public interface IAttack
{
    int Damage { get; }

    Transform transform { get; } //transform has to be lower case?
    //why is transform needed?
}
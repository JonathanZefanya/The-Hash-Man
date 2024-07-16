using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Abilities/AbilitySystem", fileName = "New Ability System")]

public class AbilitySystem : ScriptableObject
{
    public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDirection, Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {

    }
}

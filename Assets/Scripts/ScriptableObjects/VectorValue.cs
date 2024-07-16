using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    [Header("Value running in game")]
    public Vector2 runtimeValue;

    [Header("Value by default when starting")]
    public Vector2 initialValue;
}

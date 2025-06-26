using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Objects/Data: Grunt")]
public class EnemyData : ScriptableObject
{
    [Header("Patrol Settings")]
    public Transform pointA;
    public float pointA_Radius;
    public Transform pointB;
    public float pointB_Radius;
    public int MaxHP;
}

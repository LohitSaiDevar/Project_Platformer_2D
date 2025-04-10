using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "ScriptableObjects/Data")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public int MaxHealthPoints;
    public int MaxDamagePerHit;
}

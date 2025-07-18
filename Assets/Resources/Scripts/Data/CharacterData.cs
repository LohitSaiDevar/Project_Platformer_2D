using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Objects/Data")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public int MaxHP;
    public int MaxAtk;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static Action<int,Vector3> OnPlayerAttack;
    public static Action OnEnemyAttack;
}

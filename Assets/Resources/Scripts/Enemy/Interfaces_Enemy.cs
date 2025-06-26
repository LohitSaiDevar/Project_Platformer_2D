using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Attack();
    void TakeDamage(int dmg, Vector3 playerPos);
}

public interface IDetect
{
    void DetectPlayer();
}

public interface IChaseable
{
    void Chase();
}

public interface IPatrol
{
    void StartPatrol();
    void Patrolling();
}

public interface ISpecialMove
{
    public void SpecialMove1();
    public void SpecialMove2();
    public void UltimateMove();
}

public interface IBarrier
{
    public void CreateBarrier();
}

public interface IBlockable
{
    public void Block();
}
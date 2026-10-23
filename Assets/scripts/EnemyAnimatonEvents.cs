using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatonEvents : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();    
    }

    public void PotisionAfterFlash()
    {
        enemy.PotisionAfterFlash();
    }

    public void AttackDealDamage()
    {
        enemy.AttackDealDamage();
    }

    public void SkillStart()
    {
        enemy.SkillStart();
    }

    public void SkillDuring()
    {
        enemy.SkillDuring();
    }
}

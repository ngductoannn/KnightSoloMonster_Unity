using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Declaration")]
    [SerializeField] private int hp;
    [SerializeField] private Animator animator;

    [Header("Skill")]
    [SerializeField] private float timeSpawnAttack;
    [SerializeField] private float timeSpawnFlash;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleTime();
    }

    private void HandleTime()
    {
        timeSpawnFlash -= Time.deltaTime;
        timeSpawnAttack -= Time.deltaTime;
        if(timeSpawnAttack <= 0)
        {
            Attack();
        }
        else if(timeSpawnFlash <= 0)
        {
            Flash();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        timeSpawnAttack = 3;
    }

    private void Flash()
    {
        animator.SetTrigger("Flash");
        timeSpawnFlash = 5;
    }

    public void PotisionAfterFlash()
    {
        transform.position = new Vector3(Random.Range(-7,7), Random.Range(0.05f, 2), transform.position.z);
    }

    public void AttackDealDamage()
    {
        
    }
    

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            Destroy(gameObject);
    }
}

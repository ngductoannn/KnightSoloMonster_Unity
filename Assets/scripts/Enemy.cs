using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Declaration")]
    [SerializeField] private int hp;
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;
    [SerializeField] private Slider enemyHpBar;

    [Header("Skill")]
    [SerializeField] private float timeSpawnAttack;
    [SerializeField] private float timeSpawnFlash;
    [SerializeField] private float timeSpawnSkill;

    [Header("Attack Damage")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage = 1;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hp = 30;
        enemyHpBar.value = hp;
        timeSpawnSkill = 15;
        timeSpawnAttack = 0.1f;
        timeSpawnFlash = 0;
    }

    private void Update()
    {
        HandleTime();
    }   

    private void HandleTime()
    {
        timeSpawnFlash -= Time.deltaTime;
        timeSpawnAttack -= Time.deltaTime;
        timeSpawnSkill -= Time.deltaTime;
        if(timeSpawnAttack <= 0)
        {
            Attack();
        }
        else if(timeSpawnFlash <= 0)
        {
            Flash();
        }
        else if(timeSpawnSkill <= 0)
        {
            Skill();
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

    private void Skill()
    {
        animator.SetTrigger("Skill");
        timeSpawnSkill = 30;
    }

    public void PotisionAfterFlash()
    {
        transform.position = new Vector3(player.transform.position.x + Random.Range(-3,3) ,player.transform.position.y+2f, transform.position.z);
        if(transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void AttackDealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        foreach (Collider2D hit in hits)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);
        }
    }
                                                                                                    

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        enemyHpBar.value = hp;
        if (hp <= 0)
            SceneManager.LoadScene("Win");
    }

    public void SkillStart()
    {
        transform.position = new Vector3(-0.3f, 2.9f, transform.position.z);
    }

    public void SkillDuring()
    {
        if(hp < 30)
        {
            hp += 1;
        }
        enemyHpBar.value = hp;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class EnemyFighter : MonoBehaviour, IAction
{
    AudioSource audioSource;
    [SerializeField] float enemyAttackCooldown = 1f;
    [SerializeField] float mobRange = 1f;
    [SerializeField] AudioClip attackSound;
    [Range(1, 300)] [SerializeField] float speed = 200f;

    EnemyAIController enemyAIController;
    Animator animator;
    Health targetPlayer;
    Health health;
    BaseStats baseStats;

    float TimeSinceLastAttack = Mathf.Infinity;


    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAIController = GetComponent<EnemyAIController>();
        health = GetComponent<Health>();
        baseStats = GetComponent<BaseStats>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (targetPlayer == null) return;
        if (targetPlayer.IsDead()) return;
        if (health.IsDead()) return;
        
        if (!GetIsInRange(targetPlayer.transform))
        {
            enemyAIController.MoveTo(targetPlayer.transform.position, 1f);
        }

        else
        {
            AttackBehaviour();
            enemyAIController.Cancel();
        }

        UpdateTimers();
    }

    public bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.transform.position) < mobRange;
    }

    private void UpdateTimers()
    {
        TimeSinceLastAttack += Time.deltaTime;
    }

    public void AttackBehaviour()
    {
        enemyAIController.Cancel();
        //attack animasyonunu baslatıcak. Aynı zamanda animasyondaki Hit eventini baslatıcak.
        RotateTowardsPlayer();
        // transform.LookAt(targetPlayer.transform.position);
        if (TimeSinceLastAttack > enemyAttackCooldown)
        {
            animator.SetTrigger("attack");
            TimeSinceLastAttack = 0;
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void PlayAttackSound()
    {
        // audioSource.clip = attackSound;
        // GetComponent<AudioSource>().PlayDelayed(attackSoundDelay);
        if (attackSound != null)
            audioSource.PlayOneShot(attackSound);
        //  GetComponent<AudioSource>().PlayOneShot(attackSound);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 targetDir = (targetPlayer.transform.position - transform.position).normalized;
        targetDir.y = 0.0f;
        var step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPlayer.transform.rotation, step);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void Attack(GameObject combatTarget)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        targetPlayer = combatTarget.GetComponent<Health>(); //targetplayeri setledim.
    }

    public void Cancel()
    {
        targetPlayer = null;
        GetComponent<EnemyAIController>().Cancel();
    }

    public void Hit()
    {
        if (!health.IsDead() && targetPlayer != null)
        {
            targetPlayer.GetComponent<Health>().ApplyDamage(this.gameObject, GetDamage());
        }
    }

    private float GetDamage()
    {
        return GetComponent<BaseStats>().GetBaseStat(Stat.Damage);
    }

    public void ApplyRootMotion()
    {
        animator.applyRootMotion = true;
        print("a");
    }

    public void DisableRootMotion()
    {
        animator.applyRootMotion = false;
        print("b");
    }
}
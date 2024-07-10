using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public EnemyData EnemyData;
    public Transform player;
    public NavMeshAgent agent;
    public Animator anim;
    public float attackRadius;
    public float currDistance;
    public float currHealth;
    public bool isDead;
    public CapsuleCollider Capcollider;
    public Rigidbody rb;
    public bool isAttacking;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = EnemyData.maxHealth;
        
    }
    private void Update()
    {
     
        if (GameManager.instance.WaitingScreen.activeInHierarchy)
        {
            return;
        }
        Movement();
    }
    public void Movement()
    {
        if (isDead)
        {
            Attack(false);
            return;
        }
        currDistance = Vector3.Distance(transform.position, player.position);
        if (currDistance > attackRadius)
        {
            anim.SetBool("isMoving", false);
            Attack(false);
            isAttacking = false;
            return;
        }
        if (Vector3.Distance(player.position, transform.position) > agent.stoppingDistance)
        {
            anim.SetBool("isMoving", true);
            Attack(false);
            isAttacking = false;
            agent.SetDestination(player.position);
        }
        else
        {
            anim.SetBool("isMoving", false);
            Attack(true);
            isAttacking = true;
            transform.LookAt(player.position, Vector3.up);
        }


    }
    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        if (currHealth <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("Dead");
            ZombieManager.instance.UpdateZombieCount();
            rb.useGravity = false;
            Capcollider.enabled = false;
            ZombieManager.instance.RemoveZombieFromWave(gameObject);
            ZombieManager.instance.CheckForWaveEnd();
            Destroy(gameObject, 5);
            return;
        }
        anim.SetTrigger("isHit");
        currHealth -= damage;

    }
    public SphereCollider AttackCollider;
    public void Attack(bool state)
    {
        if (isDead || PlayerControls.instance.isDead)
        {
            anim.SetBool("isAttacking", false);
            isAttacking = false;
            return;
        }
        anim.SetBool("isAttacking", state);
        isAttacking = true;
    }
    public void TakePlayerDamage()
    {
        if (!isAttacking || isDead)
        {
            return;
        }
        PlayerControls.instance.TakeDamage(EnemyData.damageValue);
    }
}
[Serializable]
public class EnemyData
{
    public float moveSpeed;
    public float maxHealth;
    public float damageValue;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform LeftLimit;
    public Transform RightLimit;
    public int attackDamage = 20;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public GameObject hitBox;
    #endregion

    #region  Private Variables

    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion


    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }
        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            SelectTarget();
        }
        if (inRange)
        {
            EnemyLogic();
        }
    }
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {
        if (!Bandit.m_isDead)
        {
            timer = intTimer;
            attackMode = true;
            anim.SetBool("canWalk", false);
            anim.SetBool("Attack", true);
        }
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
    private bool InsideofLimits()
    {
        return transform.position.x > LeftLimit.position.x && transform.position.x < RightLimit.position.x;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, LeftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, RightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = LeftLimit;
        }
        else
        {
            target = RightLimit;
        }
        Flip();
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
}

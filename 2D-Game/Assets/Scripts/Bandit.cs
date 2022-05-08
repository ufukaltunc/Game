using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{
    #region Private Variables
    private Animator anim;
    private Rigidbody2D rb;
    private static Bandit instance;
    private float movementInputDirection;
    private bool isWalking;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool canJump;
    private int amountOfJumpsLeft;
    //private Sensor_Bandit m_groundSensor;
    //private bool m_combatIdle = false;
    #endregion


    #region Public Variables
    [SerializeField] float movementSpeed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
    public ParticleSystem dust;
    public HealthBar healthBar;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    public int amountOfJumps = 1;
    public float variableJumpHeightMultiplier = 0.5f;
    public static bool m_isDead = false;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public int maxHealth = 100;
    int currentHealth;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    #endregion


    // Use this for initialization
    void Awake()
    {
        /*if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);*/
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
        //m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();

        /*
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            anim.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            anim.SetBool("Grounded", m_grounded);
        }

        
        
        
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);


        

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            anim.SetTrigger("Recover");
            currentHealth = maxHealth;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            anim.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }


        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            CreatDust();
            anim.SetTrigger("Jump");
            m_grounded = false;
            anim.SetBool("Grounded", m_grounded);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            anim.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            anim.SetInteger("AnimState", 1);

        //Idle
        else
            anim.SetInteger("AnimState", 0);*/
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurrondings();
    }

    private void CheckSurrondings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(movementInputDirection) > Mathf.Epsilon)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        //Set AirSpeed in animator
        anim.SetFloat("AirSpeed", rb.velocity.y);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }
    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }
    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attackRate;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


            foreach (Collider2D enemy in hitEnemies)
            {
                //Debug.Log("Hit:" + enemy.name);
                enemy.GetComponent<Enemy_behaviour>().TakeDamage(attackDamage);
            }
        }

    }

    public void PlayerTakeDamage(int damage)
    {
        //Player get damage
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }
    void PlayerDie()
    {
        //Player die
        m_isDead = true;
        anim.SetTrigger("Death");

        /*rb.bodyType = RigidbodyType2D.Kinematic;

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f);

        this.enabled = false;*/
    }

    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();

        /*players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            Destroy(players[1]);
        }*/
    }
    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
    void CreatDust()
    {
        dust.Play();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

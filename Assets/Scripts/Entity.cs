using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity:MonoBehaviour 
{

    public string sceName;

    public UnityEngine.SceneManagement.Scene scene;

    #region Component
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackPower=new Vector2(7,12);
    [SerializeField] protected Vector2 knockbackOffset=new Vector2(.5f,2);
    [SerializeField] protected float knockbackDuration=.07f;
    protected bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius=1.2f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance=1;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance=.8f;
    [SerializeField] protected LayerMask whatIsGround;

    public int knockbackDir { get;private set; }
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cd=GetComponent<CapsuleCollider2D>();
        sceName = SceneManager.GetActiveScene().name;
    }
    protected virtual void Update()
    {

    }
    public virtual void SlowEntityBy(float _slowPercentage,float _slowDuration)
    {

    }
    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }
    public virtual void DamageImpact() => StartCoroutine("HitKnockback");
    
    public virtual void SetupKnockbackDir(Transform _damageDirection)
    {
        if (_damageDirection.position.x > transform.position.x)
            knockbackDir = -1;
        else if(_damageDirection.position.x<transform.position.x)
            knockbackDir = 1;
    }
    public void SetupKnockbackPower(Vector2 _knockbackpower)=>knockbackPower = _knockbackpower;
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        float xOffset=Random.Range(knockbackOffset.x,knockbackOffset.y);
        //if(knockbackPower.x>0||knockbackPower.y>0)//make player imune to freeze effect when take hit
            rb.velocity = new Vector2((knockbackPower.x+xOffset) *knockbackDir, knockbackPower.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
        SetupZeroKnockbackPower();
    }
    protected virtual void SetupZeroKnockbackPower()
    {


    }

    private IEnumerator ReturnAfterDelay(float waitTime = 2f)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false); 
    }

    public virtual void ReturnToPool(float waitTime) 
    {
        StartCoroutine(ReturnAfterDelay(waitTime));
        
    }

    #region Velocity
    public void SetZeroVelocity()
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(0, 0);
    } 
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Collision
    public virtual bool isGroundDetected() 
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() 
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance*facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Flip
    public  void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        if(onFlipped!=null)
            onFlipped();
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
    public virtual void SetupDetailFacingDir(int _direction)
    {
        facingDir = _direction;
        if (facingDir == -1)
            facingRight = false;
    }
    public virtual bool Die()
    {
        if (sceName == "Extra")
        {
            ReturnToPool(3f);
            return true;
        }
        else
            if (SaveManager.instance != null && SaveManager.instance.gameData != null)
        {
            string enemyId = this.gameObject.name; 
            SaveManager.instance.gameData.enemiesStatus[enemyId] = true; 
        }

        return true;
    }
}

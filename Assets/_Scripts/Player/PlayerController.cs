using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rg;
    Animator ani;
    [Header("Horizotal")]
    public float Maxspeed = 1.2f;
    public float JumpHight;
    float move;

    public float endTime;

    [Header("Dash")]
    private bool CanDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime = 0.2f;

    private bool facingRight;
    private bool Ground;
    public bool hide;

    [Header("Bash")]
    [SerializeField] private float Radius;
    [SerializeField] GameObject BashAbleObj;
    private bool NearToBashAbleObj;
    private bool IsChosingDir;
    private bool IsBashing;
    [SerializeField] private float BashPower;
    [SerializeField] private float BashTime;
    [SerializeField] private GameObject ArrowBash;
    Vector3 BashDir;
    private float BashTimeReset;

    [Header("Jump")]
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;
    [Header("Jump Wall")]
    public float WallJumpTime = 0.2f;
    public float WallSliedSpeed = 0.3f;
    public float WallDistance = 0.5f;
    public float JumpWallHight;
    private int Jumpwallint;
    bool isWallSiding;
    RaycastHit2D WallCheckHit;
    float JumpTime;
    private bool TouchingWall;
    public LayerMask whatIsGround_Wall;
    //public Transform WallCheck;
    [Header("Health")]
    float autoHealth = 10f;

    private PlayerStats stats;
    public HealthBar HealthBar;
    private GameMaster gm;
    public bool shiled = false;
    public bool End = true;

    public GameObject PressSpace;
    public Transform parentsMagic;
    public GameObject Magic3;
    public GameObject Magic4;

    public Text inthealth;

    public Text SoulText;
    public Text MoneyText;

    public Image CoolDownShield;
    public float coolDown1;
    private bool IsCoolDown = false;
    public Image CoolDownDash;
    public float coolDown2;
    private bool IsCoolDown2 = false;
    public Image CoolDownHealth;
    public float coolDown4;
    private bool IsCoolDown4 = false;
    public float CanAttackUp;
    public float nextAttackUp;
    public float CanAttackDown;
    public float nextAttackDown;

    public GameObject healthparticle;

    private NPC_Controller npc;
    private NPCUpgrade NPCUpgrade;

    public VectorValue StartingPosition;

    public GameObject HitparticleUp;
    public Transform hitUp;
    public GameObject HitparticleDown;
    public Transform hitDown;
    public float JumpHightDown;

    [Header("Attributes SO")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;

    void Start()
    {
        BashTimeReset = BashTime;
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        extraJump = extraJumpValue;
        facingRight = true;
        stats = PlayerStats.instance;
        HealthBar.MaxHealth(playerAttributesSO.maxHealth);
        if (stats.currentHealth < playerAttributesSO.maxHealth)
        {
            HealthBar.SetHealth(stats.currentHealth);
        }
        else
        {
            stats.currentHealth = playerAttributesSO.maxHealth;
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPoint;
        End = true;

        CoolDownShield.fillAmount = 0;
        CoolDownDash.fillAmount = 0;
        CoolDownHealth.fillAmount = 0;

        transform.position = StartingPosition.initialValue;

    }
    void FixedUpdate()
    {
        if(!inUpgrade())
        {
            if (!inDialogue())
            {
                if (isDashing)
                {
                    return;
                }
                Ground = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
                if (End == true)
                {
                    move = Input.GetAxis("Horizontal");
                    ani.SetFloat("Speed", Mathf.Abs(move));
                    if(move >= 0.1f)
                    {
                        hide = true;
                    }
                    if (IsBashing == false)
                    Rg.velocity = new Vector2(move * Maxspeed, Rg.velocity.y);

                    if (move > 0 && !facingRight)
                    {
                        flip();
                    }
                    else if (move < 0 && facingRight)
                    {
                        flip();
                    }
                }
            }
        }

    }
    private void Update()
    {
        if(!inUpgrade())
        {
            if (!inDialogue())
            {
                Jump();
                JumpWall();
                Slide();
                IsShiled();
                Potions();
                attackUp();
                attackDown();
                Bash();
                UpgradeHealth();
                if (playerAttributesSO.UnlockDash == false)
                {
                    CoolDownDash.fillAmount = 1;
                }
                if (playerAttributesSO.UnlockShield == false)
                {
                    CoolDownShield.fillAmount = 1;
                }
                if (isDashing)
                {
                    return;
                }
                if (stats.currentHealth <= 0)
                {
                    ani.SetBool("Death", true);
                    End = false;
                    if (End == false)
                    {
                        endTime += Time.deltaTime;
                        if(endTime >=1.5f)
                        {
                            Time.timeScale = 0f;
                            PressSpace.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                playerAttributesSO.healthcolli = playerAttributesSO.healthcolli - 2;
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                                stats.currentHealth = playerAttributesSO.maxHealth;
                                Time.timeScale = 1f;
                            }
                            if (playerAttributesSO.healthcolli <= 0)
                            {
                                playerAttributesSO.healthcolli = 0;
                            }
                        }
                    }
                }
                else
                {
                    PressSpace.SetActive(false);
                }
                if(stats.currentHealth == playerAttributesSO.maxHealth)
                {
                    HealthBar.MaxHealth(playerAttributesSO.maxHealth);
                }
            }
        }
        inthealth.text = playerAttributesSO.healthcolli.ToString();
        SoulText.text = playerAttributesSO.soulFire.ToString();
        MoneyText.text = playerAttributesSO.money.ToString();

    }
    private void Jump()
    {
        if(End)
        {
            if (Ground == false)
            {
                ani.SetBool("ground", true);
            }
            if (Ground == true)
            {
                extraJump = extraJumpValue;
                ani.SetBool("ground", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
            {
                Rg.velocity = new Vector2(Rg.velocity.x, JumpHight);
                ani.SetBool("ground", true);
                extraJump--;
                FindObjectOfType<AudioManager>().Play("PlayerJump");
                if (extraJump == 0 && Ground == false)
                {
                    ani.SetBool("ground", false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && Ground == true)
            {
                Rg.velocity = new Vector2(Rg.velocity.x, JumpHight);
                ani.SetBool("ground", true);
                hide = true;
                FindObjectOfType<AudioManager>().Play("PlayerJump");
            }
        }
    }
    private void JumpWall()
    {
        if (facingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(WallDistance, 0), WallDistance, whatIsGround_Wall);
            Debug.DrawRay(transform.position, new Vector2(WallDistance, 0), Color.blue);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-WallDistance, 0), WallDistance, whatIsGround_Wall);
        }
        //TouchingWall = Physics2D.OverlapCircle(WallCheck.position, checkRadius, whatIsGround);

        if (WallCheckHit && Ground == false && move != 0)
        {
            isWallSiding = true;
            JumpTime = Time.time + WallJumpTime;
            ani.SetBool("WallSlide", true);
            ani.SetBool("ground", false);
        }
        else if (JumpTime <Time.time)
        {
            isWallSiding = false;
        }
        if(isWallSiding)
        {
            Rg.velocity = new Vector2(Rg.velocity.x, Mathf.Clamp(Rg.velocity.y, WallSliedSpeed, float.MaxValue));
            ani.SetBool("WallSlide", true);
            ani.SetBool("ground", false);
            if (Input.GetKeyDown(KeyCode.Space) && Jumpwallint > 0)
            {
                Jumpwallint = 1;
                Rg.velocity = new Vector2(Rg.velocity.x, JumpWallHight);
            }
        }
        else if( isWallSiding == false)
        {
            Jumpwallint = extraJumpValue;
            ani.SetBool("WallSlide", false);
        }
    }
    void flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    private void Slide()
    {
        if (End)
        {
            if (playerAttributesSO.UnlockDash == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && IsCoolDown2 == false && CanDash)
                {
                    IsCoolDown2 = true;
                    CoolDownDash.fillAmount = 1f;
                    StartCoroutine("StopSlide");
                    if (playerAttributesSO.maxHealth >= 300)
                    {
                        Instantiate(Magic4, transform.position, Quaternion.identity);
                    }
                }
                if (IsCoolDown2)
                {
                    CoolDownDash.fillAmount -= 1f / coolDown2 * Time.deltaTime;
                    if (CoolDownDash.fillAmount <= 0)
                    {
                        CoolDownDash.fillAmount = 0;
                        IsCoolDown2 = false;
                    }
                }
            }
        }
    }
    IEnumerator StopSlide()
    {
        CanDash = false;
        isDashing = true;
        float originalGravity = Rg.gravityScale;
        Rg.gravityScale = 0;
        if(facingRight)
        {
            Rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            ani.SetBool("SkillSlide", true);
            hide = true;
            FindObjectOfType<AudioManager>().Play("Dash");
        }
        else
        {
            Rg.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
            ani.SetBool("SkillSlide", true);
            hide = true;
            FindObjectOfType<AudioManager>().Play("Dash");
        }
        yield return new WaitForSeconds(dashingTime);
        Rg.gravityScale = originalGravity;
        isDashing = false;
        ani.SetBool("SkillSlide", false);
        yield return new WaitForSeconds(coolDown2);
        CanDash = true;
    }
    private void IsShiled()
    {
        if(End)
        {
            if (playerAttributesSO.UnlockShield == true)
            {
                if (Input.GetKeyDown(KeyCode.Q) && IsCoolDown == false)
                {
                    IsCoolDown = true;
                    CoolDownShield.fillAmount = 1f;
                    ani.SetTrigger("Shield");
                    hide = true;
                    FindObjectOfType<AudioManager>().Play("Shield");
                    if(playerAttributesSO.maxHealth >= 300)
                    {
                        Instantiate(Magic3, parentsMagic.position, parentsMagic.rotation);
                    }
                }
                if (IsCoolDown)
                {
                    CoolDownShield.fillAmount -= 1f / coolDown1 * Time.deltaTime;
                    if (CoolDownShield.fillAmount <= 0)
                    {
                        CoolDownShield.fillAmount = 0;
                        IsCoolDown = false;
                    }
                }
            }
        }
    }
    private void attackUp()
    {
        if(End)
        {
            if (Input.GetKey(KeyCode.W) && CanAttackUp >= nextAttackUp && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ani.SetTrigger("Up");
                hide = true;
                CanAttackUp = 0;
                FindObjectOfType<AudioManager>().Play("hit");
            }
            if (CanAttackUp <= 1)
            {
                CanAttackUp += Time.deltaTime;
            }
        }
    }
    public void hitParticleUp()
    {
        Instantiate(HitparticleUp, hitUp.position, hitUp.rotation);
    }
    private void attackDown()
    {
        if (End)
        {
            if (Input.GetKey(KeyCode.S) && CanAttackDown >= nextAttackDown && Ground == false && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ani.SetTrigger("Down");
                hide = true;
                CanAttackDown = 0;
                Rg.velocity = new Vector2(Rg.velocity.x, JumpHightDown);
                FindObjectOfType<AudioManager>().Play("hit");
            }
            if (CanAttackDown <= 1)
            {
                CanAttackDown += Time.deltaTime;
            }
        }
    }
    public void hitParticleDown()
    {
        Instantiate(HitparticleDown, hitDown.position, hitDown.rotation);
    }
    private void Potions()
    {
        if(End)
        {
            if (playerAttributesSO.healthcolli >= 1)
            {
                if (Input.GetKeyDown(KeyCode.F) && IsCoolDown4 == false)
                {
                    IsCoolDown4 = true;
                    CoolDownHealth.fillAmount = 1;
                    FindObjectOfType<AudioManager>().Play("Health");
                    if (stats.currentHealth >= playerAttributesSO.maxHealth)
                    {
                        stats.currentHealth += 0;
                    }
                    else
                    {
                        playerAttributesSO.healthcolli -= 1;
                        stats.currentHealth += playerAttributesSO.MoreHealth;
                        HealthBar.SetHealth(stats.currentHealth);
                        Instantiate(healthparticle, transform.position, Quaternion.identity);
                    }
                    if (stats.currentHealth > playerAttributesSO.maxHealth)
                    {
                        stats.currentHealth = playerAttributesSO.maxHealth;
                    }
                }
                if(IsCoolDown4)
                {
                    CoolDownHealth.fillAmount -= 1 / coolDown4 * Time.deltaTime;
                    if(CoolDownHealth.fillAmount <=0)
                    {
                        CoolDownHealth.fillAmount = 0;
                        IsCoolDown4 = false;
                    }
                }
            }
        }
    }
    void UpgradeHealth()
    {
        if(playerAttributesSO.maxHealth >= 200)
        {
            if(Time.time >= autoHealth)
            {
                autoHealth = Time.time + 10f;
                stats.currentHealth += 20;
                Instantiate(healthparticle, transform.position, Quaternion.identity);
            }
        }
    }
    void Bash()
    {
        if(End)
        {
            if(playerAttributesSO.UnlockBash == true)
            {
                RaycastHit2D[] rays = Physics2D.CircleCastAll(transform.position, Radius, Vector3.forward);
                foreach (RaycastHit2D ray in rays)
                {
                    NearToBashAbleObj = false;
                    if (ray.collider.tag == "Bash")
                    {
                        NearToBashAbleObj = true;
                        BashAbleObj = ray.collider.transform.gameObject;
                        break;
                    }
                }
                if (NearToBashAbleObj)
                {
                    BashAbleObj.GetComponent<SpriteRenderer>().color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Time.timeScale = 0f;
                        BashAbleObj.transform.localScale = new Vector2(1.4f, 1.4f);
                        ArrowBash.SetActive(true);
                        ArrowBash.transform.position = BashAbleObj.transform.transform.position;
                        IsChosingDir = true;
                        ani.SetTrigger("tele");
                        hide = true;
                    }
                    else if (IsChosingDir && Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        Time.timeScale = 1f;
                        BashAbleObj.transform.localScale = new Vector2(1, 1);
                        IsChosingDir = false;
                        IsBashing = true;
                        Rg.velocity = Vector2.zero;
                        //transform.position = BashAbleObj.transform.position;
                        BashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                        BashDir.z = 0;
                        if (BashDir.x > 0 && !facingRight)
                        {
                            flip();
                        }
                        else if (BashDir.x < 0 && facingRight)
                        {
                            flip();
                        }
                        BashDir = BashDir.normalized;
                        BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-BashDir * 9f, ForceMode2D.Impulse);
                        ArrowBash.SetActive(false);
                    }
                }
                else if (BashAbleObj != null)
                {
                    BashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (IsBashing)
                {
                    if (BashTime > 0)
                    {
                        BashTime -= Time.deltaTime;
                        //Rg.velocity = BashDir * BashPower * Time.deltaTime;
                        if (facingRight)
                        {
                            Rg.velocity = new Vector2(Rg.velocity.x + BashPower * Time.deltaTime, Rg.velocity.y);
                        }
                        else
                        {
                            Rg.velocity = new Vector2(Rg.velocity.x + -BashPower * Time.deltaTime, Rg.velocity.y);
                        }
                    }
                    else
                    {
                        IsBashing = false;
                        BashTime = BashTimeReset;
                        Rg.velocity = new Vector2(Rg.velocity.x, 0);
                    }
                }
            }
        }
    }
    private IEnumerator WaitAndSetHideTrue()
    {
        hide = true;
        ani.SetBool("hide", false);
        yield return new WaitForSeconds(2);
        hide = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    public void TakeDamage(int damage)
    {
        if (shiled)
            return;
        stats.currentHealth -= damage;
        HealthBar.SetHealth(stats.currentHealth);
        StartCoroutine(DamageAnimation());
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
    }
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
    private bool inDialogue()
    {
        if (npc != null)
            return npc.DialogueActive();
        else
            return false;
    }
    private bool inUpgrade()
    {
        if (NPCUpgrade != null)
            return NPCUpgrade.UpgradeActivete();
        else
            return false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Controller>();
            if (Input.GetKey(KeyCode.E))
                npc.ActivateDialogue();
        }
        if(collision.gameObject.tag == "NPCUpgrade")
        {
            NPCUpgrade = collision.gameObject.GetComponent<NPCUpgrade>();
            if (Input.GetKey(KeyCode.X))
                NPCUpgrade.ActivateUpgrade();
        }
        if (collision.gameObject.tag == "Hide")
        {
            if(hide)
            {
                StartCoroutine(WaitAndSetHideTrue());
            }
            else
            {
                hide = false;
                ani.SetBool("hide", true);
            }
        }
        if (collision.gameObject.tag == "Cutscenes")
        {
            End = false; 
        }
        if (collision.gameObject.tag == "UnlockDash")
        {
            playerAttributesSO.UnlockDash = true;
            CoolDownDash.fillAmount = 0;
        }
        if (collision.gameObject.tag == "UnlockShield")
        {
            playerAttributesSO.UnlockShield = true;
            CoolDownShield.fillAmount = 0;
        }
        if (collision.gameObject.tag == "UnlockBash")
        {
            playerAttributesSO.UnlockBash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        NPCUpgrade = null;
        End = true;
        if (collision.gameObject.tag == "Hide")
        {
            ani.SetBool("hide", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TRAP")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Controller>();
            if (Input.GetKey(KeyCode.E))
                npc.ActivateDialogue();
        }
        if (collision.gameObject.tag == "NPCUpgrade")
        {
            NPCUpgrade = collision.gameObject.GetComponent<NPCUpgrade>();
            if (Input.GetKey(KeyCode.X))
                NPCUpgrade.ActivateUpgrade();
        }
        if (collision.gameObject.tag == "HP")
        {
            playerAttributesSO.healthcolli += 1;
        }
        if (collision.gameObject.tag == "Soul")
        {
            playerAttributesSO.soulFire += 3;
        }
        if (collision.gameObject.tag == "Money")
        {
            playerAttributesSO.money += 3;
        }
        if (collision.gameObject.tag == "UnlockDash")
        {
            playerAttributesSO.UnlockDash = true;
            CoolDownDash.fillAmount = 0;
        }
        if(collision.gameObject.tag == "UnlockShield")
        {
            playerAttributesSO.UnlockShield = true;
            CoolDownShield.fillAmount = 0;
        }
        if (collision.gameObject.tag == "TRAP")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void StartCutScene()
    {
        End = false;
    }
    public void StopCutScene()
    {
        End = true;
    }
}

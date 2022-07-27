using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rg;
    Animator ani;

    public float Maxspeed;
    public float JumpHight;
    float move;

    private bool CanDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime = 0.2f;

    private bool facingRight;
    private bool Ground;

    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;

    private PlayerStats stats;
    public HealthBar HealthBar;
    private GameMaster gm;
    public bool shiled = false;
    public bool End = true;

    public GameObject PressSpace;

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
    void Start()
    {
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        extraJump = extraJumpValue;
        facingRight = true;
        stats = PlayerStats.instance;
        HealthBar.MaxHealth(stats.maxHealth);
        if(stats.currentHealth < stats.maxHealth)
        {
            HealthBar.SetHealth(stats.currentHealth);
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
                Slide();
                IsShiled();
                Potions();
                attackUp();
                attackDown();
                if(isDashing)
                {
                    return;
                }
                if (stats.currentHealth <= 0)
                {
                    ani.SetBool("Death", true);
                    End = false;
                    PressSpace.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        stats.currentHealth = stats.maxHealth;
                    }
                }
                else
                {
                    PressSpace.SetActive(false);
                }
                if(stats.currentHealth == stats.maxHealth)
                {
                    HealthBar.MaxHealth(stats.maxHealth);
                }
            }
        }
        inthealth.text = stats.healthcolli.ToString();
        SoulText.text = stats.soulFire.ToString();
        MoneyText.text = stats.money.ToString();

    }
    private void Jump()
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
            FindObjectOfType<AudioManager>().Play("PlayerJump");
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
            if (Input.GetKeyDown(KeyCode.E) && IsCoolDown2 == false && CanDash)
            {
                IsCoolDown2 = true;
                CoolDownDash.fillAmount = 1f;
                StartCoroutine("StopSlide");
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
            FindObjectOfType<AudioManager>().Play("Dash");
        }
        else
        {
            Rg.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
            ani.SetBool("SkillSlide", true);
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
            if (Input.GetKeyDown(KeyCode.Q) && IsCoolDown == false)
            {
                IsCoolDown = true;
                CoolDownShield.fillAmount = 1f;
                ani.SetTrigger("Shield");
                FindObjectOfType<AudioManager>().Play("Shield");

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
    private void attackUp()
    {
        if(End)
        {
            if (Input.GetKey(KeyCode.W) && CanAttackUp >= nextAttackUp && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ani.SetTrigger("Up");
                CanAttackUp = 0;
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
                CanAttackDown = 0;
                Rg.velocity = new Vector2(Rg.velocity.x, JumpHightDown);
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
            if (stats.healthcolli >= 1)
            {
                if (Input.GetKeyDown(KeyCode.F) && IsCoolDown4 == false)
                {
                    IsCoolDown4 = true;
                    CoolDownHealth.fillAmount = 1;
                    FindObjectOfType<AudioManager>().Play("Health");
                    if (stats.currentHealth >= stats.maxHealth)
                    {
                        stats.currentHealth += 0;
                    }
                    else
                    {
                        stats.healthcolli -= 1;
                        stats.currentHealth += stats.MoreHealth;
                        HealthBar.SetHealth(stats.currentHealth);
                        Instantiate(healthparticle, transform.position, Quaternion.identity);
                    }
                    if (stats.currentHealth > stats.maxHealth)
                    {
                        stats.currentHealth = stats.maxHealth;
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
            if (Input.GetKey(KeyCode.T))
                npc.ActivateDialogue();
        }
        if(collision.gameObject.tag == "NPCUpgrade")
        {
            NPCUpgrade = collision.gameObject.GetComponent<NPCUpgrade>();
            if (Input.GetKey(KeyCode.X))
                NPCUpgrade.ActivateUpgrade();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        NPCUpgrade = null;
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
        if (collision.gameObject.tag == "HP")
        {
            stats.healthcolli += 1;
        }
        if (collision.gameObject.tag == "Soul")
        {
            stats.soulFire += 1;
        }
        if (collision.gameObject.tag == "Money")
        {
            stats.money += 1;
        }
    }
}

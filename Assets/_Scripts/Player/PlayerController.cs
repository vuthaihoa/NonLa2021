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
    public float SkillSlide;
    float move;

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

    int healthcolli;
    public int MoreHealth=30;
    public int intPotions;
    public Text inthealth;

    public Text SoulText;

    public Image CoolDownShield;
    public float coolDown1;
    private bool IsCoolDown = false;
    public Image CoolDownDash;
    public float coolDown2;
    private bool IsCoolDown2 = false;
    public Image CoolDownHealth;
    public float coolDown4;
    private bool IsCoolDown4 = false;

    public GameObject healthparticle;

    private NPC_Controller npc;
    private NPCUpgrade NPCUpgrade;

    public VectorValue StartingPosition;

    void Start()
    {
        Rg = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        extraJump = extraJumpValue;
        facingRight = true;
        stats = PlayerStats.instance;
        HealthBar.MaxHealth(stats.maxHealth);

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPoint;
        End = true;

        healthcolli = intPotions;

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
        inthealth.text = healthcolli.ToString();
        SoulText.text = stats.soulFire.ToString();


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
            if (Input.GetKeyDown(KeyCode.E) && IsCoolDown2 == false)
            {
                IsCoolDown2 = true;
                CoolDownDash.fillAmount = 1f;
                FindObjectOfType<AudioManager>().Play("Dash");
                if (facingRight)
                {
                    Rg.AddForce(Vector2.right * SkillSlide);
                    ani.SetBool("SkillSlide", true);
                    ani.SetBool("ground", false);
                }
                else
                {
                    Rg.AddForce(Vector2.left * SkillSlide);
                    ani.SetBool("SkillSlide", true);
                    ani.SetBool("ground", false);
                }
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
        yield return new WaitForSeconds(0.2f);
        ani.SetBool("SkillSlide", false);
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
    private void Potions()
    {
        if(End)
        {
            if (healthcolli >= 1)
            {
                if (Input.GetKeyDown(KeyCode.F) && IsCoolDown4 == false)
                {
                    IsCoolDown4 = true;
                    CoolDownHealth.fillAmount = 1;
                    FindObjectOfType<AudioManager>().Play("Health");
                    if (stats.currentHealth >= 100)
                    {
                        stats.currentHealth += 0;
                    }
                    else
                    {
                        healthcolli -= 1;
                        stats.currentHealth += MoreHealth;
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
            healthcolli += 1;
        }
        if (collision.gameObject.tag == "Soul")
        {
            stats.soulFire += 1;
        }
    }
}

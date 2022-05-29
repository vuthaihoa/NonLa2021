using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject visualCube;
    [SerializeField] private GameObject puch_T;
    private Transform player;
    bool FaceingRight = true;
    private float MoveDiretion;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        FilpTowardsPlayer();
    }
    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }
    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            visualCube.SetActive(true);
            puch_T.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag==("Player"))
        {
            visualCube.SetActive(true);
            puch_T.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            visualCube.SetActive(false);
            puch_T.SetActive(false);
        }
    }
    void FilpTowardsPlayer()
    {
        float PlayerPosition = player.position.x - transform.position.x;
        if(PlayerPosition < 0 && FaceingRight)
        {
            Flip();
        }
        else if (PlayerPosition > 0 && !FaceingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        MoveDiretion *= -1;
        FaceingRight = !FaceingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

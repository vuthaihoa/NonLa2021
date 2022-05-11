using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject visualCube;
    [SerializeField] private GameObject puch_T;

    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }
    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
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
}

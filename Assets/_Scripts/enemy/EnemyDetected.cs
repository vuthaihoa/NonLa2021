using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetected : MonoBehaviour
{
    // The radius of the detection sphere
    public float detectionRadius = 10.0f;
    // The layer mask for the player layer
    public LayerMask playerLayer;

    // The transform of the player
    private Transform playerTransform;
    // The Animator component of the enemy
    private Animator enemyAnimator;

    private void Start()
    {
        // Get the Animator component of the enemy
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is within the detection radius
        if (IsPlayerInRange())
        {
            // Notify other enemies if the player is within range
            NotifyOtherEnemies();

            // Set the "PlayerDetected" trigger on the enemy's Animator
            //enemyAnimator.SetTrigger("PlayerDetected");
        }
    }

    // Returns true if the player is within the detection radius
    private bool IsPlayerInRange()
    {
        // Check if the player is within the detection radius using a sphere cast
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, Vector2.zero, 0.0f, playerLayer);
        if (hit.collider != null)
        {
            // Save the transform of the player
            playerTransform = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Notify other enemies that the player has been detected
    private void NotifyOtherEnemies()
    {
        // Find all enemy game objects in the scene
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        // Send a message to all enemy game objects to alert them that the player has been detected
        foreach (GameObject enemy in enemyObjects)
        {
            enemy.SendMessage("OnPlayerDetected", playerTransform, SendMessageOptions.DontRequireReceiver);
        }
    }
}

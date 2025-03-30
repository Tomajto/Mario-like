using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform currentCheckpoint;
    private Health playerHealth;
    //private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        //uiManager = FindObjectOfType<UIManager>();
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
        {
            //uiManager.GameOver();
            Console.WriteLine("No checkpoint set! Cannot respawn correctly.");
            return;
        }

        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
        Console.WriteLine("Respawning at: " + currentCheckpoint.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    private Animator animator;
    private bool doorLocked = true;
    private int health = 3;
    public SpriteRenderer closedDoor;
    public SpriteRenderer openDoor;
    public PlayerMovement playerMovement;

    [SerializeField] private AudioSource keyGrabbedSound;
    [SerializeField] private AudioSource damagedSound;
    [SerializeField] private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        print(health);
    }

    // Update is called once per frame
    void Update()
    {
         if (health <= 0) {
            KillPlayer();
         }
    }

    private void UnlockDoor() 
    {
        doorLocked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            keyGrabbedSound.Play();
            Destroy(other.gameObject);
            UnlockDoor();
            closedDoor.enabled = false;
            openDoor.enabled = true;
        }

        if (other.gameObject.tag == "BottomBorder" || other.gameObject.tag == "Lava")
        {
            KillPlayer();
        }

        if (other.gameObject.tag == "Exit") {
            if (doorLocked) return;
            FindObjectOfType<GameManager>().CompleteLevel();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Damaged();
        }
    }

    private void Damaged() {
        health--;
        damagedSound.Play();
        animator.SetBool("damaged", true);
        print(health);
        Invoke("TurnOffDamageAnimation", 1f);
    }

    private void TurnOffDamageAnimation ()
    {
        animator.SetBool("damaged", false);
    }

    private void KillPlayer(){
        deathSound.Play();
        animator.SetBool("isDead", true);
        FindObjectOfType<GameManager>().EndGame();
        playerMovement.enabled = false;
    }
}

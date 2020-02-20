using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static int playerHealth;
    public Image[] hearts;
    public Sprite fullHealth, emptyHealth;

    public GameObject Player;
    private Animator PlayerAnim;
    private Rigidbody2D player;
    private bool Dead;
    public PhysicsMaterial2D withFricton;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetComponent<Rigidbody2D>();
        PlayerAnim = Player.GetComponent<Animator>(); 
        playerHealth = 3;
        Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead == true)
        {
            return;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].sprite = fullHealth;
            }
            else
            {
                hearts[i].sprite = emptyHealth;
            }

            if (i < 3)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
                playerHealth--;
            }
            if (playerHealth <= 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        PlayerAnim.SetBool("IsDead", true);
        Dead = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.sharedMaterial = withFricton;
        //load overlay screen (game over)
    }
}

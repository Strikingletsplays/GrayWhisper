using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public Transform RespawnPos;

    private HealthSystem PlayersHealth;
    private bool isMoving;

    //for detecting rock movement
    private Transform t;
    private Vector2 lastPosition;

    private void Start()
    {
        PlayersHealth = Player.GetComponent<HealthSystem>();
        t = GetComponent<Transform>();
        lastPosition = t.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player") && !isMoving)
        {
            PlayersHealth.playerHealth -= 1;
            if (PlayersHealth.playerHealth >= 1)
                Player.transform.position = RespawnPos.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.name.Equals("Player"))
            {
                PlayersHealth.playerHealth -= 1;
                if (PlayersHealth.playerHealth >= 1)
                    Player.transform.position = RespawnPos.position;
            }
    }
    private void Update()
    {
       isMoving = Mathf.Abs(lastPosition.x - t.position.x) > 0.1;
    }

}

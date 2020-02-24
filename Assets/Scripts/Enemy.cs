using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public Transform RespawnPos;
    
    private bool isMoving;
    private Transform t;
    private Vector2 lastPosition;

    private void Start()
    {
        t = GetComponent<Transform>();
        lastPosition = t.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            HealthSystem.playerHealth -= 1;
            if (HealthSystem.playerHealth >= 1)
                Player.transform.position = RespawnPos.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving)
        {
            if (collision.gameObject.name.Equals("Player"))
            {
                HealthSystem.playerHealth -= 1;
                if (HealthSystem.playerHealth >= 1)
                    Player.transform.position = RespawnPos.position;
            }
        }
    }
    private void Update()
    {
        isMoving = Mathf.Abs(lastPosition.x - t.position.x) > 0.01;
    }

}

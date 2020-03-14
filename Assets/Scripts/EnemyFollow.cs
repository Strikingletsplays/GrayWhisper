using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform Player;
    private float Distance;
    private SpriteRenderer SpriteRenderer;
    private HealthSystem HealthSystem;

    public float FollowDistance;
    public float speed = 3;
    public Transform RespawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        FollowDistance = 10f;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        HealthSystem = Player.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(Player.position, transform.position);
        if (Distance < FollowDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            if (transform.position.x > Player.position.x)
            {
                //Player is Left
                SpriteRenderer.flipX = true;
            }
            else
            {
                //Player is Right
                SpriteRenderer.flipX = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            HealthSystem.playerHealth--;
            if(HealthSystem.playerHealth != 0) {
                //teleport player
                Player.transform.position = RespawnPoint.position;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}

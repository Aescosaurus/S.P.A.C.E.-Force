using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFox : MonoBehaviour
{
    GameObject player;
    Vector2 playerPosition;
    bool flipped;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition.x > transform.position.x && !flipped)
        {
            sprite.flipX = true;
            flipped = true;
        }
        else if (playerPosition.x < transform.position.x)
        {
            sprite.flipX = false;
            flipped = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, 0.01f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

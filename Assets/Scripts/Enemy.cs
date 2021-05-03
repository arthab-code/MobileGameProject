using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player playerScript;
    private new Rigidbody2D rigidbody2D;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameplayManager.Instance.playerScript;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move(speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player") && playerScript.isBouncing == false)
        {
            if (playerScript.speed <= 0)
                return;

            rigidbody2D.simulated = false;
            playerScript.isBouncing = true;          

            if (!playerScript.isControlling)
            {
                Vector3 tempVector = playerScript.BouncePosition;
                playerScript.BouncePosition = new Vector3(0, playerScript.BouncePosition.y - 0.5f, 0);
                tempVector = playerScript.BouncePosition;
                return;
            }

            if (playerScript.speed < playerScript.MaxSpeed)
                playerScript.speed += (playerScript.speed/20);
        }
    }

    private void Move(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed, 0);
    }
}

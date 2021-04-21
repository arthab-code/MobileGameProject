using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Player playerScript;

    private new Rigidbody2D rigidbody2D;
    private new BoxCollider2D boxCollider2D;
    private Vector3 startPosition;
    private Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerScript = GameplayManager.Instance.playerScript;
        boxCollider2D = GetComponent<BoxCollider2D>();
        startPosition = transform.position;

    }

    private float SpeedGoal()
    {
        float temp = playerScript.MaxSpeed;
        return temp -= playerScript.MaxSpeed / 8;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetMass();
        }
    }
    private void SetMass()
    {
        if (playerScript.speed > SpeedGoal())
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            boxCollider2D.enabled = false;
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0,0);
            //transform.position = startPosition;
            transform.rotation = startRotation;
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
    }
}

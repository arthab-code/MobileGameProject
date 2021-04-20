using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f;
    public float force = 5f;
    public float bounceReturn = 0.5f;
    public float speedDecreaseTime = 1f;
    public bool isPlaying = false;
    public bool isControlling = false;
    public bool isBouncing = false;
    private bool isSavingReturnPosition = false;
    private float maxSpeed;

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }
    

    private Vector3 startPosition;
    private Vector3 bouncePosition;

    public Vector3 BouncePosition
    {
        get
        {
            return bouncePosition;
        }

        set
        {
            bouncePosition = value;
        }
    }

    private new Rigidbody2D rigidbody2D;

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            return rigidbody2D;
        }

        set
        {
            rigidbody2D = value;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MaxSpeed = speed;

        rigidbody2D = GetComponent<Rigidbody2D>();

        GameplayManager.Instance.OnGamePaused += DoPause;
        GameplayManager.Instance.OnGamePlaying += DoPlay;

        rigidbody2D.simulated = false;

        startPosition = new Vector3(transform.position.x + 3f, transform.position.y + 4f, 0);
        bouncePosition = startPosition;

        StartCoroutine(SpeedDecrease());

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (isPlaying == false)
        {
            CheckStartGame(force);
            return;
        }

        if (isControlling == true)
        {
            if (!isSavingReturnPosition)
            {
                BouncePosition = new Vector3(0, transform.position.y+bounceReturn, 0);
                isSavingReturnPosition = true;
            }

            AddForce(-(force * 1.5f));
        }

        if (isBouncing == true)
        {
            if (isControlling == true)
            {
                isSavingReturnPosition = false;
                isControlling = false;
            }

            Bounce(force * 1.5f);
        }
    }

    private IEnumerator SpeedDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedDecreaseTime);

            if (isPlaying)
                speed -= (speed/10);
        }
    }

    private void DoPause()
    {
        rigidbody2D.simulated = false;
    }

    private void DoPlay()
    {
        rigidbody2D.simulated = true;
    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector3(1f * speed, 0, 0);
    }

    private void CheckStartGame(float forceQuantity)
    {
        if (transform.position.x < startPosition.x)
            return;

        if (transform.position.y < startPosition.y)
            AddForce(forceQuantity*2f);
        else
            isPlaying = true;
     
    }

    public void AddForce(float forceQuantity)
    {
        rigidbody2D.AddForce(Vector2.up * forceQuantity, ForceMode2D.Impulse);
    }

    public void Bounce(float forceQuantity)
    {
        if (transform.position.y <= BouncePosition.y)
            AddForce(forceQuantity);
        else
            isBouncing = false;
    }
}

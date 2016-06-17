﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float speedIncrements = 1f;

    private float speed = 0;
    private Vector2 moveDirection = Vector2.up;

    // Variables para los rebotes.
    private bool collisionNow = false;
    private Vector2 vectorColision = new Vector2(0f, 0f);

    // Variables para el sonido
    private AudioSource boing;

    // Use this for initialization
    void Start()
    {
        GameObject ship = GameObject.Find("Ship");
        ShipController shipController = ship.GetComponent<ShipController>();
        shipController.initBall(this);
        // shootBall();

        boing = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(moveDirection * speed * Time.deltaTime);

        if (collisionNow)
        {
            collisionNow = false;

            vectorColision.Normalize();
            this.moveDirection = Vector2.Reflect(this.moveDirection, vectorColision);

            vectorColision = new Vector2(0f, 0f);

            boing.Play();
        }
    }

    // Shoots the ball
    public void shootBall()
    {
        Quaternion initialAngle = Quaternion.Euler(0, 0, Random.Range(-45f, 45f));

        moveDirection = initialAngle * moveDirection;
        speed = minSpeed;
    }

    // Manages collisions with other gameObjects
    public void OnCollisionEnter2D(Collision2D collision)
    {

        this.calculateVector(collision);

        // Si es la primer colisión detectada en este Frame.
        if (!collisionNow)
        {
            this.increaseSpeed();
            collisionNow = true;
        }

        // Si es un bloque lo destruimos
        if ("Block" == collision.gameObject.tag)
        {
            // Destroy(collision.gameObject);
            Block block = collision.gameObject.GetComponent<Block>();
            block.DestroyBlock();
        }
    }

    private void calculateVector(Collision2D collision)
    {
        string tagName = collision.gameObject.tag;
        // Debug.Log("Enter collision with tag: " + tagName);

        Vector2 ballPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        // Debug.Log("Ball position. X=" + ballPosition.x + " Y =" + ballPosition.y);

        if (tagName == "Player")
        {
            // Debug.Log("Collision with Player Ship");
            Vector2 shipPosition = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            vectorColision += (ballPosition - shipPosition);
        }
        else
        {
            // Debug.Log("Enter collision with: " + collision.gameObject.name);
            ContactPoint2D[] contacts = collision.contacts;
            // Debug.Log("Collision contacts points: " + contacts);

            foreach (ContactPoint2D contact in contacts)
            {
                // Debug.Log("Contact point. X=" + contact.point.x + " Y =" + contact.point.y);
                // vectorColision += (ballPosition - contact.point);
                vectorColision += contact.normal;
            }
        }
    }

    private void increaseSpeed()
    {
        if (this.speed < maxSpeed)
        {
            this.speed += speedIncrements;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ball -- Colision con " + collision.gameObject.name);
        string tagName = collision.gameObject.tag;
        Debug.Log("Ball -- Tag: " + tagName);

        if ("Finish" == tagName)
        {
            Debug.Log("Ball -- Se escapó la bola del campo.");
            Destroy(this.gameObject, 0.1f);
        }
    }
}

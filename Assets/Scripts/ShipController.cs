using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public float speed = 1f;

    public Ball ball = null;
    private GameObject ballSpawner;

    private bool leftLimit = false;
    private bool rightLimit = false;

    public float shipWidth;

    // Use this for initialization
    void Start()
    {
        this.calculateShipWidth();
    }

    private void calculateShipWidth()
    {
        shipWidth = 0;
        Renderer[] renders = this.GetComponentsInChildren<Renderer>();
        Debug.Log("calculateShipWidth. renders: " + renders);
        foreach (Renderer render in  renders)
        {
            Debug.Log("calculateShipWidth. render.size: " + render.bounds.size);
            shipWidth += render.bounds.size.x;
        }
        Debug.Log("calculateShipWidth. shipWidth: " + shipWidth);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var shoot = Input.GetButton("Jump");

        // Limit control
        if (horizontal > 0 && rightLimit)
        {
            horizontal = 0;
        }
        else if (horizontal < 0 && leftLimit)
        {
            horizontal = 0;
        }

        if (horizontal != 0)
        {
            this.transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime, Space.World);
            rightLimit = leftLimit = false;
        }

        // Shoot the ball
        if (shoot && this.ball != null)
        {
            ball.shootBall();
            ball = null;
        }

        // Move the ball with the ship
        if (this.ball != null)
        {
            this.ball.transform.position = ballSpawner.transform.position;
        }
    }

    // Manages collisions with other gameObjects
    public void OnCollisionEnter2D(Collision2D collision)
    {
        string objectName = collision.gameObject.name;
        string tagName = collision.gameObject.tag;

        Debug.Log("-- Ship - Enter collision with: " + objectName);
        Debug.Log("-- Ship - Enter collision with tag: " + tagName);

        if (tagName == "Wall")
        {
            this.leftLimit = objectName == "Left";
            this.rightLimit = objectName == "Right";
        }
    }

    public void initBall(Ball ball)
    {
        this.ball = ball;

        if (ballSpawner == null)
        {
            ballSpawner = GameObject.Find("BallSpawner");
        }

        this.ball.transform.position = ballSpawner.transform.position;
    }

    public void newBall(GameObject oldBall)
    {
        GameObject newBallGameObj = Instantiate(oldBall);
        Ball ball = newBallGameObj.GetComponent<Ball>();
        this.initBall(ball);
    }
}

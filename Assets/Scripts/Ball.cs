using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float speedIncrements = 1f;

    private float speed = 0;
    private Vector2 moveDirection = Vector2.up;

    // Variable para la nave
    private ShipController shipController;

    // Variables para los rebotes.
    private bool collisionNow = false;
    private Vector2 vectorColision = new Vector2(0f, 0f);

    // Variables para el sonido
    private AudioSource boing;

    void Awake ()
    {
        GameObject ship = GameObject.Find("Ship");
        shipController = ship.GetComponent<ShipController>();

        boing = this.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        shipController.initBall(this);
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
            collisionNow = true;
        }

        // Si es un bloque lo destruimos
        if ("Block" == collision.gameObject.tag)
        {
            // Destroy(collision.gameObject);
            this.increaseSpeed();
            Block block = collision.gameObject.GetComponent<Block>();
            block.DestroyBlock();
        }
    }

    private void calculateVector(Collision2D collision)
    {
        string tagName = collision.gameObject.tag;
        // Debug.Log("Enter collision with tag: " + tagName);

        // Vector2 ballPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        // Debug.Log("Ball position. X=" + ballPosition.x + " Y =" + ballPosition.y);

        if (tagName == "Player")
        {
            // Vector2 shipPosition = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            // vectorColision += (ballPosition - shipPosition);
            GameObject ship = collision.gameObject;
            Vector2 shipPosition = ship.transform.position;

            ShipController shipCont = ship.GetComponent<ShipController>();

            ContactPoint2D[] contacts = collision.contacts;

            foreach (ContactPoint2D contact in contacts)
            {
                float leftShipPos = shipPosition.x - (shipCont.shipWidth / 2);
                float shipRelativeContact = contact.point.x - leftShipPos;

                // Relative es desde 0 a 1, siendo 0 totalmente a la izquierda y 1 totalmente a la derecha
                float relative = shipRelativeContact / shipCont.shipWidth;

                if (relative < 0.3f)
                {
                    Debug.Log("Ball -- contact Left side.");
                    this.collisionNow = true;
                    float range = (0.3f - relative) / 0.3f;
                    this.updateMoveDirection(range);
                }
                else if (relative >= 0.3f && relative <= 0.7f)
                {
                    Debug.Log("Ball -- contact Center.");
                    vectorColision = contact.normal;
                }
                else
                {
                    if (relative > 1)
                    {
                        relative = 1;
                    }
                    Debug.Log("Ball -- contact Right side.");
                    this.collisionNow = true;
                    float range = (0.7f - relative) / 0.3f;
                    this.updateMoveDirection(range);
                }
            }
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

    private void updateMoveDirection(float range)
    {
        float angle = range * 45;
        Debug.Log("Ball -- updateMoveAngle. angle: " + angle);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        moveDirection = rotation * Vector2.up;
    }

    private void increaseSpeed()
    {
        if (this.speed < maxSpeed)
        {
            this.speed += speedIncrements;
        }
    }

    // Manage collision with floor.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ball -- Colision con " + collision.gameObject.name);
        string tagName = collision.gameObject.tag;
        Debug.Log("Ball -- Tag: " + tagName);

        if ("Finish" == tagName)
        {
            PlayerData.lives--;
            shipController.newBall(this.gameObject);
            Debug.Log("Ball -- Se escapó la bola del campo.");
            Destroy(this.gameObject, 0.1f);
        }
    }
}

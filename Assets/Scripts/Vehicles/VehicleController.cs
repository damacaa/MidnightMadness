using UnityEngine;


public class VehicleController : MonoBehaviour
{
    public static VehicleController selectedVehicle = null;

    public float speed = 10f;
    public float handling = 1f;
    public float drifFactor = 0.95f;
    
    public GameObject[] ligths;
    bool turnedOn = false;


    ///public Light2D light2D;
    public float Speed
    {
        get
        {
            return rb.velocity.magnitude;
        }
    }

    float angle = 0f;

    Rigidbody2D rb;

    public Transform[] seats;
    CharacterController[] passengers;

    public GameObject tireMarkPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        passengers = new CharacterController[seats.Length];

        
    }

    GameObject tireMarkLeft;
    GameObject tireMarkRight;
    public void Move(float x, float y)
    {
        if (y < 0 && Vector2.Dot(rb.velocity, transform.up) > 0.1f)
            return;

        if (!AudioManager.instance.isPlaying("cocheEnMarcha") && rb.velocity.magnitude > 1)
        {
            AudioManager.instance.PlayLoop("cocheEnMarcha");
        }
        if(AudioManager.instance.isPlaying("cocheEnMarcha") && rb.velocity.magnitude < 1)
        {
            AudioManager.instance.Pause("cocheEnMarcha");
        }

        rb.AddForce(transform.up * speed * y, ForceMode2D.Force);

        float turnFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);

        angle += handling * -x * turnFactor * Mathf.Sign(Vector2.Dot(rb.velocity, transform.up));
        rb.MoveRotation(angle);

        float drift = Vector2.Dot(rb.velocity, transform.right);
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * drift;

        //Debug.Log(Mathf.Abs(drift));
        if (Mathf.Abs(drift) >= .9f)
        {
            if (!tireMarkLeft)
            {
                tireMarkLeft = GameObject.Instantiate(tireMarkPrefab, transform);
                tireMarkLeft.transform.localPosition = new Vector2(-0.86f, -2.15f);
            }
            if (!tireMarkRight)
            {
                tireMarkRight = GameObject.Instantiate(tireMarkPrefab, transform);
                tireMarkRight.transform.localPosition = new Vector2(0.86f, -2.15f);
            }
        }
        else
        {
            if (tireMarkLeft)
            {
                tireMarkLeft.transform.parent = null;
                tireMarkLeft = null;
            }

            if (tireMarkRight)
            {
                tireMarkRight.transform.parent = null;
                tireMarkRight = null;
            }
        }

        rb.velocity = forwardVelocity + (drifFactor * rightVelocity);
    }

    public bool GetIn(CharacterController character, out Transform transform)
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if (!passengers[i])
            {
                passengers[i] = character;
                transform = seats[i];
                if (!turnedOn && i == 0)
                    TurnOn();
                return true;
            }
        }

        transform = null;
        return false;
    }

    public void GetOut(CharacterController character)
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] == character)
            {
                passengers[i] = null;
                if (turnedOn && i == 0)
                    TurnOff();
                AudioManager.instance.Stop("cocheEnMarcha");
                return;
            }
        }
    }

    float minVehicleSpeed = 0.1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy) && Speed > minVehicleSpeed)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            //enemy.Stun(5, dir.normalized * Speed * 0.5f);
            enemy.Hurt();
        }
    }

    private void TurnOn()
    {
        turnedOn = true;
        foreach (GameObject light in ligths)
        {
            light.SetActive(true);
        }
    }
    private void TurnOff()
    {
        turnedOn = false;
        foreach (GameObject light in ligths)
        {
            light.SetActive(false);
        }
    }
    public void Explode() { }


}

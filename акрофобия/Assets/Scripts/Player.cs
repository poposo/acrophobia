using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //Floats
    public float speed = 50f;
    public float jumpPower = 250f;
    public float maxSpeed = 3;
    public float distToGround;


    //Stats
    public int curHealth;
    public int maxHealth;
    
    
    //Booleans
    public bool grounded;
    public bool canDoubleJump;
	public bool jumping;

    //References
    private Rigidbody2D rb2d;
    private Animator anim;

	//Cam Control
	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = (GameObject) GameObject.FindWithTag ("MainCamera");//finding camera
        rb2d= GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        maxHealth = 100;
        curHealth = maxHealth;       
	
	}

	public float Limit(double value, double min, double max){//Limiter for Cam Movement
		if (value < min)
			value = min;
		if (value > max)
			value = max;
		return (float) value;	
	}

	// Update is called once per frame
	void Update () {
		
		mainCamera.transform.position = new Vector3(Limit(transform.position.x, -100, 3.4), Limit(transform.position.y, -2.5, 1.8),-5);//moving cam to follow player
        anim.SetBool("Grounded", grounded);
		anim.SetBool ("Jumping", jumping);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

		if (Input.GetButtonDown ("Jump") && grounded)
			jumping = true;
		else
			jumping = false;
		
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
				if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }

                else if (!canDoubleJump && !grounded)
                {
					//Nothing... for now
                }
            }
            
        }




        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
        }

		

    }


    void FixedUpdate()
    {
		
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");


        //Fake Friction
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        //Moving the Player
        rb2d.AddForce(Vector2.right * speed * h);
       
        // Limiting Speeds
        if(rb2d.velocity.x > maxSpeed)
        {
			
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if(rb2d.velocity.x < -maxSpeed)
        {
			
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

		//Cahnging Direction
		if(rb2d.velocity.x > 0.1)
			transform.localScale = new Vector2 (-1, 1);
		else if(rb2d.velocity.x < -0.1)
			transform.localScale = new Vector2 (1, 1);
		
    }

    void Die()
    {
        //To Restart Game uncomment and fix this thing

        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}

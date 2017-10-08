using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public bool grounded = false;
    public Animator anim;

    private Rigidbody2D rb2d;

	/*
     * Peter's addition
     * 
     * The following three events help link the character to both the fuel bar and the health bar.
     * Here is how to use them
     * 
     * 1. Link the Player GameObject to the Fuel Bar and Health Bar in the scene editor!
     * 2. Call HealthChanged and FuelChanged, respectively
     * 3. MAKE SURE you call WillBeRespawned before re-instantiating the character!
     */
	public event HealthChangedEventHandler HealthChanged;
	public event FuelChangedEventHandler FuelChanged;
	public event WillBeRespawnedEventHandler WillBeRespawned;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //grounded = true;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float hMove = Input.GetAxis("Horizontal");

        if (hMove * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * hMove * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (hMove > 0 && !facingRight)
            Flip();
        else if (hMove < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.Play("jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        } else if (!grounded)
        {
            anim.Play("jump");
        }
        else if (facingRight)
        {
            anim.Play("right");
        } else if (!facingRight)
        {
            anim.Play("left");
        }
        

    }

    void Flip()
    {
        facingRight = !facingRight;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
    }
}

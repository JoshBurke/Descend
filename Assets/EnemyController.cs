using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public bool facingRight = true;
    public float moveForce = 600f;
    public float maxSpeed = 5f;
    public float health = 100;
    public float sightDistance = 100f;
    public GameObject eye;
    public GameObject player;

    public Rigidbody2D rb2d;
    public Slider healthSlider;

    private float moveTime = 0;
    private float leftRight = 0;
    private float lastMove = 0;

    private bool idleMoving = false;
    public bool alerted = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!idleMoving && !alerted)
        {
            StartCoroutine(idleMove());
            idleMoving = true;
        } else if (alerted)
        {
            Chase(player);
        }

        playerCheck();
        healthSlider.value = health;

        if(rb2d.velocity.x > 0)
        {
            facingRight = true;
        }
        
        if(rb2d.velocity.x < 0)
        {
            facingRight = false;
        }
	}

    void playerCheck()
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(1,1,1);
            Debug.DrawRay(eye.transform.position, Vector3.right * sightDistance);
            if(Physics2D.Raycast(eye.transform.position, Vector3.right, sightDistance, player.layer))
            {
                Debug.Log("Ray hit!");
                alerted = true;
            } else
            {
                Debug.Log("Ray miss.");
                alerted = false;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.DrawRay(eye.transform.position, Vector3.left * sightDistance);
            if (Physics2D.Raycast(eye.transform.position, Vector3.left, sightDistance, player.layer))
            {
                Debug.Log("Ray hit!");
                alerted = true;
            }
            else
            {
                Debug.Log("Ray miss.");
                alerted = false;
            }
        }
    }

    void Chase(GameObject player)
    {
        Vector3 toPlayer = transform.position - player.transform.position;
        float distance = toPlayer.magnitude;
        toPlayer.Normalize();
        rb2d.AddForce(toPlayer * (moveForce/10 - distance));
    }

    IEnumerator idleMove()
    {
        Debug.Log("idleMoving");
        lastMove = Time.time;
        moveTime = Random.Range(1,2);
        int direction = Random.Range(-1, 2);
        while(direction == 0)
        {
            direction = Random.Range(-1, 2);
        }
        Debug.Log(direction);
        while((Time.time - lastMove) < moveTime)
        {
            rb2d.AddForce(new Vector3(direction, 0, 0) * (moveForce / Random.Range(1,3)));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);

        idleMoving = false;
    }

    void takeDamage(float f)
    {
        if(health - f < 0)
        {
            health = 0;
            die();
        }
        else
        {
            health -= f;
        }
    }

    void moveRandom()
    {
        if(moveTime <= 0)
        {
            moveTime = Random.Range(1,3);
            leftRight = Random.Range(0,2) - 1;
            lastMove = Time.time;
        }
        if(leftRight > 0)
        {
            rb2d.AddForce(Vector2.right * moveForce);
        }
        else
        {
            rb2d.AddForce(Vector2.left * moveForce);
        }

        moveTime -= Time.time - lastMove;
        lastMove = Time.time;
    }

    void die()
    {

    }
}

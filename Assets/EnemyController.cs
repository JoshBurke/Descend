using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public bool facingRight = true;
    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public float health = 100;

    public Rigidbody2D rb2d;
    public Slider healthSlider;

    private float moveTime = 0;
    private float leftRight = 0;
    private float lastMove = 0;

    private bool idleMoving = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!idleMoving)
        {
            StartCoroutine(idleMove());
            idleMoving = true;
        }
        healthSlider.value = health;
	}

    IEnumerator idleMove()
    {
        lastMove = Time.time;
        moveTime = Random.Range(1, 3);
        float direction = Random.Range(-1, 1);
        while((Time.time - lastMove) < moveTime)
        {
            rb2d.AddForce(Vector2.right * direction * moveForce);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3);

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

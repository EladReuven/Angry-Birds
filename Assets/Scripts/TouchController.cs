using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public float force = 100;

    Vector3 changeInDirection;
    Vector3 offset;
    Vector3 startingPos;
    Quaternion startingRot;
    Rigidbody2D rb2D;
    bool thrown = false;
    bool scored = false;
    bool play = true;
    

    private void Awake()
    {
        play = true;
        rb2D = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        startingRot = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (play)
        {
            if(UI.shotsRemaining <= 0)
            {
                play = false;
                EventManager.GameOverM();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!scored)
        {
            EventManager.ScoredAPoint();
        }
        scored = true;
    }

    private void OnMouseDown()
    {
        if(!thrown)
        {
            rb2D.gravityScale = 0;
            Vector3 MouseClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseClick.z = 0;
            offset = MouseClick - transform.position;
            offset.z = 0;
        }
    }

    private void OnMouseDrag()
    {
        if(!thrown)
        {
            Vector3 transformBefore = transform.position;
            transformBefore.z = 0;
            Vector3 newPlayerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPlayerPos.z = 0;
            transform.position = newPlayerPos - offset;
            Vector3 transformAfter = transform.position;
            transformAfter.z = 0;
            if (transformAfter != transformBefore)
            {
                changeInDirection = transformAfter - transformBefore;
            }
            rb2D.velocity = Vector2.zero;
        }
    }

    private void OnMouseUp()
    {
        if(!thrown)
        {
            rb2D.gravityScale = 1;
            rb2D.AddForce(changeInDirection * force, ForceMode2D.Impulse);
            thrown = true;
            EventManager.Shoot();
            StartCoroutine(WaitTilNextShot());
        }
    }

    public IEnumerator WaitTilNextShot()
    {
        yield return new WaitForSeconds(5);
        thrown = false;
        scored = false;
        transform.position = startingPos;
        transform.rotation = startingRot;
        rb2D.gravityScale = 0;
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
    }

}

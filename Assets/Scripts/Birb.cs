using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birb : MonoBehaviour
{
    Vector3 offset;
    Vector3 birdOrigin;
    Vector3 facingDirection;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    float pos1, pos2;
    

    [SerializeField] float maxRadius = 2;
    [SerializeField] float force = 250;
    [SerializeField] float rotateSpeed = 1000;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        facingDirection = transform.position - birdOrigin;
        facingDirection.Normalize();

        StartCoroutine(yDif());
        if(pos1 - pos2 > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, facingDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed);
        }
    }

    private void OnMouseDown()
    {
        birdOrigin = transform.position;
        Vector3 MouseClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseClick.z = 0;
        offset = MouseClick - transform.position;
    }

    private void OnMouseDrag()
    {
        float distance;
        float angle;
        
        Vector3 PlayerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlayerPos.z = 0;

        //calculate the distacnce between current pos and origin(makor)
        distance = Vector3.Distance(PlayerPos - offset, birdOrigin);

        Vector3 newBirdPos = PlayerPos - offset;

        if (distance < maxRadius)
        {
            transform.position = newBirdPos;
        }
        else
        {
            Vector3 radiusBirdPos;
            angle = Mathf.Atan2(PlayerPos.y - birdOrigin.y,PlayerPos.x - birdOrigin.x);
            Debug.Log(angle);
            radiusBirdPos.x = maxRadius * Mathf.Cos(angle);
            radiusBirdPos.y = maxRadius * Mathf.Sin(angle);
            radiusBirdPos.z = 0;
            transform.position = radiusBirdPos;
        }
    }

    private void OnMouseUp()
    {
        //add force to rb here
        rb.gravityScale = 1;
        Vector3 dragVector = transform.position - birdOrigin;
        rb.AddForce(new Vector2(-dragVector.x * force, -dragVector.y * force));
    }

    IEnumerator yDif()
    {
        pos1 = transform.position.y;
        yield return new WaitForEndOfFrame();
        pos2 = transform.position.y;
    }

    [ContextMenu("print pos1 and pos2")]
    public void PrintPos1And2()
    {
        Debug.Log(pos1);
        Debug.Log(pos2);
        Debug.Log(pos1 - pos2);
    }

}

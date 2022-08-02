using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] float iSeconds = 2;
    bool hit = false;
    bool isActive = true;


    private void Update()
    {
        if(hp <= 0)
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D()
    {
        Debug.Log("hit");
        if(!hit)
        {
            hp--;
            StartCoroutine(InvincibilityTimer());
        }
    }

    IEnumerator InvincibilityTimer()
    {
        hit = true;
        yield return new WaitForSeconds(iSeconds);
        hit = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerBirb;

    Vector3 newPos;
    Vector3 startCamPos;
    Vector3 birdPos;
    Vector3 disToMiddleOfScreen;

    bool followBird = false;

    private void Start()
    {
        startCamPos = transform.position;
    }
    private void Update()
    {
        birdPos = playerBirb.transform.position;
        disToMiddleOfScreen = transform.position - birdPos;

        if(!followBird)
        {
            newPos.x = disToMiddleOfScreen.x < 0 ? birdPos.x : transform.position.x;
            newPos.y = disToMiddleOfScreen.y < 0 ? birdPos.y : transform.position.y;
        }
        else
        {
            newPos = birdPos;
        }
        
        if(disToMiddleOfScreen.y >=8)
        {
            followBird = true;
        }

        newPos.z = startCamPos.z;
        transform.position = newPos;
    }
}

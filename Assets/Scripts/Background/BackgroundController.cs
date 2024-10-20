using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Vector2 startPos;
    private float length;
    public GameObject cam;
    public float parallaxEffectX;

    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        length = 12;
    }

    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffectX;
        float distanceY = 0;
        if(cam.transform.position.x != startPos.x)
            distanceY = cam.transform.position.y;
        
        
        float movmentX = cam.transform.position.x * (1 - parallaxEffectX);
        
        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);
        
        
        if (movmentX > startPos.x + length)
        {
            startPos.x += length;
        }
        else if (movmentX < startPos.x - length)
        {
            startPos.x -= length;
        }
    }
}
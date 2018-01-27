using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 movement;
    public float speed = 10f;
    public float smoothing = 5f;    //not used yet

    public int boundary = 50;
    private int screenWidth;
    private int screenHeight;

    public int maxZoomIn = 3;   //only for ortographic cam
    public int maxZoomOut = 8;

    void Awake()
    {
        screenWidth = Screen.width;     //fürs edge pannen
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");       //den Input auf die Axen abfangen (wasd)
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
            MoveWASD(h, v);
        else
            MovePan();
        ScrollMouse();
    }

    private void ScrollMouse()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)     //zoom in
        {
            // Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, maxZoomIn);   //only usable with ortograhpic cam
            Quaternion target = Quaternion.Euler(transform.rotation.x + 20.0f, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 10.0f);  //not working properly.....
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)     //zoom out
        {
            //Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, maxZoomOut);   //only usable with ortograhpic cam
            Quaternion target = Quaternion.Euler(transform.rotation.x - 20.0f, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 10.0f); //not working properly.....
        }
    }

    private void MovePan()
    {
        Vector3 mousePosition = Input.mousePosition;
        float x = mousePosition.x, y = mousePosition.y;
        if (x > screenWidth - boundary && x <= screenWidth)     //x axis move
        {
            movement.Set((x - screenWidth + boundary) / boundary, 0, 0);
            transform.position += movement * speed * Time.deltaTime;
        }
        else if (x < boundary && x >= 0)                        //-x axis move
        {
            movement.Set(-(boundary - x) / boundary, 0, 0);
            transform.position += movement * speed * Time.deltaTime;
        }
        if (y > screenHeight - boundary && y <= screenHeight)   //y axis move
        {
            movement.Set(0, 0, (y - screenHeight + boundary) / boundary);
            transform.position += movement * speed * Time.deltaTime;
        }
        else if (y < boundary && y >= 0)                        //-y axis move
        {
            movement.Set(0, 0, -(boundary - y) / boundary);
            transform.position += movement * speed * Time.deltaTime;
        }
    }

    private void MoveWASD(float h, float v)
    { 
        movement.Set(h, 0f, v);
        movement = movement.normalized;
        movement = movement.normalized * speed * Time.deltaTime * 4f;    //normalizen da man sonst schneller ist wenn man sich diagonal bewegt
        transform.position += movement;
    }
}

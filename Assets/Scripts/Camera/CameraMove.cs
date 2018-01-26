using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 movement;
    public float speed = 6f;
    public float smoothing = 5f;

    public int boundary = 50;
    private int screenWidth;
    private int screenHeight;

    public int maxZoomIn = 3;
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

        MoveWASD(h, v);
        MovePan();
        ScrollMouse();
    }

    private void ScrollMouse()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)     //zoom in
        {
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, maxZoomIn);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)     //zoom out
        {
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, maxZoomOut);
        }
    }

    private void MovePan()
    {
        if (Input.mousePosition.x > screenWidth - boundary)        //x axis move
        {
            movement.Set(1, 0, 0);
            transform.position += movement * speed * Time.deltaTime;
        }
        if (Input.mousePosition.x < 0 + boundary)        //-x axis move
        {
            movement.Set(-1, 0, 0);
            transform.position += movement * speed * Time.deltaTime;
        }
        if (Input.mousePosition.y > screenHeight - boundary)        //y axis move
        {
            movement.Set(0, 0, 1);
            transform.position += movement * speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < 0 + boundary)        //-y axis move
        {
            movement.Set(0, 0, -1);
            transform.position += movement * speed * Time.deltaTime;
        }
    }

    private void MoveWASD(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;    //normalizen da man sonst schneller ist wenn man sich diagonal bewegt

        transform.position += movement;
    }

}

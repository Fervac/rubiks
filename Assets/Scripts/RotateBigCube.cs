﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector3 previousMousePosition;
    Vector3 mouseDelta;

    public GameObject target;

    private float speed = 200f;

    private void Start()
    {
        
    }

    private void Update()
    {
        Swipe();
        Drag();
        CheckInput();
    }

    private void CheckInput()
    {
        if ( Input.GetKeyDown(KeyCode.UpArrow) )
            target.transform.Rotate(90, 0, 0, Space.World);
        if ( Input.GetKeyDown(KeyCode.DownArrow) )
            target.transform.Rotate(0, 0, 90, Space.World);
        if ( Input.GetKeyDown(KeyCode.RightArrow) )
            target.transform.Rotate(0, -90, 0, Space.World);
        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
            target.transform.Rotate(0, 90, 0, Space.World);

        // if (LeftSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(0, 90, 0, Space.World);
        //     }
        //     else if (RightSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(0, -90, 0, Space.World);
        //     }
        //     else if (UpLeftSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(90, 0, 0, Space.World);
        //     }
        //     else if (UpRightSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(0, 0, -90, Space.World);
        //     }
        //     else if (DownLeftSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(0, 0, 90, Space.World);
        //     }
        //     else if (DownRightSwipe(currentSwipe))
        //     {
        //         target.transform.Rotate(-90, 0, 0, Space.World);
        //     }
    }

    private void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - previousMousePosition;
            mouseDelta *= 0.1f;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }
        previousMousePosition = Input.mousePosition;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    private bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    private bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    private bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0f && currentSwipe.x < 0f;
    }

    private bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0f && currentSwipe.x > 0f;
    }

    private bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0f && currentSwipe.x < 0f;
    }

    private bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0f && currentSwipe.x > 0f;
    }
}

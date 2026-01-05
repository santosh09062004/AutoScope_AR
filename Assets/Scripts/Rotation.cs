using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeedmodifier = 0.1f;

    void Update()
    {
        if (Input.touchCount>0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    - touch.deltaPosition.x * rotateSpeedmodifier,
                    0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScreenBoundary : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float colliderWidth;
    
    void Start()
    {
        float edgePos = (colliderWidth * 0.5f) + 0.5f;
        float colliderLength = (colliderWidth * 2f) + 1f;
        
        Vector2 horizontalSize = new Vector2(colliderLength, colliderWidth);
        Vector2 verticalSize = new Vector2(colliderWidth, colliderLength);

        Vector2 offset = new Vector2(0f, edgePos);
        
        for (int angleX = 0; angleX < 360; angleX += 90)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angleX);
            Vector2 rotatedOffset = rotation * offset;
            
            BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
            bool isVertical = angleX % 180 != 0;
            col.size = isVertical ? verticalSize : horizontalSize;
            col.offset = rotatedOffset;
        }
    }

    void FixedUpdate()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        transform.localScale = cam.ScreenToWorldPoint(screenSize) * 2f;
    }
}

using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melange : MonoBehaviour
{
    private InputAction stickAction;
    private float rotation;
    private float previousPosition;

    public int rotationNumber = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stickAction = InputSystem.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        if (stickAction.triggered)
        {
          rotation += Rotate(stickAction.ReadValue<Vector2>());  
        }
        print(rotation);

        if (rotation >= 180f)
        {
            rotationNumber++;
            rotation = 0;
        }
    }

    public float Rotate(Vector2 direction)
    {
        
        float angle = Mathf.Round((Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI)));
        if (angle != previousPosition)
        {
            previousPosition = angle;
            return angle;
        }
        
        return 0f;
    }
    
    public int GetRotationNumber()
    {
        return rotationNumber;
    }
}

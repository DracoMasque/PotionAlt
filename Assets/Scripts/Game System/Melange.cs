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
    public float timerMelange;
    
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
            Rotate(stickAction.ReadValue<Vector2>());  
        }

        if (rotation >= 150f)
        {
            rotationNumber++;
            rotation = 0;
        }
    }

    public void Rotate(Vector2 direction)
    {
        
        float angle = Mathf.Round((Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI)));
        if (angle != previousPosition)
        {
            previousPosition = angle;
            AddRotation(angle);
        }
        timerMelange = 0.8f;
    }

    public void AddRotation(float rotationToAdd)
    {
        rotation += rotationToAdd;
    }
   
}

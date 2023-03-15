using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    void Start()
    {
        
    }
    
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y++; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y--;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x++;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * movementSpeed * Time.deltaTime;


        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
}

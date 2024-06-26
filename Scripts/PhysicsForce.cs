using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsForce : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag;

    private float verticalVelocity;
    public Vector3 Movement => Vector3.up * verticalVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}

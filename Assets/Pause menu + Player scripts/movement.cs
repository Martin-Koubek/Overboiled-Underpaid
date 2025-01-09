using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    public CharacterController controller;

    //hodnoty hr·Ëe
    private float speed;
    public float walk = 12f;
    public float run = 17f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    //gravitace
    public float gravity = -9.81f;

    //hodnoty groundChecku
    public Transform Ground;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool grounded;

    void start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //controla groundu
        grounded = Physics.CheckSphere(Ground.position, groundDistance, groundMask);
        if(grounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        //movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //p˘sobenÌ gravitace na hr·Ëe
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /*//jump
        if(Input.GetButtonDown("Jump")&& grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        //sprint
        if (Input.GetButton("Sprint"))
        {
            speed = run;
        }
        else { speed = walk; }
    }
}

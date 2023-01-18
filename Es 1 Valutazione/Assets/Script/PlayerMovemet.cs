//importo le librerie
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    public CharacterController controller;

    //inizializzo le variabili per la fisica del movimento del player
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    //creo una variabile booleana per controllare se il player ta toccado terra
    bool isGrounded;

    void Update()
    {
        
        //controllo se è a terra controllando il raggio di una sfera situata nella parte bassa del player
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //prendo come input o WASD o le frecce direzionali
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //muovo il player controllando la direzione della telecamera
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //controllo se viene premuto il tasto per saltare e applico la gravità al player
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
//importo le librerie
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    //inizializzo la sesibilità di movimeto a 1000
    public float mouseSensitivity = 1000f;
    
    float xRotation = 0f;

    void Start()
    {
        //blocco il cursore al centro dello schermo quando il gioco è avviato
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //prendo come input la x e la y insieme alla sensibilità e al deltaTime per i diversi framerate
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;

        //utilizzo la tecnica del Clamping per bloccare a 180 gradi la visuale verticale
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);
    }
}
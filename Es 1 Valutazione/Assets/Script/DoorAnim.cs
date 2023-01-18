//importo le librerie
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class DoorAnim : MonoBehaviour
{
    public Transform PlayerCamera;
    public Text text;
    private Animator anim;

    //inizializzo la distanza massima di interazione a 5
    public float MaxDistance = 5;

    //creo una variabile booleana per controllare lo stato della porta
    private bool opened = false;

    private WebSocket ws;

    //creo la variabile che contiene il dato da inviare al server
    public string isOpenable;

    void Start()
    {
        //istanzio un oggetto WebSocket,lo metto in ascolto verso il server e lo connetto
        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) =>
        {
            //Non fuziona la console
            //Debug.Log("Ricevuto: " + e.Data);
        };
        ws.Connect();
    }

    void Update()
    {
        //controllo se viene premuto il tasto F
        if (Input.GetKeyDown(KeyCode.F))
        {
            Pressed();

            //mando al server lo stato della porta
            ws.Send(isOpenable);   
        }
    }

    void Pressed()
    {
        ////creo l'oggetto che fornisce le info del componente colpito
        RaycastHit doorhit;

        //se viene interagito l'oogetto nel range di 5
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {
            //se l'oggetto è del tag "Door" avvio l'animazione della porta e scrivo sul canvas che si può aprire la porta
            if (doorhit.transform.tag == "Door")
            {
                isOpenable = "true";
                text.text = "LA PORTA SI APRE";

                anim = doorhit.transform.GetComponentInParent<Animator>();
                opened = !opened;
                anim.SetBool("Opened", !opened);
            }

            //se l'oggetto è del tag "LockedDoor" scrivo sul canvas che non si può aprire la porta
            else if (doorhit.transform.tag == "LockedDoor")
            {
                isOpenable = "locked";
                text.text = "LA PORTA NON SI APRE";
            }

            //se si interagisce su un altro oggetto scrivo sul canvas che non è stata interagita nessuna porta
            else
            {
                isOpenable = "false";
                text.text = "NON E' UNA PORTA";
            }
        }

        //se non si preme F e non si interagisce niente scrivo sul canvas che non abbiamo interagito nulla
        else
        {
            isOpenable = "null";
            text.text = "NULLA";
        }
        
            
        

            
    }
}
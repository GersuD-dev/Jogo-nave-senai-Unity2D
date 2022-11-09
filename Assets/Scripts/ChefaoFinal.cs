using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefaoFinal : MonoBehaviour
{
    public float velocidade;
    public float limiteX;
    public float limiteY;
    private float cronometro;
    private float tempoMudaDirecao = 3f;
    private Vector2 movimento = new Vector2(0,1);

    public Transform[] posTiroLaser;
    public Transform[] posTiroForte;

    public GameObject tiroLaser;
    public GameObject tiroForte;

    public float tempoEntreTiros;
    private float cronometroTiro;
    private bool podeAtacar;
    private int combo;

    private void Start ()
    {
        combo = 0;
        cronometro = 0;
        podeAtacar = true;
        CalculaMovimento ();
    }

    void Update ()
    {

        Mover ();

        if (podeAtacar)
        { 
            cronometroTiro += Time.deltaTime;
        }



        //Verifica se está na altura definida para ataques
        if (transform.position.y < limiteY)
        {
            //Para a nave verticalmente
            movimento.y = 0;
            GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY;
            

            //Inicia ataques se estiver no tempo     
            if(cronometroTiro > tempoEntreTiros && podeAtacar)
            {
                combo++;
                cronometroTiro = 0;
                if (combo > 5)
                {
                    combo = 0;
                    //Dispara o tiro forte
                    StartCoroutine ("DispararTiroForte");
                }
                else
                {
                    //Dispara tiro Laser
                    DispararTiroLaser ();
                }
            }
        }
    
    }
    private void CalculaMovimento ()
    {
        float x = Random.Range (-1.0f, 1.0f);
        movimento = new Vector2 (x, movimento.y).normalized;
        movimento = movimento * velocidade;
    }

    private void Mover ()
    {
        if (transform.position.x < -limiteX)
        {
            movimento.x = 1;
        }
        else if (transform.position.x > limiteX)
        {
            movimento.x = -1;
        }
        if (!podeAtacar)
        {
            movimento.x = 0;
            GetComponent<Rigidbody2D> ().velocity = movimento;
        }
        else
            GetComponent<Rigidbody2D> ().velocity = movimento;
    }

    public void DispararTiroLaser ()
    {
        if (posTiroLaser.Length == 0) return;

        foreach (var pos in posTiroLaser)
        {
            Instantiate (tiroLaser, pos.position, pos.rotation);
        }
    }

    IEnumerator DispararTiroForte ()
    {
        podeAtacar = false;
        yield return new WaitForSeconds (3);

        foreach (var pos in posTiroForte)
        {
            Instantiate (tiroForte, pos.position, pos.rotation);
        }

        yield return new WaitForSeconds (3);
        podeAtacar = true;
        CalculaMovimento ();
        
    }
}

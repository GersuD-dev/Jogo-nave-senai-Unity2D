using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MonoBehaviour
{
    public float velocidade;
    public float limiteX;
    public float limiteY;
    private float cronometro;
    private float tempoMudaDirecao = 2f;    
    private Vector2 movimento;
    

    private void Start ()
    {
        cronometro = 0;
        CalculaMovimento ();
    }

    void Update ()
    {
        if (GerenciadorJogo.instance.jogadorMorto) return;

        cronometro += Time.deltaTime;
        if(cronometro > tempoMudaDirecao)
        {
            cronometro = 0;
            CalculaMovimento ();
        }

        Mover ();

        //Se saiu da tela, destroi nave inimiga
        if (transform.position.y < limiteY)
        {
            GerenciadorJogo.instance.Inimigos.Remove (gameObject);
            Destroy (gameObject);
        }

    }
    private void CalculaMovimento()
    {
        float x = Random.Range (-1.0f, 1.0f);
        movimento = new Vector2 (x, 1).normalized;
        movimento = movimento * velocidade;
    }

    private void Mover()
    {
        if (transform.position.x < -limiteX)
        {
            movimento.x = 1;
        }
        else if (transform.position.x > limiteX)
        {
            movimento.x = -1;
        }

        GetComponent<Rigidbody2D> ().velocity = movimento;
    }

}

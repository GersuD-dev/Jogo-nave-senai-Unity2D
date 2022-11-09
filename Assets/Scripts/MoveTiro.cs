using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTiro : MonoBehaviour
{
    public float velocidadeTiro;
    public float poderTiro;
    public float tempoVida;
    public string tagAlvo;

    void Start ()
    {
        Destroy (this.gameObject, tempoVida);
        GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, velocidadeTiro);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag (tagAlvo) && col.transform.position.y < 10f)
        {
            col.GetComponent<ControleVida> ().RecebeDano (poderTiro);
            Destroy (gameObject);
        }
    }
}

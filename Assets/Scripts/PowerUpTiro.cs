using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTiro : MonoBehaviour
{

    public float velocidade;

    private void Start ()
    {
        GetComponent<Rigidbody2D> ().velocity = Vector2.down * velocidade;
    }

    private void OnTriggerEnter2D (Collider2D outroObjeto)
    {
        if(outroObjeto.CompareTag("Player"))
        {
            outroObjeto.GetComponent<Disparar> ().SubirNivel ();
            Destroy (this.gameObject);
        }
    }
}

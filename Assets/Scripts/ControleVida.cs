using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleVida : MonoBehaviour
{
    public float vidaAtual;
    public float vidaMaxima;
    public string tagInimigo;
    public string tagPlayer;
    public GameObject itemDropPrefab;

    private void Start ()
    {
        vidaAtual = vidaMaxima;
    }

    public void RecebeDano(float dano)
    {
        //Retira a quantidade de vida conforme o dano
        vidaAtual = Mathf.Clamp (vidaAtual - dano, 0, vidaMaxima);
        
        //Verifica se nave foi destruída
        if(vidaAtual == 0)
        {
            Morte ();
        }
    }

    private void Morte()
    {
        //Caso seja o inimigo
        if (gameObject.CompareTag (tagInimigo))
        {
            if (itemDropPrefab != null)
            {
                float sorteio = Random.Range (0, 100);
                if(sorteio > 98) Instantiate (itemDropPrefab, transform.position, Quaternion.identity);
            }
            GerenciadorJogo.instance.Inimigos.Remove (gameObject);
            Destroy (gameObject);
        }

        //Caso seja o jogador
        if (gameObject.CompareTag (tagPlayer))
        {
            //O que acontece quando jogador morre
            GerenciadorJogo.instance.jogadorMorto = true;
            GerenciadorJogo.instance.botaoVidaExtra.SetActive (true);
            Destroy (gameObject);            
        }
    }


}

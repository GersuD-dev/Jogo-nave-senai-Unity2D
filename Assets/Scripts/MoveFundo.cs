using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFundo : MonoBehaviour
{
    public float velocidade = 0.1f;
    public Renderer imagem;

    private void Update ()
    {
        Vector2 novoOffset = new Vector2 (0, velocidade * Time.deltaTime);
        imagem.material.mainTextureOffset += novoOffset;
    }
}

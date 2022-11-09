using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public enum Levels //Definir quais são os níveis de tiro disponíveis para a nave
    {
        tiroSimples,
        tiroDuplo,
        tiroTriplo,
        tiroQuadruplo,
        tiroMaximo
    }
    public Levels level; //Define qual nível de tiro a nave está

    public Transform[] posTiroLevel1; //Locais que devem ser criados tiros no level 1
    public Transform[] posTiroLevel2; //Locais que devem ser criados tiros no level 2
    public Transform[] posTiroLevel3; //Locais que devem ser criados tiros no level 3
    public Transform[] posTiroLevel4; //Locais que devem ser criados tiros no level 4
    public Transform[] posTiroLevel5; //Locais que devem ser criados tiros no level 5

    public Transform[] posTiro; //Armazena todas as posições possíveis d acordo com o level

    public GameObject tiroPrefab; //Prefab do tiro que deve ser criado ao atirar

    private float tempoEntreTiros;
    private float cronometro;

    private void Start ()
    {
        level = Levels.tiroSimples;
        cronometro = 0;
        tempoEntreTiros = 0.6f;
        posTiro = posTiroLevel1;
    }
    private void Update ()
    {
        cronometro += Time.deltaTime;

        if(cronometro > tempoEntreTiros)
        {
            cronometro = 0;
            Atirar ();
        }
        
    }

    public void Atirar()
    {
        if (posTiro.Length == 0) return;

        for( int indice = 0; indice < posTiro.Length; indice++ )
        {
            Instantiate (tiroPrefab, posTiro[indice].position, posTiro[indice].rotation);
        }
    }

    public void SubirNivel()
    {
        switch (level)
        {
            case Levels.tiroSimples:
                level = Levels.tiroDuplo;
                tempoEntreTiros = 0.5f;
                posTiro = posTiroLevel2;
                break;

            case Levels.tiroDuplo:
                level = Levels.tiroTriplo;
                tempoEntreTiros = 0.4f;
                posTiro = posTiroLevel3;
                break;

            case Levels.tiroTriplo:
                level = Levels.tiroQuadruplo;
                tempoEntreTiros = 0.3f;
                posTiro = posTiroLevel4;
                break;

            case Levels.tiroQuadruplo:
                level = Levels.tiroMaximo;
                tempoEntreTiros = 0.2f;
                posTiro = posTiroLevel5;
                break;

            case Levels.tiroMaximo:
                level = Levels.tiroMaximo;
                tempoEntreTiros = Mathf.Clamp (tempoEntreTiros - 0.05f, 0.1f, tempoEntreTiros);
                posTiro = posTiroLevel5;
                break;
        }
    }

}

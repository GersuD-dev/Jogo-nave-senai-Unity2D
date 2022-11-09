using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour
{

    public enum Dificuldades //Definir quais são os níveis de dificuldade que a fase pode assumir
    {
        MuitoFacil,
        Facil,
        Medio,
        Dificil,
        MuitoDificil,
        ChefaoFinal
    }
    public Dificuldades dificuldade; //Define qual nível de dificuldade da fase
    public static GerenciadorJogo instance;

    public GameObject naveJogadorPrefab; //Prefab da nave do jogador
    public Transform posCriacaoNaveJogador; //Define onde a nave do jogador será criada

    public GameObject[] InimigosPrefab; //Prefabs dos possíveis inimigos
    public float tempoEntreCriacaoInimigos; //tempo entre a criação dos inimigos
    private float cronometro; //Controla o tempo desde a última criação de inimigo
    public float quantMaxInimigosVivos; //Define a quantidade máxima possível de inimigos vivos no jogo

    private float posCriacaoInimigoX; //Define a posição em X que a nave inimiga poderá ser criada
    public float posCriacaoInimigoY; //Define a posição em Y que a nave inimiga poderá ser criada

    public float limitePosCriacaoInimigoX;  //Define o limite em X que a nave inimiga poderá ser criada  

    public List<GameObject> Inimigos = new List<GameObject>(); //Lista de inimigos vivos na fase

    public float tempoEntreCriacaoPowerUps; //tempo entre a criação dos inimigos
    private float cronometroPowerUp; //Controla o tempo desde a última criação de inimigo
    public GameObject[] powerUps;

    public float[] quantidadeInimigosPorDificuldade; //Define quantos inimigos devem ser criados em cada nível de dificuldade
    private float controleInimigosCriados; //Controla a quantidade de inimigo que já foram criados em cada nível de dificuldade


    public GameObject botaoVidaExtra;
    public bool jogadorMorto;
    

    private void Start ()
    {
        instance = this;
        CriaNaveJogador ();
        ConfiguraFase ();
    }

    private void Update ()
    {
        if (jogadorMorto)
        {
            return;
        }

        cronometro += Time.deltaTime;
        if(cronometro > tempoEntreCriacaoInimigos && Inimigos.Count < quantMaxInimigosVivos)
        {
            cronometro = 0;
            CriaInimigo ();
        }


        cronometroPowerUp += Time.deltaTime;
        if (cronometroPowerUp > tempoEntreCriacaoPowerUps)
        {
            cronometroPowerUp = 0;
            CriaPowerUps ();
        }
    }

    public void CriaNaveJogador()
    {
        Instantiate (naveJogadorPrefab, posCriacaoNaveJogador.position, Quaternion.identity);
        jogadorMorto = false;
        botaoVidaExtra.SetActive (false);
    }

    public void CriaInimigo()
    {
        if(InimigosPrefab.Length > 0)
        {
            GameObject inimigo;

            posCriacaoInimigoX = Random.Range (-limitePosCriacaoInimigoX, limitePosCriacaoInimigoX);

            switch (dificuldade)
            {
                case Dificuldades.MuitoFacil:
                    if(controleInimigosCriados >= quantidadeInimigosPorDificuldade[0])
                    {
                        dificuldade = Dificuldades.Facil;
                        ConfiguraFase ();
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[0], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }
                    
                    break;

                case Dificuldades.Facil:
                    if (controleInimigosCriados >= quantidadeInimigosPorDificuldade[1])
                    {
                        dificuldade = Dificuldades.Medio;
                        ConfiguraFase ();
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[1], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }

                    break;

                case Dificuldades.Medio:
                    if (controleInimigosCriados >= quantidadeInimigosPorDificuldade[2])
                    {
                        dificuldade = Dificuldades.Dificil;
                        ConfiguraFase ();
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[2], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }

                    break;

                case Dificuldades.Dificil:
                    if (controleInimigosCriados >= quantidadeInimigosPorDificuldade[3])
                    {
                        dificuldade = Dificuldades.MuitoDificil;
                        ConfiguraFase ();
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[3], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }

                    break;

                case Dificuldades.MuitoDificil:
                    if (controleInimigosCriados >= quantidadeInimigosPorDificuldade[4])
                    {
                        dificuldade = Dificuldades.ChefaoFinal;
                        ConfiguraFase ();
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[4], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }

                    break;

                case Dificuldades.ChefaoFinal:
                    if (controleInimigosCriados >= quantidadeInimigosPorDificuldade[5] && Inimigos.Count == 0)
                    {
                        //COMPLETOU A FASE
                        Debug.Log ("passou de fase!");
                    }
                    else
                    {
                        controleInimigosCriados++;
                        inimigo = Instantiate (InimigosPrefab[5], new Vector2 (posCriacaoInimigoX, posCriacaoInimigoY), Quaternion.identity);
                        Inimigos.Add (inimigo);
                    }

                    break;
            }
        }        

    }

    public void ConfiguraFase()
    {
        cronometro = 0;
        controleInimigosCriados = 0;

        switch (dificuldade)
        {
            case Dificuldades.MuitoFacil:
                quantMaxInimigosVivos = 3f;
                tempoEntreCriacaoInimigos = 1f;
                break;

            case Dificuldades.Facil:
                quantMaxInimigosVivos = 5f;
                tempoEntreCriacaoInimigos = 1.5f;
                break;

            case Dificuldades.Medio:
                quantMaxInimigosVivos = 7f;
                tempoEntreCriacaoInimigos = 2.5f;
                break;

            case Dificuldades.Dificil:
                quantMaxInimigosVivos = 10f;
                tempoEntreCriacaoInimigos = 3f;
                break;

            case Dificuldades.MuitoDificil:
                quantMaxInimigosVivos = 3f;
                tempoEntreCriacaoInimigos = 3f;
                break;

            case Dificuldades.ChefaoFinal:
                quantMaxInimigosVivos = 1f;
                tempoEntreCriacaoInimigos = 0.5f;
                break;
        }
    }

    public void CriaPowerUps()
    {
        if (powerUps.Length > 0)
        {
            float x = Random.Range (-limitePosCriacaoInimigoX, limitePosCriacaoInimigoX);
            Instantiate (powerUps[Random.Range (0, powerUps.Length - 1)], new Vector2 (x, posCriacaoInimigoY), Quaternion.identity);
        }        
    }

}

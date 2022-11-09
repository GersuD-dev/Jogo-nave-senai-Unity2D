using UnityEngine;

public class DisparoInimigo : MonoBehaviour
{
    public enum Dificuldades //Definir quais são os níveis de tiro disponíveis para a nave
    {
        MuitoFacil,
        Facil,
        Medio,
        Dificil,
        MuitoDificil
    }
    public Dificuldades dificuldade; //Define qual nível de tiro a nave está

    public Transform[] posTiroLevel1; //Locais que devem ser criados tiros no level 1
    public Transform[] posTiroLevel2; //Locais que devem ser criados tiros no level 2
    public Transform[] posTiroLevel3; //Locais que devem ser criados tiros no level 3
    public Transform[] posTiroLevel4; //Locais que devem ser criados tiros no level 4
    public Transform[] posTiroLevel5; //Locais que devem ser criados tiros no level 5

    private Transform[] posTiro; //Armazena todas as posições possíveis d acordo com o level

    public GameObject[] tirosPrefab; //Opções de tiro que pdoerão ser utilizados conforme dificuldade do inimigo
    private GameObject tiro; //tiro que este inimigo irá disparar

    private float tempoEntreTiros; //tempo entre tiros do inimigo
    private float cronometro; //Controla o tempo percorrido do último tiro disparado

    private void Start ()
    {
        cronometro = 0;
        DefineDificuldade ();
    }

    private void Update ()
    {
        if (GerenciadorJogo.instance.jogadorMorto) return;

        cronometro += Time.deltaTime;

        if (cronometro > tempoEntreTiros)
        {
            cronometro = 0;
            Atirar ();
        }

    }

    public void Atirar ()
    {
        if (posTiro.Length == 0) return;

        for (int indice = 0; indice < posTiro.Length; indice++)
        {
            Instantiate (tiro, posTiro[indice].position, posTiro[indice].rotation);
        }
    }

    public void DefineDificuldade()
    {
        switch (dificuldade)
        {
            case Dificuldades.MuitoFacil:
                posTiro = posTiroLevel1;
                tiro = tirosPrefab[0];
                tempoEntreTiros = 2;
                break;

            case Dificuldades.Facil:
                posTiro = posTiroLevel2;
                tiro = tirosPrefab[0];
                tempoEntreTiros = 1.6f;
                break;

            case Dificuldades.Medio:
                posTiro = posTiroLevel3;
                tiro = tirosPrefab[0];
                tempoEntreTiros = 1.2f;
                break;

            case Dificuldades.Dificil:
                posTiro = posTiroLevel4;
                tiro = tirosPrefab[0];
                tempoEntreTiros = 0.8f;
                break;

            case Dificuldades.MuitoDificil:
                posTiro = posTiroLevel5;
                tiro = tirosPrefab[1];
                tempoEntreTiros = 0.4f;
                break;
        }
    }
}

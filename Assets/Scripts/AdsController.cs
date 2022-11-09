using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Video;

public class AdsController : MonoBehaviour, IUnityAdsListener
{
    public static AdsController instance;

    public string IdVideoComum;
    public string IdVideoRecompensa;

    public bool assistiuVideo = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Advertisement.AddListener(this);
            Advertisement.Initialize("3767115");
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Advertisement.isShowing)
        {
            Debug.Log("Exibindo anúncio...");
        }
        else
        {
            Debug.Log("Jogando...");
        }
    }

    public void ExibirAnuncioVideo()
    {
        if (Advertisement.IsReady(IdVideoComum))
        {
            Advertisement.Show(IdVideoComum);
        }
    }

    public void ExibirAnuncioVideoRecompensa()
    {
        if (Advertisement.IsReady(IdVideoRecompensa))
        {
            Advertisement.Show(IdVideoRecompensa);
        }
    }



    // Métodos de interface IUnityAdsListener: (Obrigatórios)
    public void OnUnityAdsDidFinish(string tipoVideo, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogWarning("O anúncio não terminou devido a um erro.");
                assistiuVideo = false;
                PlayFabController.PFC.CarregaCena ("Menu");
                break;

            case ShowResult.Skipped:
                // Não recompense o usuário por pular o anúncio.
                Debug.Log("sem recompensa, pois pulou o vídeo");
                assistiuVideo = false;
                PlayFabController.PFC.CarregaCena ("Menu");
                break;

            case ShowResult.Finished:
                // Recompense o usuário por assistir o anúncio até o fim.
                Debug.Log("Parabéns, você recebeu uma recompensa!");
                assistiuVideo = true;
                GerenciadorJogo.instance.CriaNaveJogador ();
                break;
        }
    }

    public void OnUnityAdsReady(string tipoVideo)
    {
        if (tipoVideo == IdVideoComum)
        {
            // Ações opcionais a serem executadas quando o anúncio estiver pronto (por exemplo, habilitar o botão de anúncios premiados)
            Debug.Log("Anúncio pronto para exibir!");
        }
        else if (tipoVideo == IdVideoRecompensa)
        {
            // Ações opcionais a serem executadas quando o anúncio estiver pronto (por exemplo, habilitar o botão de anúncios premiados)
            Debug.Log("Anúncio pronto para exibir!");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Ocorreu um erro
        Debug.Log("Não foi possível exibir o anúncio.");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Ações opcionais a serem executadas quando os usuários finais acionam um anúncio.
        Debug.Log("Anúcio Iniciado!");
    }


}

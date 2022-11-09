using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabController : MonoBehaviour
{   
    public static string PlayFabID; //armazena o ID do Playfab do jogador    
    public string nomeProximaFase; //Define qual cena deve abrir ao efetuar o login no playfab

    public static PlayFabController PFC; //permite o acesso a este script diretamente de outra classe

    void Start()
    {
        if(PFC != null && PFC != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PFC = this;
            DontDestroyOnLoad(this.gameObject);
        }

    
    }

    public void Conectar()
    {
        //VERIFICA SE ESTÁ COM INTERNET
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CarregarJogoDesconectado ();
            return;
        }
        //VERIFICA SE O JOGADOR TEM SEUS DADOS SALVOS, CASO TENHA EFETUA O LOGIN
        else
        {
            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID (request, OnCustomLoginSuccess, OnCustomLoginFailure);
        }
    }
    private void OnCustomLoginFailure(PlayFabError obj)
    {
        Debug.Log("Erro ao conectar com Servidor, tente novamente mais tarde!");
    }

    private void OnCustomLoginSuccess(LoginResult result)
    {
        Debug.Log("CustomLogin efetuado com sucesso");
        CarregarJogoConectado();
    }

    public void GetUserData(string id) 
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
            PlayFabId = PlayFabID,
            Keys = null
        }, result => {
            
            if (result.Data == null || !result.Data.ContainsKey(id)) 
            {
                Debug.Log("Conteúdo vazio!");
            }
                
            else if(result.Data.ContainsKey(id))
            {
                PlayerPrefs.SetString(id,result.Data[id].Value);
            }

        }, (error) => {
            Debug.Log(error.GenerateErrorReport());
        }); 
    }


    public void SetUserData(string id, string valor) 
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
            Data = new Dictionary<string, string>() {
                {id, valor}
            }
        }, 
        result => Debug.Log("Dados atualizados com sucesso!"),
        error => {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void SalvaDisplayName(string nome)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = nome};
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,DisplayNameSucesso,DisplayNameFalhou);
        
    }

    private void DisplayNameFalhou(PlayFabError erro)
    {
        Debug.LogError(erro.GenerateErrorReport());
        
    }

    private void DisplayNameSucesso(UpdateUserTitleDisplayNameResult result)
    {
        PlayerPrefs.SetString("DisplayName",result.DisplayName);
        Debug.Log("Nome de exibição: " + result.DisplayName);
    }

    public void CarregarJogoConectado ()
    {
        //Implementar aqui o que deve ser feito ao terminar o login automatico
        Debug.Log ("Conectado!");
        CarregaCena (nomeProximaFase);
    }

    public void CarregarJogoDesconectado()
    {
        //Implementar aqui o que deve ser feito quando o jogador estiver sem internet
    }

    public void CarregaCena(string nomeCena)
    {
        SceneManager.LoadScene (nomeCena);
    }
}

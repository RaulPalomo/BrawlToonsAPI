using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
//referencia a los modelos
using BrawlToonsAPI.Models;


public class APIManager : MonoBehaviour
{
    //url básica de characters en la api
    private const string ApiBaseUrl = "http://localhost:5000/api/Characters";

    //funciones para llamar a las corrutinas
    public void GetCharacterFunc()
    {
        StartCoroutine(GetCharacter(1));
    }

    public void GetCharactersSortedByWinsFunc()
    {
        StartCoroutine(GetCharactersSortedByWins());
    }
    public void UpdateWinsFunc(int id)
    {
        StartCoroutine(UpdateWins(id));
    }

    public void UpdateLosesFunc(int id)
    {
        StartCoroutine(UpdateLoses(id));
    }

    //Recupera los datos del character por la id
    public IEnumerator GetCharacter(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{ApiBaseUrl}/GET/{id}"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var character = JsonConvert.DeserializeObject<Characters>(request.downloadHandler.text);
                Debug.Log($"Id: {character.character_id}, Name: {character.name}");
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
            
        }
    }

    //Recupera el ranking de los personajes
    public IEnumerator GetCharactersSortedByWins()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:5000/api/Characters/SortedByWins"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                List<Characters> characters = JsonConvert.DeserializeObject<List<Characters>>(request.downloadHandler.text);
                foreach (var character in characters)
                {
                    Debug.Log($"Id: {character.character_id}, Name: {character.name}, Wins: {character.total_wins}");
                }
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }


    //Suma la victoria al total de victorias
    public IEnumerator UpdateWins(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Put($"{ApiBaseUrl}/UpdateWins/{id}", ""))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }

    //Suma la derrota al total de derrotas
    public IEnumerator UpdateLoses(int id)
    {
        using (UnityWebRequest request = UnityWebRequest.Put($"{ApiBaseUrl}/UpdateLoses/{id}", ""))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}
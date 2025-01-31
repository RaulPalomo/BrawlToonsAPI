using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
//Referéncia a las classes modelo
using BrawlToonsAPI.Models;

public class MatchesController : MonoBehaviour
{
    private const string ApiBaseUrl = "http://localhost:5000/api/Matches";

    //Funciones para llamar a las corrutinas
    public void PostMatchFunc(Matches match)
    {
        StartCoroutine(PostMatch(match));
    }

    public void GetMatchFunc(int matchId)
    {
        StartCoroutine(GetMatch(matchId));
    }

    // Implementa el POST de match
    private IEnumerator PostMatch(Matches match)
    {
        string jsonData = JsonConvert.SerializeObject(match);
        using (UnityWebRequest request = new UnityWebRequest($"{ApiBaseUrl}/PostMatch", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }

    // Implementa GET para recuperar una partida
    private IEnumerator GetMatch(int matchId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{ApiBaseUrl}/{matchId}"))
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                var match = JsonConvert.DeserializeObject<Matches>(request.downloadHandler.text);
                Debug.Log($"Match ID: {match.match_id}, Player 1: {match.player_1_id}, Player 2: {match.player_2_id}, Winner: {match.winner_id}, Date: {match.date}");
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}

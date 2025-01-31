using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using BrawlToonsAPI.Models;

public class PlayerCharacterController : MonoBehaviour
{
    private const string ApiBaseUrl = "http://localhost:5000/api/PlayerCharacter";

    public void GetPlayerCharacterStatsFunc(int playerId, int characterId)
    {
        StartCoroutine(GetPlayerCharacterStats(playerId, characterId));
    }

    public void AddPlayerCharacterFunc(PlayerCharacter playerCharacter)
    {
        StartCoroutine(AddPlayerCharacter(playerCharacter));
    }

    public void UpdatePlayerCharacterFunc(PlayerCharacter playerCharacter)
    {
        StartCoroutine(UpdatePlayerCharacter(playerCharacter));
    }

    private IEnumerator GetPlayerCharacterStats(int playerId, int characterId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"{ApiBaseUrl}/GET/{playerId},{characterId}"))
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                var playerCharacter = JsonConvert.DeserializeObject<PlayerCharacter>(request.downloadHandler.text);
                Debug.Log($"Player ID: {playerCharacter.player_id}, Character ID: {playerCharacter.character_id}, Wins: {playerCharacter.wins}, Defeats: {playerCharacter.defeats}");
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }

    private IEnumerator AddPlayerCharacter(PlayerCharacter playerCharacter)
    {
        string jsonData = JsonConvert.SerializeObject(playerCharacter);
        using (UnityWebRequest request = new UnityWebRequest($"{ApiBaseUrl}/POST", "POST"))
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

    private IEnumerator UpdatePlayerCharacter(PlayerCharacter playerCharacter)
    {
        string jsonData = JsonConvert.SerializeObject(playerCharacter);
        using (UnityWebRequest request = new UnityWebRequest($"{ApiBaseUrl}/UPDATE", "PUT"))
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
}

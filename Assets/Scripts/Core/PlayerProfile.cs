using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static event Action<GamePrerequisite> OnPlayerInit;
    public static PlayerProfile Instance { get; private set; }

    public string Id { get; private set; }
    public PlayerProfilePicture ProfilePicture { get; private set; }
   
    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            await InitializeUnityServices();
            Id = AuthenticationService.Instance.PlayerId;
            OnPlayerInit?.Invoke(GamePrerequisite.Player);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private async Task InitializeUnityServices()
    {
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"Failed to sign in anonymously: {ex.Message}");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Request failed: {ex.Message}");
        }
    }
}

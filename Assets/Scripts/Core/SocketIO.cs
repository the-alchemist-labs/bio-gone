using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketIOClient;
using UnityEngine;

public class SocketIO : MonoBehaviour
{
    public static SocketIO Instance { get; private set; }

    public SocketIOUnity Socket { get; private set; }

    public static event Action<GamePrerequisite> OnSocketConnected;

    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            await Initialize();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instance?.Disconnect();
    }

    private async Task Initialize()
    {
        // Force TLS 1.2 for secure connections
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

        Uri uri = new Uri(Consts.ServerURI.Replace("http", "ws"));

        Socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
            Reconnection = true, // Enable reconnection in case of issues
            ReconnectionAttempts = 5,
            EIO = 4 // Use Engine.IO v4 for better performance
        });

        Socket.OnReconnectError += OnReconnectError;

        try
        {
            Debug.Log("Connecting to socket...");
            await Task.Run(async () => await Socket.ConnectAsync());
            SocketConnected();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Socket connection failed: {ex.Message}");
        }
    }

    private void OnReconnectError(object sender, Exception e)
    {
        Debug.LogError($"Socket failed to reconnect: {e.Message}");
    }

    private static void SocketConnected()
    {
        Debug.Log("Socket connected");
        OnSocketConnected?.Invoke(GamePrerequisite.Socket);
    }

    private void Disconnect()
    {
        Socket.Disconnect();
        Socket.Dispose();
    }

    public void RegisterEvent<T>(string eventName, Action<T> handler)
    {
        Socket.On(eventName, response =>
        {
            T parsedResponse = JsonConvert.DeserializeObject<T[]>(response.ToString(), new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                Error = (sender, args) =>
                {
                    Debug.LogError(args.ErrorContext.Error);
                }
            }).First();
            MainThreadDispatcher.Enqueue(() => handler(parsedResponse));
        });
    }

    public void RegisterEvent(string eventName, Action handler)
    {
        Socket.On(eventName, response =>
        {
            MainThreadDispatcher.Enqueue(() => handler());
        });
    }

    public async void EmitEvent<T>(string eventName, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        try
        {
            await Task.Run(async () => await Socket.EmitStringAsJSONAsync(eventName, jsonData));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to send event {eventName}: {ex.Message} {ex.Data} {ex.StackTrace} { ex }");
        }
    }
}
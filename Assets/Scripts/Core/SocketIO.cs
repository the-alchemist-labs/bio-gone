using System;
using System.Collections.Generic;
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
        Uri uri = new Uri($"ws://{Consts.ServerURI}");
        
        Socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        Socket.OnReconnectError += OnReconnectError;

        await Socket.ConnectAsync();
        SocketConnected();
    }

    private void OnReconnectError(object sender, Exception e)
    {
        Debug.LogError("Socket failed to connect");
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

    // Maybe unite to 1 function with handler = null, Action noParamHandler = null
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
        Socket.On(eventName, response => MainThreadDispatcher.Enqueue(handler));
        Socket.On(eventName, response =>
        {
            MainThreadDispatcher.Enqueue(() => handler());
        });
    }
    
    public void EmitEvent<T>(string eventName, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        Socket.EmitAsync(eventName, jsonData);
    }
}
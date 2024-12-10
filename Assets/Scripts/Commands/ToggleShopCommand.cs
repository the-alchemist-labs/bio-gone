
using System;
using Newtonsoft.Json;

[Serializable]
public class ToggleShopCommandPayload
{ 
    public bool IsOpen { get; set; }
    
    public ToggleShopCommandPayload(bool isOpen)
    {
        IsOpen = isOpen;
    }
}

public class ToggleShopCommand : ICommand
{
    public static event Action<bool> OnShopToggled;

    public bool IsOpen { get; }
    
    public ToggleShopCommand(string payloadString)
    {
        ToggleShopCommandPayload payload = JsonConvert.DeserializeObject<ToggleShopCommandPayload>(payloadString);
        IsOpen = payload.IsOpen;
    }
    
    public void Execute()
    {
        OnShopToggled?.Invoke(IsOpen);
    }
}
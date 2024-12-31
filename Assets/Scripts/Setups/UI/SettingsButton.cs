using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private SettingPanel settingsPanel;
    
    public void SettingsClicked()
    {
        settingsPanel.OpenPanel();
    }
}
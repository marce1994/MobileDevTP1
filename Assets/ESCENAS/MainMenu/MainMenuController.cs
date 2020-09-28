using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private List<Button> buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>().ToList();

        Button EasyModeButton = buttons.Single(x => x.name == "SinglePlayer");
        Button HardModeButton = buttons.Single(x => x.name == "MultiPlayer");
        Button SettingsButton = buttons.Single(x => x.name == "Settings");
        Button CreditsButton = buttons.Single(x => x.name == "Credits");
        Button ExitButton = buttons.Single(x => x.name == "Exit");
        Button ImageButton = buttons.Single(x => x.name == "Image");

        ImageButton.Select();

        EasyModeButton.onClick.AddListener(OnEasyModeButton);
        HardModeButton.onClick.AddListener(OnHardModeButton);
        SettingsButton.onClick.AddListener(OnSettingsButton);
        CreditsButton.onClick.AddListener(OnCreditsButton);
        ExitButton.onClick.AddListener(OnExitButton);
    }

    void OnEasyModeButton()
    {
        Config.isHardMode = false;
        Debug.Log("OnEasyModeButton");
        SceneManager.LoadSceneAsync(1);
    }
    void OnHardModeButton()
    {
        Config.isHardMode = true;
        Debug.Log("OnHardModeButton");
        SceneManager.LoadSceneAsync(1);
    }

    void OnSettingsButton()
    {
        var gameobject = SuperPoolManager.Instance.GetGameobject("Settings");
        gameobject.transform.position = Vector3.zero;
        Debug.Log("OnSettingsButton");
    }

    void OnCreditsButton()
    {
        Debug.Log("OnCreditsButton");
        SceneManager.LoadScene(3);
    }

    void OnExitButton()
    {
        Debug.Log("OnExitButton");
        Application.Quit();
    }
}

public static class Config {
    public static bool isHardMode = false;
}

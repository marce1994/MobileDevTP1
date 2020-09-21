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

        Button SinglePlayerButton = buttons.Single(x => x.name == "SinglePlayer");
        Button MultiPlayerButton = buttons.Single(x => x.name == "MultiPlayer");
        Button SettingsButton = buttons.Single(x => x.name == "Settings");
        Button CreditsButton = buttons.Single(x => x.name == "Credits");
        Button ExitButton = buttons.Single(x => x.name == "Exit");
        Button ImageButton = buttons.Single(x => x.name == "Image");

        ImageButton.Select();

        SinglePlayerButton.onClick.AddListener(OnSinglePlayerButton);
        MultiPlayerButton.onClick.AddListener(OnMultiPlayerButton);
        SettingsButton.onClick.AddListener(OnSettingsButton);
        CreditsButton.onClick.AddListener(OnCreditsButton);
        ExitButton.onClick.AddListener(OnExitButton);
    }

    void OnSinglePlayerButton()
    {
        var gameobject = SuperPoolManager.Instance.GetGameobject("SinglePlayer");
        gameobject.transform.position = Vector3.zero;
        Debug.Log("OnSinglePlayerButton");
    }
    void OnMultiPlayerButton()
    {
        SceneManager.LoadSceneAsync(1);
        Debug.Log("OnMultiPlayerButton");
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

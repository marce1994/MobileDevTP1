using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance = null;

    [SerializeField] GameObject popup;

    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<UIManager>();
        }
        return _instance;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {   
            _instance = null;
        }
    }
}

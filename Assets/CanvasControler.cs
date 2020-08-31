using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    public GameObject Creditos;
    void Update()
    {
        if (ConfigControler.instance.GetCreditos())
        {
            Creditos.SetActive(true);
            ConfigControler.instance.SetCreditos(false);
        }
    }
}

using UnityEngine;

public class DisableIfNotAndroid : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
#if (UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR)
        gameObject.SetActive(true);
#endif
    }
}

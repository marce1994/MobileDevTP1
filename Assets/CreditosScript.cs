using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosScript : MonoBehaviour
{
    float timer = 10f;

    void Update()
    {
        timer -= Time.deltaTime;
     
        if(timer<0)
            SceneManager.LoadScene(0);

        transform.position = transform.position + Vector3.up * Time.deltaTime * 100;
    }
}

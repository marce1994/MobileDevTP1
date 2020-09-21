using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecicleOnCameraExit : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        SuperPoolManager.Instance.Recicle(this.gameObject);
    }
}

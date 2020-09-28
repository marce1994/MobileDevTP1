using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEasyMode : MonoBehaviour
{
    private void Awake()
    {
        if (!Config.isHardMode)
        {
            Destroy(gameObject); // no funca el setactive TwT
            return;
        }    

        TaxiComp taxi = GetComponent<TaxiComp>();

        if (taxi == null)
            return;

        taxi.Vel = 9;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

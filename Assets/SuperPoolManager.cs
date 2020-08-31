using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperPoolManager : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> _pool;

    static InputManager instance = null;

    public GameObject[] gameObjects;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }

            return instance;
        }
    }

    public GameObject GetGameobject(string type)
    {
        if (_pool == null)
            _pool = new Dictionary<string, List<GameObject>>();
        if (_pool[type] == null)
            _pool[type] = new List<GameObject>();

        var objectToReturn = _pool[type].FirstOrDefault(x => x.activeSelf == false);
        if (objectToReturn == null)
            objectToReturn = Instantiate(gameObjects.Single(x => x.name == type));
        return objectToReturn;
    }

    public void Recicle(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

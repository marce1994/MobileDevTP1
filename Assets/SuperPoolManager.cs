using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperPoolManager
{
    private Dictionary<string, List<GameObject>> _pool;

    static SuperPoolManager instance = null;

    GameObject[] gameObjects;

    public static SuperPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SuperPoolManager();
                instance.gameObjects = Resources.LoadAll<GameObject>("");
                Debug.Log(instance.gameObjects.Length);
            }

            return instance;
        }
    }

    public GameObject GetGameobject(string type)
    {
        if (_pool == null)
            _pool = new Dictionary<string, List<GameObject>>();
        if (!_pool.ContainsKey(type))
            _pool.Add(type, new List<GameObject>());

        var objectToReturn = _pool[type].FirstOrDefault(x => !x.activeSelf);

        if (objectToReturn == null)
        {
            objectToReturn = GameObject.Instantiate(gameObjects.Single(x => x.name == type));
            _pool[type].Add(objectToReturn);
        }
        
        objectToReturn.SetActive(true);
        var rigidbody = objectToReturn.GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.Sleep();

        return objectToReturn;
    }

    public void Recicle(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

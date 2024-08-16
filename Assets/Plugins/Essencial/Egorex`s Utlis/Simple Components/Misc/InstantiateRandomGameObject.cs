using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class InstantiateRandomGameObject : MonoBehaviour
{
    public List<GameObject> prefabs;

    [SerializeField] bool onAwake = true;
    
    [Foldout("Events")] UnityEvent<GameObject> onInstantiate = new();

    void Awake()
    {
        if (onAwake){
            InstantiateRandom();
        } 
    }

    public void InstantiateRandom()
    {
        var obj = Instantiate(prefabs.Random(), transform);
        onInstantiate.Invoke(obj);
    }
}

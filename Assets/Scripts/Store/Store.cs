using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public List<IStoreObject> GetStoreObjects()
    {
        return new List<IStoreObject>(GetComponentsInChildren<IStoreObject>());
    }
}
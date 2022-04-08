using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T _prefab = null;
    
    public T GetInstance(Vector3 instancePosition)
    {
        return Instantiate(_prefab, instancePosition, Quaternion.identity);
    }
}

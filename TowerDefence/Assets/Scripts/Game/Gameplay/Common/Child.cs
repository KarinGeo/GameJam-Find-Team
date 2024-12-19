using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child<T> : MonoBehaviour
{            
    private T _parent;

    public T Parent { get { return _parent; } }

    public virtual void Init(T parent)
    {
        _parent = parent;
    }
}

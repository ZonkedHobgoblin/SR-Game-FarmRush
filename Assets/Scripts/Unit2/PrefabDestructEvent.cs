using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDestructEvent : MonoBehaviour
{
    public static event Action<GameObject> OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(gameObject);
    }
}

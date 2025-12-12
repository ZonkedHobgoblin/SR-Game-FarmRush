using UnityEngine;
using System.Collections.Generic;

public class DefenseCleanup : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    void Update()
    {
        if (prefabs.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void HandleDestroyed(GameObject obj)
    {
        if (prefabs.Contains(obj))
        {
            prefabs.Remove(obj);
        }
    }
}
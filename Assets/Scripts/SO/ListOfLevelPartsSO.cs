using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListOfLevelPartsSO : ScriptableObject
{
    public List<GameObject> prefabs;

    public GameObject GetRandomPart(GameObject prev) {
        if (prefabs.Count == 0) return null;
        if (prefabs.Count == 1) return prefabs[0];

        int prevIndex = prefabs.IndexOf(prev);

        if (prevIndex == -1) {
            return prefabs[Random.Range(0, prefabs.Count)];
        }

        int index = Random.Range(0, prefabs.Count - 1);

        if (index >= prevIndex) {
            index++;
        }

        return prefabs[index];
    }
}

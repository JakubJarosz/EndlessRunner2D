using System.Collections;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Start() {
        StartCoroutine(Load());
    }

    private IEnumerator Load() {
        yield return new WaitForSeconds(1f);

        SceneLoader.Instance.LoadTargetScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    [SerializeField] private string sceneName;
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("TheEnd");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForSceneLoad());
        }
        SceneManager.LoadScene(sceneName);
    }
}

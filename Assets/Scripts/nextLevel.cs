using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{     
    private IEnumerator WaitForSceneLoad() {
     yield return new WaitForSeconds(3);
     SceneManager.LoadScene("Level2");
     
 }
}

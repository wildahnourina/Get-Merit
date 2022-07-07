using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Back : MonoBehaviour
{
    [SerializeField] private AudioSource klik;
     public void BackToMenu(string sceneName){
        klik.Play();
        SceneManager.LoadScene(sceneName);
    }
}

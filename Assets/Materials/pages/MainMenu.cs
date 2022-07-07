using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource klik;

    public void Play(){
        klik.Play();
        SceneManager.LoadScene("Level1");
    }

    public void Option(){
        klik.Play();
        SceneManager.LoadScene("Options");
    }

    public void About(){
        klik.Play();
        SceneManager.LoadScene("About");
    }

    public void Quit(){
        klik.Play();
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        playButton.onClick.AddListener( () => {
            Loader.Load(Loader.Scene.SampleScene);
        });
        
        quitButton.onClick.AddListener( () => {
            Application.Quit();
        });

        playButton.Select();
    }
}

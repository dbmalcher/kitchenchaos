using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    
    void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
            Time.timeScale = 1f;
        });
    }

    void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e){
        Hide();
    }

    void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e){
        Show();
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}

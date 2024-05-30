using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [Header("Bind Text")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI Gamepad_interactText;
    [SerializeField] private TextMeshProUGUI Gamepad_interactAlternateText;
    [SerializeField] private TextMeshProUGUI Gamepad_pauseText;

    void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        UpdateVisual();
        Show();
    }

    void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e){
        if(KitchenGameManager.Instance.IsCountdownToStartActive()){
            Hide();
        }
    }

    void GameInput_OnBindingRebind(object sender, System.EventArgs e){
        UpdateVisual();
    }

    private void UpdateVisual(){
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        Gamepad_interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        Gamepad_interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlt);
        Gamepad_pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}


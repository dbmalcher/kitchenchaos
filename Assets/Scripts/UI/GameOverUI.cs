using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI recipesDeliveredText;

    void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e){
        if(KitchenGameManager.Instance.IsGameOver()){
            recipesDeliveredText.text = DeliveryManager.Instance.GetSucessfulRecipesAmount().ToString();
            Show();
        } else {
            Hide();
        }
    }

    void Show(){
        gameObject.SetActive(true);
    }

    void Hide(){
        gameObject.SetActive(false);
    }
}

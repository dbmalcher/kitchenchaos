using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private Animator animator;
    private const string POPUP = "popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField]private Color successColor;
    [SerializeField]private Color failedColor;
    [SerializeField]private Sprite sucessSprite;
    [SerializeField]private Sprite failedSprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e){
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = sucessSprite;
        messageText.text = "DELIVERY\nSUCESS";
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e){
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
    }
}

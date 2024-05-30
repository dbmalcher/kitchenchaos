using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField]private Animator animator;
    private int previousCountdownNumber;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e){
        if(KitchenGameManager.Instance.IsCountdownToStartActive()){
            Show();
        } else {
            Hide();
        }
    }

    void Update()
    {
        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countDownNumber.ToString();

        if(previousCountdownNumber != countDownNumber){
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger("numberPopup");
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    void Show(){
        gameObject.SetActive(true);
    }

    void Hide(){
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;
    private Animator animator;
    private const string IS_FLASHING = "isFlashing";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start(){
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        float burnShowProgressAmount = .5f;

        if(stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount){
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

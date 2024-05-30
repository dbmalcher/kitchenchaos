using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
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
    }

    void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        float burnShowProgressAmount = .5f;
        bool isFlashing = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool(IS_FLASHING, isFlashing);
    }
}

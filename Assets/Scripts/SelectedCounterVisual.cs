using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]private ClearCounter clearCounter;
    [SerializeField]private GameObject selectedVisual;
    void Start(){
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e){
        if(e.selectedCounter == clearCounter){
            UpdateSelectedVisual(true);
        } else{
            UpdateSelectedVisual(false);
        }
    }

    void UpdateSelectedVisual(bool condition){
        selectedVisual.SetActive(condition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]private BaseCounter baseCounter;
    [SerializeField]private GameObject[] selectedVisualArray;
    void Start(){
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e){
        if(e.selectedCounter == baseCounter){
            UpdateSelectedVisual(true);
        } else{
            UpdateSelectedVisual(false);
        }
    }

    void UpdateSelectedVisual(bool condition){
        foreach(GameObject selectedVisual in selectedVisualArray){
            selectedVisual.SetActive(condition);
        }
    }
}

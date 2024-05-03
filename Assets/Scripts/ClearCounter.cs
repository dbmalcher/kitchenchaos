using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField]private ClearCounter secondClearCounter;

    public override void Interact(Player player){
        
    }
}

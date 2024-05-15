using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField]private ClearCounter secondClearCounter;

    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //No Kitchen Object on the Counter
            if(player.HasKitchenObject()){
                //PLayer is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
         if(!player.HasKitchenObject()){
            GetKitchenObject().SetKitchenObjectParent(player);
         }
        }
    }
}

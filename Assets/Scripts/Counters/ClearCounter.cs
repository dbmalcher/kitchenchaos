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
         } else {
            //Player is carrying something
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                //Player is holding a plate
                if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                    GetKitchenObject().DestroySelf();
                }
            } else {
            if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                //Counter is holding a plate
                if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().DestroySelf();
                }
            }
         }
        }
    }}
}

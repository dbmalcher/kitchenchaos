using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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

    public override void InteractAlternate(Player player){
        if(HasKitchenObject()){
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}

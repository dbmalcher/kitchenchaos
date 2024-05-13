using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray
    ;
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
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput (KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance {get; private set;}

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax){
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        foreach(RecipeSO waitingRecipeSO in waitingRecipeSOList){
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                //Has the same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList){
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        
                        if(plateKitchenObjectSO == recipeKitchenObjectSO){
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound){
                        //this recipe ingredient was not found on the plate;
                        plateContentMatchesRecipe = false;
                    }
                }
                if(plateContentMatchesRecipe){
                    Debug.Log("correct recipe");
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    return;
                }
            }
        }
        Debug.Log("wrong recipe");
    }

}

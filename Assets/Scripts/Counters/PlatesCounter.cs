using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField]private KitchenObjectSO plateKitchenObjectSO;

    private float spawnSplateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    void Update(){
        spawnSplateTimer += Time.deltaTime;
        if(spawnSplateTimer >= spawnPlateTimerMax){
            spawnSplateTimer = 0f;

            if(platesSpawnedAmount < platesSpawnedAmountMax){
                platesSpawnedAmount ++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            //player is empty handed
            if(platesSpawnedAmount>0){
                //counter has at least 1 plate
                platesSpawnedAmount --;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

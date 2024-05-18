using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }

    void Start(){
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count,0f);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e){
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}

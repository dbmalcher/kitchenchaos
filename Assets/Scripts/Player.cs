using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance{get;private set;}

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangeEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }

    private bool isWalking;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 lastMoveDir;
    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;
    [SerializeField]private Transform kitchenObjectHoldPoint;

    private void Awake(){
        if(Instance != null){
            Debug.LogError("More than one player instance");
        }
        Instance = this;
    }

    private void Start(){
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e){
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;

        HandleInteractions();
    }

    private void Update()
    {
        HandleMovement();
        HighlightCounter();
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        MoveCheck(moveDir);

        isWalking = moveDir != Vector3.zero;
        RotatePlayer(moveDir);
    }

    public bool IsWalking(){
        return isWalking;
    }

    private void MoveCheck(Vector3 moveDir){
        float moveDistance = moveSpeed * Time.deltaTime;

        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if(canMove){
            transform.position +=  moveDir * moveDistance;
        }
        else if(!canMove){
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            bool canMoveX = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMoveX){
                moveDir = moveDirX;
            } else if(!canMoveX){
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                bool canMoveZ = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if(canMoveZ){
                    moveDir = moveDirZ;
                }
            }
        }
    }

    private void RotatePlayer(Vector3 moveDir){
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
    }

    private void HighlightCounter(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        bool interactableObjectHit;

        if(moveDir != Vector3.zero){
            lastMoveDir = moveDir;
        }

        interactableObjectHit = Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHitOutput, interactDistance, countersLayerMask);

        if(interactableObjectHit){
            if(raycastHitOutput.transform.TryGetComponent(out BaseCounter baseCounter)){
                if(baseCounter != selectedCounter){
                    selectedCounter = baseCounter;
                    SetSelectedCounter(baseCounter);
                }
            }
            else{
                selectedCounter = null;
                SetSelectedCounter(null);
            }
        }
        else{
            selectedCounter=null;
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs {
            selectedCounter = selectedCounter
        });
    }

    void HandleInteractions(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        bool interactableObjectHit;

        if(moveDir != Vector3.zero){
            lastMoveDir = moveDir;
        }

        interactableObjectHit = Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHitOutput, interactDistance, countersLayerMask);

        if(interactableObjectHit){
            if(raycastHitOutput.transform.TryGetComponent(out BaseCounter baseCounter)){
                baseCounter.Interact(this);
            }
        }
    }

    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null){
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}

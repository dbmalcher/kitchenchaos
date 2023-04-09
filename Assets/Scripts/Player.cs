using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool isWalking;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
        moveCheck(moveDir);

        isWalking = moveDir != Vector3.zero;
        rotatePlayer(moveDir);
    }

    public bool IsWalking(){
        return isWalking;
    }

    private void moveCheck(Vector3 moveDir){
        float moveDistance = moveSpeed * Time.deltaTime;

        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if(canMove){
            transform.position +=  moveDir * moveDistance;
        }
        else if(!canMove){
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMoveX){
                moveDir = moveDirX;
            } else if(!canMoveX){
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized;
                bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMoveZ){
                    moveDir = moveDirZ;
                }
            }
        }
    }

    private void rotatePlayer(Vector3 moveDir){
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
    }
}

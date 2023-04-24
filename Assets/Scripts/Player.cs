using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPickedSomething;

    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }

    public static Player LocalInstance { get; private set; }


    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private LayerMask collisionsLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private List<Vector3> spawnPositionList;
    [SerializeField] private PlayerVisual playerVisual;

    private bool isWalking;
    private bool isCarrying;

    private Vector3 lastInteractDirection;

    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        GameInputHandler.Instance.OnInteractAction += GameInputHandler_OnInteractAction;
        GameInputHandler.Instance.OnInteractAlternateAction += GameInputHandler_OnInteractAlternateAction;

        PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromClientID(OwnerClientId);
        playerVisual.SetPlayerMaterial(KitchenGameMultiplayer.Instance.GetPlayerMaterial(playerData.materialID));
     }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        { 
            LocalInstance = this;
        }

        transform.position = spawnPositionList[KitchenGameMultiplayer.Instance.GetPlayerDataIndexFromClientID(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if(IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientID)
    {
        if (clientID == OwnerClientId && HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        HandleMovement();

        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInputHandler.Instance.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {                
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                    
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInputHandler.Instance.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = movementSpeed * Time.deltaTime;
        float playerRadius = .6f;
        //float playerHeight = 2f;
        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirection, Quaternion.identity, moveDistance, collisionsLayerMask);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x < -0.5f || moveDirection.x > +0.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirX, Quaternion.identity, moveDistance, collisionsLayerMask);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = (moveDirection.z < -0.5f || moveDirection.z > +0.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirZ, Quaternion.identity, moveDistance, collisionsLayerMask);
                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
                else
                {

                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        isCarrying = HasKitchenObject();                      
        
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsCarrying()
    {
        return isCarrying;
    }    

    private void GameInputHandler_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }        
    }
    private void GameInputHandler_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}

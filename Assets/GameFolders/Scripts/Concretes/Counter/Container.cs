
using UnityEngine;

public class Container : Counter, IContainer
{
    #region Fields
    [SerializeField] private FoodSO _foodToSpawn;
    [SerializeField] private Transform _hatch;
    private SpriteRenderer _containerSprite;
    private Animator _animator;
    private const string _doorTrigger = "GetFood";
    private int collectableId;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (_foodToSpawn == null)
            Debug.LogError("The food to spawn is null");
        _containerSprite = GetComponentInChildren<SpriteRenderer>();
        if (_containerSprite == null)
            Debug.Log("The container could not gather its sprite");
        _containerSprite.sprite = _foodToSpawn.foodSprite;

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.Log("The animator of the container is null");

        collectableId = _foodToSpawn.id;
    }

    public override void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!CheckValidInteraction(playerInteractionHandler))
            return;

        if (IsFilled)
                CollectableMovedToPlayer(playerInteractionHandler);
        else
                HandleEmptyTop(playerInteractionHandler);
    }

    private void HandleEmptyTop(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (playerInteractionHandler.HasFoodOnHand)
            GatherCollectableFromPlayer(playerInteractionHandler);
        else
        {
            SpawnFood();
            CollectableMovedToPlayer(playerInteractionHandler);
        }

    }

    public void SpawnFood()
    {
        if (_foodToSpawn == null)
            return;

        OpenHatch();
        GameObject spawnedObj = PoolManager.Instance.RequestObjFromThePool(collectableId);
        spawnedObj.transform.localScale = Vector3.one;
        spawnedObj.transform.localPosition = CollectablePosition;

        if (spawnedObj.TryGetComponent(out ICollectable collectable))
        {
            int idToAssign = FoodIdGetter.ReturnFoodId(_foodToSpawn.foodName);
            collectable.SetInitials(idToAssign, _foodToSpawn.isProcessable, _foodToSpawn.servableState);   
            FoodSpawned(collectable);
        }
    }

    private void OpenHatch()
    {
        if (_hatch == null)
            return;

        _animator.SetTrigger(_doorTrigger);
    }

    protected override bool CheckValidInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (playerInteractionHandler.HasFoodOnHand && IsFilled)
            return false;
        else
            return true;
    }

}

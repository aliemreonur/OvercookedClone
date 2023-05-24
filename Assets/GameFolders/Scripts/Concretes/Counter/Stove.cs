using UnityEngine;

public class Stove : Counter
{
    #region Fields
    public ICookable cookable => _cookable;
    [SerializeField] private GameObject _stoveOn;
    private ProgressBar _progressBar;
    private const string MEAT = "Meat";
    private ICookable _cookable;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        CookActive(false);
        SetProgressBar();
    }

    public override void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {

        if(IsFilled)
            CollectableMovedToPlayer(playerInteractionHandler);
        else
        {
            if (!CheckValidInteraction(playerInteractionHandler))
                return;
            else if (playerInteractionHandler.currentCollectable.Id != 4 || !playerInteractionHandler.currentCollectable.IsProcessable)
                return;
            GatherCollectableFromPlayer(playerInteractionHandler);
        }

    }

    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        collectable = playerInteractionHandler.currentCollectable;
        collectablePosition = new Vector3(0, 0.3f, 0);
        IsFilled = true;
        collectable.ChangePos(collectablePosition, transform);
        playerInteractionHandler.DropFood();
        _cookable = (ICookable)collectable;

        if (_cookable == null)
        {
            Debug.Log("processable is null");
            return;
        }
        _cookable.PlacedOn(true);
        _progressBar.SetProcessable(_cookable);
        CookingOperation(true);
    }

    public override void CollectableMovedToPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        CookingOperation(false);
        playerInteractionHandler.GatherFood(collectable);
        CounterEmpty();
    }

    private void CookActive(bool isActive)
    {
        _stoveOn.SetActive(isActive);
    }

    private void CookingOperation(bool isOn)
    {
        _progressBar.gameObject.SetActive(isOn);
        IsFilled = isOn;
        CookActive(isOn);

        if (_cookable == null)
            return;

        _cookable.PlacedOn(isOn);
        if (!isOn)
            _cookable.ProcessStopped();
        else
            _cookable.ProcessActive();
      
    }
    private void SetProgressBar()
    {
        _progressBar = GetComponentInChildren<ProgressBar>();
        if (_progressBar == null)
            Debug.Log("The progress bar of the stove is null");
        _progressBar.gameObject.SetActive(false);
    }

}

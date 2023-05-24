using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingCounter : Counter, IProcessor
{
    #region Fields
    [SerializeField] private ProgressBar _progressBar;
    public bool IsCutting => _isCutting;
    public ICuttable iCuttable => _iCuttable;

    private ICuttable _iCuttable;
    private bool _isCutting;
    private Animator _animator;
    private const string ISCUTTING = "IsCutting";
    #endregion

    protected override void Awake()
    {
        base.Awake();
        collectablePosition = new Vector3(-0.15f, 1.3f, 0);
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.LogError("The animator is null");
        _progressBar.gameObject.SetActive(false);
    }

    public override void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!CheckValidInteraction(playerInteractionHandler))
            return;

        if (IsFilled)
            CollectableMovedToPlayer(playerInteractionHandler);
        else if (CheckForValidFood(playerInteractionHandler))
            GatherCollectableFromPlayer(playerInteractionHandler);
    }

    public override void HandleAlternateInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (collectable == null)
            return;

        _iCuttable = (ICuttable)collectable;
        if (_iCuttable == null)
            return;
        _progressBar.SetProcessable(_iCuttable);
        _iCuttable.OnProcessFinished += ResetCutting;
        _iCuttable.Cut(playerInteractionHandler);
        if(!_isCutting)
        {
            CuttingActive(true);
        }

        playerInteractionHandler.OnAlternateEnd += ResetCutting;
    }

    private void ResetCutting()
    {
        if (!_isCutting)
            return;

        CuttingActive(false);
    }

    private void CuttingActive(bool isCutOn)
    {
        _animator.SetBool(ISCUTTING, isCutOn);
        _progressBar.gameObject.SetActive(isCutOn);
        _isCutting = isCutOn;
        if (isCutOn == false)
            _iCuttable.ProcessStopped();
    }

    private bool CheckForValidFood(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!playerInteractionHandler.HasFoodOnHand)
            return false;

       if (!playerInteractionHandler.currentCollectable.IsProcessable || playerInteractionHandler.currentCollectable.Id == 4)
            return false;
        else
            return true;
    }

}


public class Cutable : Processable, ICuttable
{
    private IPlayerInteractionHandler _playerInteractionHandler;
    override protected void Update()
    {
        base.Update();
        if (!CheckForPlayerInteraction() || isProcessActive)
            return;
        isProcessActive = CheckProcessedTime();
    }

    public void Cut(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!CheckProcessable() || !playerInteractionHandler.IsAlternateInteracting)
            return;

        _playerInteractionHandler = playerInteractionHandler;
        ProcessActive();
    }

    private bool CheckForPlayerInteraction()
    {
        if (_playerInteractionHandler == null)
            return false;
        else if (!_playerInteractionHandler.IsAlternateInteracting)
            return false;
        else
            return true;
    }

}

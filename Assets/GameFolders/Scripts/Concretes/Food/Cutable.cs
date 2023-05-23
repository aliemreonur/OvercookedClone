

public class Cutable : Processable, ICuttable
{

    private IPlayerInteractionHandler _playerInteractionHandler;


    override protected void Update()
    {
        base.Update();
        if (!CheckForPlayerInteraction() || isProcessActive)
            return;

        //if auto processed, we need to check if its placed on the stove
        //else, we need to check if its placed on a chopping board

        isProcessActive = CheckProcessedTime();

    }

    public void Cut(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (!CheckProcessable() || !playerInteractionHandler.IsAlternateInteracting)
            return;

        _playerInteractionHandler = playerInteractionHandler;
        ProcessActive();
      
    }

    //not shared
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

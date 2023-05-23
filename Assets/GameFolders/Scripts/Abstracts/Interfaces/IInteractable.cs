using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Select();
    void DeSelect();
    void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler);
    void HandleAlternateInteraction(IPlayerInteractionHandler playerInteractionHandler);
}

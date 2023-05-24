
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private GameObject _interactableIndicator;

    protected virtual void Awake()
    {        
        _interactableIndicator = transform.GetChild(1).gameObject;
        if (_interactableIndicator == null)
            Debug.LogError("this + " + gameObject.name + " could not gather its selected object");
    }
    
    public virtual void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        //this will be used for pickup and drop the collectable to the floor when no interaction
    }

    public virtual void HandleAlternateInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {
        //this will be for throwing object when no interacted obj
    }

    public void Select()
    {
        ObjectSelected(true);
    }

    public void DeSelect()
    {
        ObjectSelected(false);
    }

    private void ObjectSelected(bool isSelected)
    {
        _interactableIndicator.SetActive(isSelected);
    } 
}

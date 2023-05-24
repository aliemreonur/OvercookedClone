
using UnityEngine;

public interface ICollectable : IEntityController, IThrowable, ITrashable
{

    public bool IsProcessable { get; }
    public bool IsReadyToServe { get;  }
    public int Id { get; }
    void SetInitials(int id, bool isProcessable, int serveState);
    void ChangePos(Vector3 posToMove, Transform parentTransform);
    void Pickup(IPlayerInteractionHandler playerInteractionHandler);
    void StateUpdated();
    void DeActivate();
}

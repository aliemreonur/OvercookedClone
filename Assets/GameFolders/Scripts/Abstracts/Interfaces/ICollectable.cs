using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICollectable : IThrowable, IEntityController
{
    public event Action OnTrashed;
    public bool IsProcessable { get; }
    public bool IsReadyToServe { get;  }
    public int Id { get; } //alternatively we could assign if they are cuttable and cookable on this method.
    void SetInitials(int id, bool isProcessable, int serveState);
    void ChangePos(Vector3 posToMove, Transform parentTransform);
    void Pickup(IPlayerInteractionHandler playerInteractionHandler);
    void StateUpdated();
    void Trashed();
    void DeActivate();
}

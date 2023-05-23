using System;
using UnityEngine;

public interface ICuttable : ICollectable, IProcessable
{
    void Cut(IPlayerInteractionHandler playerInteractionHandler);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController : IEntityController
{
  IPlayerInteractionHandler playerInteractionHandler { get; }
}

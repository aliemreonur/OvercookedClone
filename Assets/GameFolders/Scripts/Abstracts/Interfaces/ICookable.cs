using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICookable : IProcessable
{
    //bool IsBurnt { get; }
    //void Cook();
    void PlacedOn(bool isOn);
}

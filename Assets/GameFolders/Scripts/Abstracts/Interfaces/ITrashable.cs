
using System;

public interface ITrashable 
{
    public event Action OnTrashed;
    void Trashed();
}

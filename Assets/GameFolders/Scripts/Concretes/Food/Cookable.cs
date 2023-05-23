
public class Cookable : Processable, ICookable
{
   
    private bool _isPlacedOn;

    override protected void Update()
    {
        if (!_isPlacedOn)
            return;
        base.Update();

    }

    public void PlacedOn(bool isPlaced)
    {
        _isPlacedOn = isPlaced;
    }

}

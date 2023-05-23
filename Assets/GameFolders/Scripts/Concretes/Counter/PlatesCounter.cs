using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatesCounter : Counter
{

    /// <summary>
    /// This counter is only made for plates, no other thing can be placed in here
    /// This is both a counter and a spawner ?!?!?
    /// Splitting the spawner to another obj is feasable. 
    /// </summary>

    public List<Plate> platesOnTable = new List<Plate>();
    private PoolManager _poolManager;
    private GameManager _gameManager;
    [SerializeField] private int _numberOfMaxPlates = 3;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _poolManager = PoolManager.Instance;
        SpawnPlates();
        _gameManager.OnSuccessfulDelivery += SpawnNewPlate;
    }

    public override void HandleInteraction(IPlayerInteractionHandler playerInteractionHandler)
    {

        //check if the player is carrying something
        //if the player is carrying something, check if we

        if (!CheckValidInteraction(playerInteractionHandler))
            return;

        if (IsFilled)
            CollectableMovedToPlayer(playerInteractionHandler);

    }

    public override void CollectableMovedToPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {
        if (platesOnTable.Count == 0)
            return;

        Plate plateToPlayer = platesOnTable[platesOnTable.Count-1];
        playerInteractionHandler.GatherFood(plateToPlayer);
        platesOnTable.Remove(plateToPlayer);
            
    }

    public override void GatherCollectableFromPlayer(IPlayerInteractionHandler playerInteractionHandler)
    {

        //this counter cannot handle any items from outside
    }

    //will be called on order fulfillment
    public void SpawnNewPlate()
    {
        Plate instantiatedPlate = _poolManager.RequestObjFromThePool(0).GetComponent<Plate>();
        instantiatedPlate.transform.position = GatherSpawnPos();
        platesOnTable.Add(instantiatedPlate);

        if (!hasPlate)
            hasPlate = true;

        if (!IsFilled)
            IsFilled = true;

        ReorderPlates();
    }

    private void SpawnPlates()
    {
        for(int i=0; i<_numberOfMaxPlates; i++)
        {
            float yPos = 1.3f + i * .1f ;
            Vector3 spawnPos = new Vector3(transform.position.x, yPos, transform.position.z);
            
            Plate instantiatedPlate = _poolManager.RequestObjFromThePool(0).GetComponent<Plate>();
            instantiatedPlate.transform.position = spawnPos;
            platesOnTable.Add(instantiatedPlate);
        }

        hasPlate = true;
        IsFilled = true;
    }

    private Vector3 GatherSpawnPos()
    {
        float yPos = 1.5f* - platesOnTable.Count * 0.1f;
        return new Vector3(transform.position.x, yPos, transform.position.z);
    }

    private void ReorderPlates()
    {
        float yPos = 0;
        for(int i =0; i<platesOnTable.Count; i++)
        {
            yPos = i * 0.1f + 1.3f;
            platesOnTable[i].transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }

    }

}

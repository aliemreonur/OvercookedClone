using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class FoodSO : ScriptableObject
{
    public GameObject objectPrefab;
    public string foodName;
    public int id;
    public Sprite foodSprite;
    //public float processTime;
    public bool isProcessable;
    public int servableState;
}

using UnityEngine;

[CreateAssetMenu (fileName = "Enemies", menuName = "Enemies", order = 0)] 
public class Enemy : ScriptableObject //INHERITANCE
{
    [SerializeField] private new string name;
    [SerializeField] private int calories;
    [SerializeField] private int calPer100g;
    [SerializeField] private int weight;
    [SerializeField] private bool isFood;

    public string Name => name;
    public int Calories //ENCAPSULATION
    {
        get
        {
            if (calories == 0) calories = calPer100g * 100 / weight;
            return calories;
        }
    }
    public int CalPer100g => calPer100g;
    public int Weight => weight;
    public bool IsFood => isFood;
}

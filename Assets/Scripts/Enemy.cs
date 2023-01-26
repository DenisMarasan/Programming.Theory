using UnityEngine;

[CreateAssetMenu (fileName = "Enemies", menuName = "Enemies", order = 0)] 
public class Enemy : ScriptableObject //INHERITANCE
{
    [SerializeField] private new string name;
    [SerializeField] private int calories;
    [SerializeField] private bool isFood;

    public string Name => name;
    public int Calories => calories;
    public bool IsFood => isFood;
}

using UnityEngine.Events;

public class EventManager
{
    public static ClickEvent Clicked = new ClickEvent();
}

public class ClickEvent : UnityEvent <ClickEventData> { }

public class ClickEventData
{
    public bool IsClicked;
    public bool IsFood;
    public bool IsCollided;
    public int Calories;

    public ClickEventData (bool isClicked, bool isFood, bool isCollided, int calories)
    {
        this.IsClicked = isClicked;
        this.IsFood = isFood;
        this.IsCollided = isCollided;
        this.Calories = calories;
    }
}


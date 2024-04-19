using UnityEngine;

public interface IMovable
{
    void Move(Vector2 newPosition);
}

public interface IActionable
{
    void PerformAction();
}

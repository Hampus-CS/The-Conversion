using UnityEngine;

public class Actionable : MonoBehaviour, IActionable
{
    public void PerformAction()
    {
        Debug.Log(gameObject.name + " is performing an action!");
        // Add specific action logic here
    }
}

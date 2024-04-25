using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
/*    public static UnitManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public List<GameObject> selectedUnits; // List of currently selected units

    // Method to handle user input for moving units
    public void OnUserClick(Vector2 position)
    {
        foreach (GameObject unit in selectedUnits)
        {
            IMovable movable = unit.GetComponent<IMovable>();
            if (movable != null)
            {
                movable.Move(position);
            }
        }
    }

    // Method to attempt to move a unit
    public void TryMoveUnit(GameObject unit, Vector2 newPosition, Card playedCard)
    {
        IMovable movable = unit.GetComponent<IMovable>();
        if (movable != null && GameManager.Instance.CanMoveUnit(newPosition, playedCard))
        {
            movable.Move(newPosition);
            Debug.Log("Move successful!");
        }
        else
        {
            Debug.Log("Move blocked by card rules or unit is immovable!");
        }
    }

    // Method to activate a unit's action
    public void ActivateUnitAction(GameObject unit)
    {
        IActionable actionable = unit.GetComponent<IActionable>();
        if (actionable != null)
        {
            actionable.PerformAction();
        }
        else
        {
            Debug.Log("This unit cannot perform actions!");
        }
    }
*/
}

using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    [SerializeField] private Sprite cardImage;
    [SerializeField] private CardType cardType;
    [SerializeField] private int troopCount;

    public void ActivateEffect()
    {
        switch (cardType)
        {
            case CardType.MoveLeftFlank:
                MoveTroops("left", troopCount);
                break;
            case CardType.MoveCenter:
                MoveTroops("center", troopCount);
                break;
            case CardType.MoveRightFlank:
                MoveTroops("right", troopCount);
                break;
            case CardType.MoveRightLeftFlank:
                MoveTroops("right-left", troopCount);
                break;
            default:
                Debug.Log("Unknown card type");
                break;
        }
    }

    private void MoveTroops(string flank, int count)
    {
        Debug.Log($"Moving {count} troops to the {flank} flank.");
        // Add logic here for moving troops
    }
}


/*
using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{

    public Sprite cardImage;  // For displaying the card in UI
    public CardType cardType;  // This will dictate the card's effect
    public int troopCount;  // Number of troops that can be moved

    // Method to activate the card's effect
    public void ActivateEffect()
    {
        switch (cardType)
        {
            case CardType.MoveLeftFlank:
                // Logic to move troops on the left flank
                MoveTroops("left", troopCount);
                break;
            case CardType.MoveCenter:
                // Logic to move troops in the center
                MoveTroops("center", troopCount);
                break;
            case CardType.MoveRightFlank:
                // Logic to move troops on the right flank
                MoveTroops("right", troopCount);
                break;
            case CardType.MoveRightLeftFlank:
                // Logic to move troops in the right/left flank
                MoveTroops("right-left", troopCount);
                break;
            default:
                Debug.Log("Unknown card type");
                break;
        }
    }

    void MoveTroops(string flank, int count)
    {
        Debug.Log($"Moving {count} troops to the {flank} flank.");
        // Add the actual troop movement logic here
    }
}
*/
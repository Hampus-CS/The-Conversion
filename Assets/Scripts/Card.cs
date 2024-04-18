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
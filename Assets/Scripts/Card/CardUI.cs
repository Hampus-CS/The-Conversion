using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Card cardData; // Card data to be assigned in the Inspector
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null && cardData != null)
        {
            button.onClick.AddListener(() => FindObjectOfType<CardManager>().UseCard(cardData));
            // Assuming UseCard now handles logic that might have been intended for OnCardClick
        }

        UpdateCardUI();
    }

    // Changed from private to public
    public void UpdateCardUI()
    {
        Image image = GetComponent<Image>();
        if (image != null && cardData.cardImage != null)
        {
            image.sprite = cardData.cardImage;
        }

        Text text = GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = cardData.cardName;
        }
    }
}
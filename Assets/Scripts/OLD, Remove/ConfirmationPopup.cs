using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    
    [Header("Popup Panel")]
    public GameObject popupPanel;
    public Button confirmButton;
    public Button cancelButton;

    [Header("Menu Return Button")]
    public GameObject returnButton;

    private System.Action onConfirmAction;

    void Awake()
    {
        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
    }

    public void Show(System.Action confirmAction)
    {
        onConfirmAction = confirmAction;
        popupPanel.SetActive(true);
        returnButton.SetActive(false);
    }

    private void Confirm()
    {
        onConfirmAction?.Invoke();
        popupPanel.SetActive(false);
        returnButton.SetActive(true);
    }

    private void Cancel()
    {
        popupPanel.SetActive(false);
        returnButton.SetActive(true);
    }
}
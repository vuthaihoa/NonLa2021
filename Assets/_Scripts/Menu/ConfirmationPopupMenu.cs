using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationPopupMenu : Menu
{
    [Header("Components")]
    [SerializeField] private Text displayText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancleAction)
    {
        this.gameObject.SetActive(true);
        this.displayText.text = displayText;

        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            DeactiveMenu();
            confirmAction();
        });
        cancelButton.onClick.AddListener(() =>
        {
            DeactiveMenu();
            confirmAction();
        });
    }
    private void DeactiveMenu()
    {
        this.gameObject.SetActive(false);
    }
}

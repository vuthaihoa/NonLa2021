using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string proFileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject HasDataContent;
    [SerializeField] private Text percentageCompleteText;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }
    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            HasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            HasDataContent.SetActive(true);

            percentageCompleteText.text = data.GetPercentageComplete() + "% COMPLETE";
        }
    }
    public string GetProfileID()
    {
        return this.proFileId;
    }
    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}

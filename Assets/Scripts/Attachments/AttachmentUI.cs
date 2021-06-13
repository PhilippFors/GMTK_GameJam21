using Attachments;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentUI : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject magazineUI;
    [SerializeField] private GameObject muzzelUI;
    [SerializeField] private GameObject selectionUI;

    public void MoveSwitcher(int id)
    {
        switch (id)
        {
            case 0:
                selectionUI.transform.DOMove(statusUI.transform.position, 0.2f);
                break;
            case 1:
                selectionUI.transform.DOMove(magazineUI.transform.position, 0.2f);
                break;
            case 2:
                selectionUI.transform.DOMove(muzzelUI.transform.position, 0.2f);
                break;
        }
    }
    
    public void SwitchUI(int id, AttachmentBase attachmentBase)
    {
        switch (id)
        {
            case 0:
                statusUI.GetComponentInChildren<Image>().sprite = attachmentBase.UISprite;
                break;
            case 1:
                magazineUI.GetComponentInChildren<Image>().sprite = attachmentBase.UISprite;
                break;
            case 2:
                muzzelUI.GetComponentInChildren<Image>().sprite = attachmentBase.UISprite;
                break;
        }
    }
    
    
}
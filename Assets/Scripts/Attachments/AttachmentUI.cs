using System.Collections.Generic;
using Attachments;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttachmentUI : MonoBehaviour
{
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject magazineUI;
    [SerializeField] private GameObject muzzelUI;
    [SerializeField] private Image muzzelNext;
    [SerializeField] private Image magazineNext;
    [SerializeField] private Image statusNext;
    [SerializeField] private Image muzzelPrevious;
    [SerializeField] private Image magazinePrevious;
    [SerializeField] private Image statusPrevious;
    [SerializeField] private GameObject muzzleAnchorUp;
    [SerializeField] private GameObject magzineAnchorUp;
    [SerializeField] private GameObject statusAnchorUp;
    [SerializeField] private GameObject muzzleAnchorDown;
    [SerializeField] private GameObject magazinAnchorDown;
    [SerializeField] private GameObject statusAnchorDown;
    [SerializeField] private GameObject selectionUI;


    private Image currentNext;
    private Image currentPrevious;
    private GameObject currentMain;

    public void MoveSwitcher(int id)
    {
        switch (id)
        {
            case 0:
                selectionUI.transform.DOMove(statusUI.transform.position, 0.2f);
                TurnOffNextPrvious();
                TurnOnNextPrvious(statusNext, statusPrevious, statusAnchorUp, statusAnchorDown, statusUI);
                break;
            case 1:
                selectionUI.transform.DOMove(magazineUI.transform.position, 0.2f);
                TurnOffNextPrvious();
                TurnOnNextPrvious(magazineNext, magazinePrevious, magzineAnchorUp, magazinAnchorDown, magazineUI);
                break;
            case 2:
                selectionUI.transform.DOMove(muzzelUI.transform.position, 0.2f);
                TurnOffNextPrvious();
                TurnOnNextPrvious(muzzelNext, muzzelPrevious, muzzleAnchorUp, muzzleAnchorDown, muzzelUI);
                break;
        }
    }

    private void TurnOnNextPrvious(Image next, Image previous, GameObject anchorUp, GameObject anchorDown,
        GameObject main)
    {
        next.gameObject.transform.DOMove(anchorUp.transform.position, 0.3f);
        previous.gameObject.transform.DOMove(anchorDown.transform.position, 0.3f);
        currentNext = next;
        currentPrevious = previous;
        currentMain = main;
    }

    private void TurnOffNextPrvious()
    {
        if (currentNext && currentPrevious)
        {
            currentNext.gameObject.transform.DOMove(currentMain.transform.position, 0.3f);
            currentPrevious.gameObject.transform.DOMove(currentMain.transform.position, 0.3f);
        }
    }

    public void SwitchUI(int id, int current, List<AttachmentBase> attachments)
    {
        switch (id)
        {
            case 0:
                statusUI.GetComponent<Image>().sprite = attachments[current].UISprite;
                ChangePreviousAndNext(current, statusNext, statusPrevious, attachments);
                break;
            case 1:
                magazineUI.GetComponent<Image>().sprite = attachments[current].UISprite;
                ChangePreviousAndNext(current, magazineNext, magazinePrevious, attachments);
                break;
            case 2:
                muzzelUI.GetComponent<Image>().sprite = attachments[current].UISprite;
                ChangePreviousAndNext(current, muzzelNext, muzzelPrevious, attachments);
                break;
        }
    }

    private void ChangePreviousAndNext(int current, Image next, Image previous, List<AttachmentBase> attachments)
    {
        // get previous
        int c = current;
        if (c == 0)
        {
            c = attachments.Count - 1;
        }
        else
        {
            c--;
        }

        previous.GetComponent<Image>().sprite = attachments[c].UISprite;

        c = current;
        if (c == attachments.Count - 1)
        {
            c = 0;
        }
        else
        {
            c++;
        }

        next.GetComponent<Image>().sprite = attachments[c].UISprite;
    }
}
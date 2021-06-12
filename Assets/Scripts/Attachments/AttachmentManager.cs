using System;
using System.Collections;
using System.Collections.Generic;
using Attachments;
using Entities.Player.PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;
using AttachmentBase = System.Net.Mail.AttachmentBase;

namespace Attachments
{
    public class AttachmentManager : MonoBehaviour
    {
        public List<DamageAttachment> muzzle;
        public List<StatusEffectAttachment> status;
        public List<DamageAttachment> magazine;

        public List<AttachmentBase> currentAttachments = new List<AttachmentBase>();
        public Action<int, int> OnAttachmentSwitch;


        public DamageAttachment CurrentMuzzle
        {
            get => muzzle[muzzleCount];
            set => currentAttachments[0] = value;
        }

        public StatusEffectAttachment CurrentStatus
        {
            get => status[statusCount];
            set => currentAttachments[1] = value;
        }

        public DamageAttachment CurrentMagazine
        {
            get => magazine[magazineCount];
            set => currentAttachments[2] = value;
        }

        int muzzleCount = 0;
        int statusCount = 0;
        int magazineCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            currentAttachments.Add(muzzle[0]);
            currentAttachments.Add(status[0]);
            currentAttachments.Add(magazine[0]);
            PlayerInputController.Instance.Mousewheel.Performed += ChangeAttachment;
        }

        public void ChangeAttachment(InputAction.CallbackContext ctx)
        {
            float z = ctx.ReadValue<float>();
            if (PlayerInputController.Instance.Attachment1.IsPressed)
            {
                muzzleCount = checkValue(muzzleCount, z);
                ChangeAttachmentInRenderer(0, muzzleCount);
                /*if (CurrentMuzzle != muzzle[muzzleCount] && muzzle[muzzleCount] != null)
                {*/
                CurrentMuzzle = muzzle[muzzleCount];
                //}
            }
            else if (PlayerInputController.Instance.Attachment2.IsPressed)
            {
                statusCount = checkValue(statusCount, z);
                ChangeAttachmentInRenderer(1, statusCount);
                /*if (CurrentStatus is { } && CurrentStatus != status[statusCount])
                {*/
                CurrentStatus = status[statusCount];
                //}
            }
            else if (PlayerInputController.Instance.Attachment3.IsPressed)
            {
                magazineCount = checkValue(magazineCount, z);
                ChangeAttachmentInRenderer(2, magazineCount);
                CurrentMagazine = magazine[magazineCount];
            }
        }

        public int checkValue(int i, float z)
        {
            if (z > 0)
                ++i;
            else if (z < 0)
                --i;

            if (i > 2)
            {
                i = 0;
            }

            if (i < 0)
            {
                i = 2;
            }

            return i;
        }

        // Update is called once per frame
        public void ChangeAttachmentInRenderer(int typeID, int newID)
        {
            OnAttachmentSwitch.Invoke(typeID, newID);
        }
    }
}
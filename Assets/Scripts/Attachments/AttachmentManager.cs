using System;
using System.Collections;
using System.Collections.Generic;
using Attachments;
using Attachments.DamageAttachments;
using Entities.Enemy;
using Entities.Player.PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using AttachmentBase = System.Net.Mail.AttachmentBase;

namespace Attachments
{
    public class AttachmentManager : MonoBehaviour
    {
        public List<MuzzleAttachment> muzzle;
        public List<StatusEffectAttachment> status;
        public List<MagazineAttachment> magazine;

        public List<AttachmentBase> currentAttachments = new List<AttachmentBase>();
        // public Action<int, int> OnAttachmentSwitch;

        [SerializeField] private AttachmentUI attachmentUI;
        
        
        public Color startColorOfBullets;
        public Color currentMuzzleColor;
        public Color currentStatusColor;
        public Color currentMagazineColor;


        public MuzzleAttachment CurrentMuzzle
        {
            get => muzzle[muzzleCount];
            set => currentAttachments[0] = value;
        }

        public StatusEffectAttachment CurrentStatus
        {
            get => status[statusCount];
            set => currentAttachments[1] = value;
        }

        public MagazineAttachment CurrentMagazine
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

            currentMagazineColor = GetCurrentColor(CurrentMagazine);
            currentStatusColor = GetCurrentColor(CurrentStatus);
            currentMuzzleColor = GetCurrentColor(CurrentMuzzle);
            setColor();
            
            attachmentUI.SwitchUI(0, CurrentStatus);
            attachmentUI.SwitchUI(1, CurrentMagazine);
            attachmentUI.SwitchUI(2, CurrentMuzzle);
            
            attachmentUI.MoveSwitcher(0);
            
            PlayerInputController.Instance.Mousewheel.Performed += ChangeAttachment;
            PlayerInputController.Instance.ChangeSlot.Performed += ctx => ChangeSlot();
        }

        private int currentSlot = 0;
        private void ChangeSlot()
        {
            currentSlot++;

            if (currentSlot > 2)
            {
                currentSlot = 0;
            }
            
            attachmentUI.MoveSwitcher(currentSlot);
        }
        public void ChangeAttachment(InputAction.CallbackContext ctx)
        {
            float z = ctx.ReadValue<float>();
            // if (PlayerInputController.Instance.Attachment1.IsPressed)
            // {
            //     statusCount = checkValue(statusCount, z);
            //     ChangeAttachmentInRenderer(1, statusCount);
            //     /*if (CurrentStatus is { } && CurrentStatus != status[statusCount])
            //     {*/
            //     CurrentStatus = status[statusCount];
            //     attachmentUI.SwitchUI(0, CurrentStatus); //}
            //     //}
            // }
            // else if (PlayerInputController.Instance.Attachment2.IsPressed)
            // {
            //     magazineCount = checkValue(magazineCount, z);
            //     ChangeAttachmentInRenderer(2, magazineCount);
            //     CurrentMagazine = magazine[magazineCount];
            //     attachmentUI.SwitchUI(1, CurrentMagazine);
            // }
            // else if (PlayerInputController.Instance.Attachment3.IsPressed)
            // {
            //     muzzleCount = checkValue(muzzleCount, z);
            //     ChangeAttachmentInRenderer(0, muzzleCount);
            //     /*if (CurrentMuzzle != muzzle[muzzleCount] && muzzle[muzzleCount] != null)
            //     {*/
            //     CurrentMuzzle = muzzle[muzzleCount];
            //     attachmentUI.SwitchUI(2, CurrentMuzzle);
            // }   
            
            if (currentSlot == 0)
            {
                statusCount = checkValue(statusCount, z);
                ChangeAttachmentInRenderer(1, statusCount);
                /*if (CurrentStatus is { } && CurrentStatus != status[statusCount])
                {*/
                CurrentStatus = status[statusCount];
                currentStatusColor = GetCurrentColor(CurrentStatus);
                setColor();
                attachmentUI.SwitchUI(currentSlot, CurrentStatus); //}
                //}
            }
            else if (currentSlot == 1)
            {
                magazineCount = checkValue(magazineCount, z);
                ChangeAttachmentInRenderer(2, magazineCount);
                CurrentMagazine = magazine[magazineCount];
                currentMagazineColor = GetCurrentColor(CurrentMagazine);
                setColor();
                attachmentUI.SwitchUI(currentSlot, CurrentMagazine);
            }
            else if (currentSlot == 2)
            {
                muzzleCount = checkValue(muzzleCount, z);
                ChangeAttachmentInRenderer(0, muzzleCount);
                /*if (CurrentMuzzle != muzzle[muzzleCount] && muzzle[muzzleCount] != null)
                {*/
                CurrentMuzzle = muzzle[muzzleCount]; 
                currentMuzzleColor = GetCurrentColor(CurrentMuzzle);
                setColor();
                attachmentUI.SwitchUI(currentSlot, CurrentMuzzle);
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
            // OnAttachmentSwitch.Invoke(typeID, newID);
        }

        private void Update()
        {
           
        }


        private Color GetCurrentColor(AttachmentBase attachment)
        {
            Color currentColor = attachment.DamageType switch
            {
                DamageType.red => Color.red,
                DamageType.blue =>  Color.blue,
                DamageType.green =>  Color.green,
                _ => throw new ArgumentOutOfRangeException()
            };
            return currentColor;
        }

        private void setColor()
        {
            startColorOfBullets = (currentMagazineColor + currentMuzzleColor + currentStatusColor) / 3;
            startColorOfBullets.a = 1;
        }
    }
}
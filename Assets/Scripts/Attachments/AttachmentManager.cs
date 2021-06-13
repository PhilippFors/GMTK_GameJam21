using System;
using System.Collections;
using System.Collections.Generic;
using Attachments.DamageAttachments;
using Effects;
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
        public List<AttachmentBase> muzzle;
        public List<AttachmentBase> status;
        public List<AttachmentBase> magazine;

        public List<AttachmentID> muzzleVisuals;
        public List<AttachmentID> magazineVisuals;
        public List<AttachmentID> statusVisuals;

        private GameObject currentStatusVisual;
        private GameObject currentMagazineVisual;
        private GameObject currentMuzzleVisual;

        public List<AttachmentBase> currentAttachments = new List<AttachmentBase>();
        // public Action<int, int> OnAttachmentSwitch;

        [SerializeField] private AttachmentUI attachmentUI;

        public Color startColorOfBullets;
        public Color currentMuzzleColor;
        public Color currentStatusColor;
        public Color currentMagazineColor;

        [SerializeField] private SoundEffectController gunSFX;
        [SerializeField] private SoundEffectController redSFX;
        [SerializeField] private SoundEffectController blueSFX;
        [SerializeField] private SoundEffectController greenSFX;

        public MuzzleAttachment CurrentMuzzle
        {
            get => (MuzzleAttachment) muzzle[muzzleCount];
            set => currentAttachments[0] = value;
        }

        public StatusEffectAttachment CurrentStatus
        {
            get => (StatusEffectAttachment) status[statusCount];
            set => currentAttachments[1] = value;
        }

        public MagazineAttachment CurrentMagazine
        {
            get => (MagazineAttachment) magazine[magazineCount];
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

            attachmentUI.SwitchUI(0, statusCount, status);
            attachmentUI.SwitchUI(1, magazineCount, magazine);
            attachmentUI.SwitchUI(2, muzzleCount, muzzle);

            attachmentUI.MoveSwitcher(0);

            SetStatusVisual();
            SetMagazineVisual();
            SetMuzzleVisual();

            gunSFX.SetEffect(CurrentMuzzle.SFX);
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
                CurrentStatus = (StatusEffectAttachment) status[statusCount];
                attachmentUI.SwitchUI(currentSlot, statusCount, status);
                currentStatusColor = GetCurrentColor(CurrentStatus);
                setColor();
                SetStatusVisual();
                PlayChangeSoundEffect(CurrentStatus.DamageType);
            }
            else if (currentSlot == 1)
            {
                magazineCount = checkValue(magazineCount, z);
                CurrentMagazine = (MagazineAttachment) magazine[magazineCount];
                attachmentUI.SwitchUI(currentSlot, magazineCount, magazine);
                currentMagazineColor = GetCurrentColor(CurrentMagazine);
                setColor();
                SetMagazineVisual();
                PlayChangeSoundEffect(CurrentMagazine.DamageType);
            }
            else if (currentSlot == 2)
            {
                muzzleCount = checkValue(muzzleCount, z);
                CurrentMuzzle = (MuzzleAttachment) muzzle[muzzleCount];
                attachmentUI.SwitchUI(currentSlot, muzzleCount, muzzle);
                currentMuzzleColor = GetCurrentColor(CurrentMuzzle);
                setColor();
                SetMuzzleVisual();
                gunSFX.SetEffect(CurrentMuzzle.SFX);
                PlayChangeSoundEffect(CurrentMuzzle.DamageType);
            }
        }

        private void PlayChangeSoundEffect(DamageType type)
        {
            switch (type)
            {
                case DamageType.blue:
                    blueSFX.PlayEffect();
                    break;
                case DamageType.red:
                    redSFX.PlayEffect();
                    break;
                case DamageType.green:
                    greenSFX.PlayEffect();
                    break;
            }
        }

        private void SetMagazineVisual()
        {
            var att = magazineVisuals.Find(x => CurrentMagazine.AttachmentID == x.ID);
            if (currentMagazineVisual != null)
            {
                currentMagazineVisual.SetActive(false);
            }

            currentMagazineVisual = att.gameObject;
            currentMagazineVisual.gameObject.SetActive(true);
        }

        private void SetMuzzleVisual()
        {
            var att = muzzleVisuals.Find(x => CurrentMuzzle.AttachmentID == x.ID);
            if (currentMuzzleVisual != null)
            {
                currentMuzzleVisual.SetActive(false);
            }

            currentMuzzleVisual = att.gameObject;
            currentMuzzleVisual.gameObject.SetActive(true);
        }


        private void SetStatusVisual()
        {
            var att = statusVisuals.Find(x => CurrentStatus.AttachmentID == x.ID);
            if (currentStatusVisual != null)
            {
                currentStatusVisual.SetActive(false);
            }

            currentStatusVisual = att.gameObject;
            currentStatusVisual.gameObject.SetActive(true);
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
                DamageType.blue => Color.blue,
                DamageType.green => Color.green,
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
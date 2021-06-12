using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player.PlayerInput;
using UnityEngine;

namespace Attachments
{
    public class AttachmentRenderer : MonoBehaviour
    {
        public AttachmentManager manager => PlayerInputController.Instance.GetComponent<AttachmentManager>();
        public List<GameObject> muzzleObjects;
        public List<GameObject> statusObjects;
        public List<GameObject> magazineObjects;
        public List<GameObject> activeObjects;

        

        private void Start()
        {
            manager.OnAttachmentSwitch += SwitchAttachmentInRenderer;
        }

        private void SwitchAttachmentInRenderer(int typeID, int numberID)
        {
            activeObjects[typeID].GetComponent<Renderer>().enabled = false;
            switch (typeID)
            {
                case 0:
                    activeObjects[typeID] = muzzleObjects[numberID];
                    break;
                case 1:
                    activeObjects[typeID] = statusObjects[numberID];
                    break;
                case 2:
                    activeObjects[typeID] = magazineObjects[numberID];
                    break;
            };
            activeObjects[typeID].GetComponent<Renderer>().enabled = true;
        }
    }
}
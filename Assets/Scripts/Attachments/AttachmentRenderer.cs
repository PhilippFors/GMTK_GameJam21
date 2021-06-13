using System;
using System.Collections.Generic;
using Entities.Player.PlayerInput;
using JetBrains.Annotations;
using UnityEngine;

namespace Attachments
{
    public class AttachmentRenderer : MonoBehaviour
    {
        public AttachmentManager manager => PlayerInputController.Instance.GetComponent<AttachmentManager>();
        public GameObject attachmentObject;
        
        public List<GameObject> muzzleObjects;
        public List<GameObject> statusObjects;
        public List<GameObject> magazineObjects;
        
        public List<GameObject> activeObjects;
        public bool shouldRotate;

        

        private void Start()
        {
           // manager.OnAttachmentSwitch += SwitchAttachmentInRenderer;


            if (attachmentObject != null)
            {
                for (int i = 0; i < attachmentObject.transform.childCount; i++)
                {
                    for(int j = 0; j < attachmentObject.transform.GetChild(i).childCount; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                muzzleObjects.Add(attachmentObject.transform.GetChild(i).GetChild(j).gameObject); 
                                break;
                            case 1:
                                statusObjects.Add(attachmentObject.transform.GetChild(i).GetChild(j).gameObject); 
                                break;
                            case 2:
                                magazineObjects.Add(attachmentObject.transform.GetChild(i).GetChild(j).gameObject); 
                                break;
                        }
                    }
                }
                activeObjects.Add(muzzleObjects[0]);
                activeObjects.Add(statusObjects[0]);
                activeObjects.Add(magazineObjects[0]);
            }

          
               
            
            
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

        private void Update()
        {
            if(shouldRotate)
                rotateObjects();
        }

        void rotateObjects()
        {
            foreach (var attachment in activeObjects)
            {
                attachment.transform.Rotate(Vector3.up * Time.deltaTime * 50f);
            }
        }
    }
}
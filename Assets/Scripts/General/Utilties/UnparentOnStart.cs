using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Utilities
{
   public class UnparentOnStart : MonoBehaviour
   {
      private void Awake()
      {
         transform.DetachChildren();
         Destroy(this.gameObject);
      }
   }
}

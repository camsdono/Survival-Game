using System;
using UnityEngine;

public class plr_Resources : MonoBehaviour
{
      public int wood;

      public void GiveWood(int amount)
      {
            wood += amount;
      }
}

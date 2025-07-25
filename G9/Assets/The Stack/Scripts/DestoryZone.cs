using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace G9.MiniGame.TheStack
{
    public class DestoryZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Rubble"))
            {
                Destroy(collision.gameObject);
            }
        }
    }   
}
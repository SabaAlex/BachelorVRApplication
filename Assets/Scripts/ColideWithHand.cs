using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class ColideWithHand : MonoBehaviour
    { 
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "CustomHandLeft" || collision.gameObject.name == "CustomHandRight")
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZM.JM.SubSystem
{
    public class Sauce : Ingredient
    {

        public override void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Ingredient>() == null)
            {
                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                base.OnCollisionEnter(other);
            }
        }
    }
}
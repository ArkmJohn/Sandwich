using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZM.JM.SubSystem
{
    public class Plate : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Bread>() != null)
            {
                SubCreationManager.instance.AddIngredient("bread", other.gameObject);
            }
        }
    }
}
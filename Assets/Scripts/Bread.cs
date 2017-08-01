using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZM.JM.SubSystem
{
    public class Bread : Ingredient
    {
        public breadType myBreadType;

        //public override void AddToSub(Sub sub)
        //{
        //    ChangeState(IngredientState.INUSE);

        //    //if (!currentSub.isBotBread(sub))
        //    //{
        //    // 
        //    if (currentSub.ingredientTransform.Count == 0)
        //    {
        //        GetComponent<Rigidbody>().isKinematic = true;
        //        Destroy(gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>());
        //        //Destroy(gameObject.GetComponent<Collider>());
        //    }
        //    //}


        //}
    }

    public enum breadType
    {
        ITALIAN,
        WHEAT,
        PAR_OREGANO,
        HONEY_OAT,
        SESAME
    }

}
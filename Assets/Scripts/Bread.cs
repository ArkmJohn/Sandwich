using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZM.JM.SubSystem
{
    public class Bread : Ingredient
    {
        public breadType myBreadType;
    }

    public enum breadType
    {
        SESAME,
        ITALIAN,
        WHEAT,
        PAR_OREGANO,
        HONEY_OAT
        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace ZM.JM.SubSystem
{
    public abstract class Ingredient : MonoBehaviour
    {
        public IngredientState currentState = IngredientState.NOTTOUCHED;
        public IngredientType type;
        public Sub currentSub;
        [SerializeField]
        private bool shouldDeleteCollider = true;

        public IngredientState GetState()
        {
            return currentState;
        }

        public virtual void ChangeState(IngredientState newState)
        {
            this.currentState = newState;
        }

        public void HoldItem()
        {
            //if(currentState != IngredientState.INUSE)
                ChangeState(IngredientState.ISHELD);
        }

        public void DetachItem()
        { 
            //if (currentState != IngredientState.INUSE)
                ChangeState(IngredientState.HASHELD);
        }

        //public virtual void AddToSub(Sub sub)
        //{
        //    ChangeState(IngredientState.INUSE);

        //    //GetComponent<Rigidbody>().isKinematic = true;
        //    Destroy(GetComponent<Rigidbody>());
        //    Destroy(gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>());
        //    Destroy(gameObject.GetComponent<Collider>());



        //}

        public virtual void OnCollisionEnter(Collision other)
        {
            //if (other.gameObject.GetComponent<Ingredient>() != null && gameObject.GetComponent<Ingredient>().currentState == IngredientState.INUSE && currentSub != null)
            //{
            //    Ingredient ing = other.gameObject.GetComponent<Ingredient>();
            //    ing.AddToSub(other.gameObject.GetComponent<Ingredient>().currentSub);
            //    currentSub.AddIng(ing.type.ToString(), other.gameObject);
            //    ing.gameObject.transform.SetParent(currentSub.transform);
            //    //SubCreationManager.instance.AddIngredient(ing.type.ToString(), other.gameObject); // Adds the current gameObject to the sub

            //}   

            // 1 if the other is the sub
            if (other.gameObject.GetComponent<Sub>() != null)
            {
                this.AddToTheSub(other.gameObject.GetComponent<Sub>());
            }
            else
            {
                if (other.gameObject.GetComponent<Ingredient>() != null)
                {
                    Ingredient tempIng = other.gameObject.GetComponent<Ingredient>();
                    // 2 if the other is part of the sub
                    if (tempIng.currentSub != null)
                    {
                        //// Add this to sub
                        this.AddToTheSub(tempIng.currentSub);
                        //// Make the Last object Kinemtatic
                        //tempIng.kinSwitch(true);
                        //// RemoveThrowing Abilities
                        //tempIng.RemoveThrow();
                    }
                    else
                    {
                        // 3 if the other is not part of the sub
                    }
                }
            }
        }

        public virtual void OnCollisionExit(Collision other)
        {
            // 1 if the other is the sub
            if (other.gameObject.GetComponent<Sub>() != null && currentSub == other.gameObject.GetComponent<Sub>())
            {
                currentSub.ingredientTransform.Remove(this.gameObject);
                this.RemoveSub();
            }
            else
            {

                if (other.gameObject.GetComponent<Ingredient>() != null)
                {
                    Ingredient tempIng = other.gameObject.GetComponent<Ingredient>();
                    // 2 if the other is part of the sub
                    if (tempIng.currentSub == this.currentSub)
                    {
                        tempIng.MakeThrowable();
                        tempIng.kinSwitch(false);
                        this.RemoveSub();
                    }
                    else
                    {
                        // 3 if the other is not part of the sub

                    }
                }
                
            }
        }

        void AddToTheSub(Sub newSub)
        {
            this.currentSub = newSub;
            this.currentState = IngredientState.INUSE;
            this.currentSub.AddIng(this.type.ToString(), this.gameObject);
            this.transform.SetParent(currentSub.gameObject.transform);
            
        }

        void RemoveSub()
        {
            this.currentSub = null;
            this.currentState = IngredientState.HASHELD;
        }

        public void MakeThrowable()
        {
            this.gameObject.AddComponent<Throwable>();
        }

        public void RemoveThrow()
        {
            Destroy(this.gameObject.GetComponent<Throwable>());
                
        }

        public void kinSwitch(bool state)
        {
            if (this.GetComponent<Rigidbody>() != null)
            {
                this.GetComponent<Rigidbody>().isKinematic = state;
            }
        }
    }

    public enum IngredientType
    {
        BREAD,
        VEGGIE,
        SAUCE,
        MEAT,
        CHEESE
    }

    public enum IngredientState
    {
        NOTTOUCHED,
        ISHELD,
        HASHELD,
        INUSE
    }
}
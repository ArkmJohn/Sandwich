using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
 
namespace ZM.JM.SubSystem
{
    public class Sub : MonoBehaviour
    {
        public GameObject bread, topBread;
        private Bread myBread;
        private breadType myBreadType;

        public List<GameObject> ingredientTransform = new List<GameObject>();

        public List<GameObject> meats = new List<GameObject>();
        public List<GameObject> sauces = new List<GameObject>();
        public List<GameObject> veggies = new List<GameObject>();
        public List<GameObject> cheese = new List<GameObject>();

        public AudioSource aSource;
        public AudioClip makeSandwichSound;

        private void Awake()
        {
            aSource = this.gameObject.AddComponent<AudioSource>();
            aSource.spatialBlend = 1;
        }

        // For future Scalability
        #region AddIngredients
        public void AddMeat(GameObject obj)
        {
            meats.Add(obj);
            AddTransform(obj);
        }

        public void AddSauce(GameObject obj)
        {
            sauces.Add(obj);
            AddTransform(obj);

        }

        public void AddVeggie(GameObject obj)
        {
            veggies.Add(obj);
            AddTransform(obj);

        }

        public void AddCheese(GameObject obj)
        {
            cheese.Add(obj);
            AddTransform(obj);

        }
        #endregion

        // Sets the bread
        public void SetBread(GameObject newBread)
        {

             
            if (newBread.GetComponent<Bread>() == null)
            {
                //Debug.LogError("This is not bread");

                return;
            }
            else
            {
                if (bread != null)
                {
                    //Destroy(bread);
                    //bread = null;
                    //PlaceFinalBread(newBread);
                    if (topBread != null)
                    {
                        //if (isBotBread(newBread.GetComponent<Sub>()))
                        //{

                        //    //PlaceFinalBread(newBread);
                        //}
                    }
                    else
                    {
                        Debug.Log("Burger has been made");
                        //Destroy(newBread.GetComponent<Sub>());
                        topBread = newBread;
                        topBread.GetComponent<Ingredient>().kinSwitch(true);
                        MakeCollider();
                    }
                }
                else
                {
                    bread = newBread;
                    myBread = bread.GetComponent<Bread>();
                    myBreadType = myBread.myBreadType;

                    //bread.transform.position = SubCreationManager.instance.subSpawnPoint.transform.position;
                    //bread.transform.rotation = SubCreationManager.instance.subSpawnPoint.transform.rotation;
                    //myBread.AddToSub();
                }
            }

        }

        public bool isBotBread(Sub newBread)
        {
            if (newBread.ingredientTransform.Count < this.ingredientTransform.Count)
                return true;
            else
                return false;
                
        }

        public void PlaceFinalBread(GameObject newBread)
        {
            Vector3 final = bread.transform.position;
            Vector3 midPoint = FindCenterPoint(ingredientTransform.ToArray());
            newBread.transform.SetParent(this.gameObject.transform);
            float inc = midPoint.y - final.y;
            final = new Vector3(bread.transform.position.x, Mathf.Abs(midPoint.y + inc + 0.1f), bread.transform.position.z);
            Debug.Log(final);
            newBread.transform.position = final;
            MakeCollider();

        }

        public Vector3 FindCenterPoint(GameObject[] gos)
        {

            if (gos.Length == 0)
                return Vector3.zero;
            if (gos.Length == 1)
                return gos[0].transform.position;
            Bounds bounds = new Bounds(gos[0].transform.position, Vector3.zero);
            for (var i = 1; i < gos.Length; i++)
                bounds.Encapsulate(gos[i].transform.position);
            return bounds.center;
        }

        public void AddIng(string ingType, GameObject ingredient)
        {
            Debug.Log("Adding " + ingType);
            if (ingredientTransform.Contains(ingredient))
            {
                return;
            }

            switch (ingType.Trim().ToLower())
            {
                case "bread":
                    SetBread(ingredient);
                    break;

                case "veggie":
                    AddVeggie(ingredient);
                    break;

                case "sauce":
                    AddSauce(ingredient);
                    break;

                case "meat":
                    AddMeat(ingredient);
                    break;
    
                case "cheese":
                    AddCheese(ingredient);
                    break;
            }
        }

        public Bounds MakeBounds()
        {

            List<BoxCollider> colliders = new List<BoxCollider>();
            foreach (GameObject a in ingredientTransform)
            {
                colliders.Add(a.GetComponent<BoxCollider>());
            }

            Bounds bounds = new Bounds(this.transform.position, Vector3.zero);
            bounds.Encapsulate(this.GetComponent<BoxCollider>().bounds);
            foreach(BoxCollider a in colliders)
            {
                bounds.Encapsulate(a.bounds);
            }
            if (topBread != null)
            {
                bounds.Encapsulate(topBread.GetComponent<BoxCollider>().bounds);
            }
            return bounds;
        }

        public void MakeCollider()
        {
            Debug.Log("Creating Collider for Sandwich");
            aSource.PlayOneShot(makeSandwichSound);
            Destroy(this.GetComponent<BoxCollider>());
            //Destroy(gameObject.GetComponent<Rigidbody>());
            

            BoxCollider myBoxCollider = gameObject.AddComponent<BoxCollider>();

            myBoxCollider.size = MakeBounds().size;
            MakeSandwich();
        }

        public void MakeSandwich()
        {
            Debug.Log("Forming Sandwich");   
            foreach(GameObject a in ingredientTransform)
            {
                Destroy(a.GetComponent<Throwable>());
                a.GetComponent<Ingredient>().kinSwitch(true);
                Destroy(a.GetComponent<Ingredient>());
                Destroy(a.GetComponent<Rigidbody>());
                Destroy(a.GetComponent<BoxCollider>());

                 
            }
            Destroy(topBread.GetComponent<Throwable>());
            topBread.GetComponent<Ingredient>().kinSwitch(true);
            Destroy(topBread.GetComponent<Ingredient>());
            Destroy(topBread.GetComponent<Rigidbody>());
            Destroy(topBread.GetComponent<BoxCollider>());

            Rigidbody rb = gameObject.AddComponent<Rigidbody>(); 
            rb.isKinematic = true;
            gameObject.AddComponent<Throwable>();
            Destroy(this);
        }

        public void AddTransform(GameObject obj)
        {
            ingredientTransform.Add(obj);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(MakeBounds().center, MakeBounds().size);   
        }
    }


}
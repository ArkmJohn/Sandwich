using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZM.JM.SubSystem
{
    public class SubCreationManager : MonoBehaviour
    {

        public static SubCreationManager instance = null;

        public GameObject currenSubObject;
        [Tooltip("The Reference to the Sub script that will hold all the variables")]
        public Sub currentSub;
        public GameObject subSpawnPoint;

        public Transform botBreadSpawnPoint;

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {
            //if (currenSubObject == null)
            //{
            //    //GameObject sub = Instantiate(gameObject, subSpawnPoint.transform);
            //    transform.position = subSpawnPoint.transform.position;
            //    currentSub = gameObject.AddComponent<Sub>();
                
                
            //}
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Adds an ingredient to the sub
        public void AddIngredient(string ingType, GameObject ingredient)
        {
            switch (ingType.Trim().ToLower())
            {
                case "bread":
                    currentSub.SetBread(ingredient);
                    break;

                case "veggie":
                    currentSub.AddVeggie(ingredient);
                    break;

                case "sauce":
                    currentSub.AddSauce(ingredient);
                    break;

                case "meat":
                    currentSub.AddMeat(ingredient);
                    break;

                case "cheese":
                    currentSub.AddCheese(ingredient);
                    break;
            }
        }
    }
}

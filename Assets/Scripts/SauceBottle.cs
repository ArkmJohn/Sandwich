using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace ZM.JM.SubSystem
{
    public class SauceBottle : MonoBehaviour
    {
        public GameObject sauceSpillPrefab;
        public Transform spawnPoint;

        private float forceMultiplier = 100f;

        private Hand hand;
        private SteamVR_TrackedObject trackedObj;

        private SteamVR_Controller.Device Controller
        {
            get { return SteamVR_Controller.Input((int)trackedObj.index); }

        }

        private void OnAttachedToHand(Hand attachedHand)
        {
            hand = attachedHand;
        }

        void Awake()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }

        public bool isHeld = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        private void HandAttachedUpdate(Hand hand)
        {
            if (isHeld && hand.GetStandardInteractionButtonUp())
            {
                GameObject bulletClone = Instantiate(sauceSpillPrefab, spawnPoint.position, spawnPoint.rotation);
                bulletClone.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * forceMultiplier);
            }
        }



        public void Holding(bool state)
        {
            isHeld = state;
        }
    }
}
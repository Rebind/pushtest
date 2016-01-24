﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeAttachDetach : MonoBehaviour
{

    [SerializeField]
    public GameObject leg, arm, torso, twoLegs, twoArms;
    [SerializeField]
    public Sprite[] bodyStates;//{head, headTorso, headTorsoArm, headTorsoTwoArms,headTorsoTwoArmsLeg, headTorsoTwoArmsTwoLegs, headTorsoLeg, headTorsoTwoLegs, headTorsoArmLeg, headTorsoArmTwoLegs};

    List<GameObject> armsList;
    List<GameObject> torsoList;
    List<GameObject> legsList;

    string objectTag;

    /*
	 * 0 -  just the head
	 * 1 - head and torso
	 * 2 - head torso and one arm
	 * 3 - head torso and both arms
	 * 4 - head torso, both arms and one leg
	 * 5 - full body
	 * 6 - head, torso and one leg
	 * 7 - head, torso and both legs
	 * 8 - head, torso, leg and one arm
	 * 9 - head, torso, arm and two legs
	 */


    private Sprite currentBodyState; //stores the current state of the body, gets from the array
    private Animator myAnimator; //Animator for the different states
    private GameObject[] nearbyLimbsofType;
    public bool hasTorso, hasArm, hasSecondArm, hasLeg, hasSecondLeg;
    private Player player;
    private float minimumDistance = 3.5f;
    private Vector3 pos;
    bool armt;
    bool legt;
    bool torsot;
    // Use this for initialization
    void Start()
    {
        objectTag = "";
        armsList = new List<GameObject>();
        legsList = new List<GameObject>();
        torsoList = new List<GameObject>();
        myAnimator = GetComponent<Animator>();
        player = GetComponent<Player>();
        bodyStates = new Sprite[10];
        //hasTorso = hasArm = hasLeg = hasSecondLeg = true; //for testing purposes start with all body parts except second hand
        assignState();
        torso = GameObject.Find("Torso");
        arm = GameObject.Find("Arm");
        leg = GameObject.Find("leg");
    }

    void Update()
    {

        multipleLimbs();
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X Pressed");
            whichLimb();
        }
        else
        {
            detach();
        }
        //Debug.Log(objectTag);
    }

    /*
	 * 
	 * This is changing the players body states to whatever 
	 * the player attaches itself to. 
	 * 
	 * 
	 * */

    /*
* 0 -  just the head
* 1 - head and torso
* 2 - head torso and one arm
* 3 - head torso and both arms
* 4 - head torso, both arms and one leg
* 5 - full body
* 6 - head, torso and one leg
* 7 - head, torso and both legs
* 8 - head, torso, leg and one arm
* 9 - head, torso, arm and two legs
*/
    private void assignState() //assigns the state of the sprite
    {
        if (hasTorso && hasLeg && hasSecondLeg && hasArm && hasSecondArm)
        {
            myAnimator.SetInteger("state", 5);
            currentBodyState = bodyStates[5];
        }
        else
        if (hasTorso && hasLeg && !hasSecondLeg && hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 8);
            currentBodyState = bodyStates[8];
        }
        else
        if (hasTorso && hasLeg && hasSecondLeg && !hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 7);
            currentBodyState = bodyStates[7];
        }
        else
        if (hasTorso && hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 6);
            currentBodyState = bodyStates[6];
        }
        else
        if (hasTorso && hasLeg && !hasSecondLeg && hasArm && hasSecondArm)
        {
            myAnimator.SetInteger("state", 4);
            currentBodyState = bodyStates[4];
        }
        else
        if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 2);
            currentBodyState = bodyStates[2];
        }
        else
        if (hasTorso && !hasLeg && !hasSecondLeg && hasArm && hasSecondArm)
        {
            myAnimator.SetInteger("state", 3);
            currentBodyState = bodyStates[3];
        }
        else
        if (!hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 0);
            currentBodyState = bodyStates[0];
        }
        else
        if (hasTorso && !hasLeg && !hasSecondLeg && !hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 1);
            currentBodyState = bodyStates[1];
        }
        else
        if (hasTorso && hasLeg && hasSecondLeg && hasArm && !hasSecondArm)
        {
            myAnimator.SetInteger("state", 9);
            currentBodyState = bodyStates[9];
        }
        else if (!hasTorso)
            myAnimator.SetInteger("state", 0);
        currentBodyState = bodyStates[0];
    }

    /*
	 * 
	 * Which limbs the player are nearby
	 * 
	 * 
	 * */
    private void whichLimb()
    {
        if (nearbyLimbOfType("arm") && canAttach(arm))
        {
            attachLimb(armsList);
        }
        else if (nearbyLimbOfType("leg") && canAttach(leg))
        {
            attachLimb(legsList);
        }
        else if (nearbyLimbOfType("torso") && canAttach(torso))
        {
            attachLimb(torsoList);
        }

    }

    /*
	 * 
	 * This is to test if players have necessary limbs to attach itself
	 * 
	 * */
    private bool canAttach(GameObject limb)
    {
        if ((!hasSecondArm) && (hasTorso))
        {
            return true;
        }
        else if (!hasSecondLeg && hasTorso)
        {
            return true;
        }
        else if (!hasTorso)
        {
            return true;
        }
        else
            return false;
    }

    /*
	 * 
	 * This returns true if player is near a certain limb object
	 * 
	 * 
	 * */
    bool nearbyLimbOfType(string tag)
    {
        List<GameObject> whichList = new List<GameObject>();


        if (tag == "torso")
        {
            whichList = torsoList;

        }
        else if (tag == "arm")
        {

            whichList = armsList;

        }
        else if (tag == "leg")
        {

            whichList = legsList;

        }
        for (int i = 0; i < whichList.Count; ++i)
        {

            if (Vector3.Distance(transform.position, whichList[i].transform.position) <= minimumDistance)
            {
                if (whichList[i].tag == "torso" && !hasTorso)
                {
                    torso = whichList[i].gameObject;
                    objectTag = "torso";
                    return true;
                }
                else if (whichList[i].tag == "arm" && !hasArm && !hasSecondArm && hasTorso)
                {
                    arm = whichList[i].gameObject;
                    objectTag = "arm";
                    return true;
                }
                else if (whichList[i].tag == "arm" && hasArm && !hasSecondArm && hasTorso)
                {
                    twoArms = whichList[i].gameObject;
                    objectTag = "arm";
                    return true;
                }
                else if (whichList[i].tag == "leg" && !hasLeg && !hasSecondLeg && hasTorso)
                {
                    leg = whichList[i].gameObject;
                    objectTag = "leg";
                    return true;
                }
                else if (whichList[i].tag == "leg" && hasLeg && !hasSecondLeg && hasTorso)
                {
                    twoLegs = whichList[i].gameObject;
                    objectTag = "leg";
                    return true;
                }
            }
        }
        return false;
    }

    /**
	 * 
	 * Multiple Game Objects
	 * 
	 * 
	 * */

    void multipleLimbs()
    {
        Transform[] hinges = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
        //foreach(object go in allObjects)
        foreach (Transform go in hinges)
        {
            //Destroy (child.gameObject);
            if (go.tag == "arm")
            {
                armsList.Add(go.gameObject);
            }
            else if (go.tag == "torso")
            {
                torsoList.Add(go.gameObject);
            }
            else if (go.tag == "leg")
            {
                legsList.Add(go.gameObject);
            }
        }
    }

    /*
	 * 
	 * This is attaching the limb.
	 * It will disable the object in the game if player decides to attach.
	 * Then, it will change the player's state.
	 * 
	 * */
    public void attachLimb(List<GameObject> limb)
    {

        if (!hasTorso && objectTag == "torso")
        {
            hasTorso = true;
            assignState();
            Debug.Log("testing in here");
            //torso.transform.position = arm.transform.position;
            torso.SetActive(false);

        }
        else if (hasTorso && objectTag == "arm")
        {

            if (!hasArm)
            {
                hasArm = true;
                arm.SetActive(false);
            }
            else if (hasArm && !hasSecondArm)
            {
                hasSecondArm = true;
                twoArms.SetActive(false);
            }

            assignState();
        }
        else if (objectTag == "leg" && hasTorso)
        {
            if (!hasLeg)
            {
                hasLeg = true;
                leg.SetActive(false);
            }
            else if (hasLeg && !hasSecondLeg)
            {
                hasSecondLeg = true;
                twoLegs.SetActive(false);
            }

            assignState();


        }
    }

    /*
	 * 
	 * This is the detach part.
	 * Checks if player has the necessary parts to detach
	 * Then, the limb will respawn where players are at. 
	 * 
	 * 
	 * */
    public void detach()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasTorso)
        {
            torso.SetActive(true);
            checkForDifferentLimbs();
            instantiateBodyParts(torso);
            hasTorso = false;
            assignState();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasArm && !hasSecondArm)
        {
            arm.SetActive(true);
            instantiateBodyParts(arm);
            hasArm = false;
            assignState();
        }
        else if (Input.GetKey(KeyCode.Alpha2) && hasArm && hasSecondArm)
        {
            twoArms.SetActive(true);
            instantiateBodyParts(twoArms);
            hasSecondArm = false;
            assignState();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasLeg && !hasSecondLeg)
        {
            leg.SetActive(true);
            hasLeg = false;
            instantiateBodyParts(leg);
            assignState();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasSecondLeg)
        {
            twoLegs.SetActive(true);
            hasSecondLeg = false;
            instantiateBodyParts(leg);
            instantiateBodyParts(twoLegs);
            assignState();
        }

    }

    /*
	 * 
	 * This is checking if players have a torso, arms, and legs, and
	 * they want to detach the torso. If players detach the torso, 
	 * everything will fall apart. 
	 * 
	 * 
	 * */
    public void checkForDifferentLimbs()
    {

        if (hasArm && !hasSecondArm)
        {
            arm.SetActive(true);
            instantiateBodyParts(arm);
        }
        else if (hasArm && hasSecondArm)
        {
            twoArms.SetActive(true);
            arm.SetActive(true);
            instantiateBodyParts(twoArms);
            instantiateBodyParts(arm);
        }
        if (hasLeg && !hasSecondLeg)
        {
            leg.SetActive(true);
            instantiateBodyParts(leg);
        }
        else if (hasLeg && hasSecondLeg)
        {
            twoLegs.SetActive(true);
            leg.SetActive(true);
            instantiateBodyParts(twoLegs);
            instantiateBodyParts(leg);
        }
        hasTorso = false;
        hasArm = false;
        hasLeg = false;
        hasSecondArm = false;
        hasSecondLeg = false;
    }
    public void instantiateBodyParts(GameObject limbs)
    {
        pos = player.transform.position;
        limbs.transform.position = pos;
    }

}
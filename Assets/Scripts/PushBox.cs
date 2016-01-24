using UnityEngine;
using System.Collections;

public class PushBox : MonoBehaviour {
	//GameObject myGameObject = new GameObject("box"); // Make a new GO.
	// Use this for initialization
	void Start () {
	//myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    

	}	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H)){
        	 Debug.Log("H Pressed");
        	 //Destroy(GetComponent<Rigidbody2D>());
        	 gameObject.layer = 11;
        	 //gameObject.GetComponent<Rigidbody2D>().disabled = true;
        	 //GetComponent<Rigidbody2D>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.H)){
        	 Debug.Log("H up");
        	 gameObject.layer = 8;
        	 //Rigidbody gameObjectsRigidBody = gameObject.AddComponent<Rigidbody>();
        }
        
	}
}

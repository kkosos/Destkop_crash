using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMove : MonoBehaviour {
	public float coef=1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw ("stop") != 0){
			//rb.velocity=Vector3.zero;
		}else{
			//Debug.Log(Input.GetAxisRaw("Horizontal")+" "+Input.GetAxisRaw("Vertical")+" "+Input.GetAxisRaw("Jump")+" "+Input.GetAxisRaw("rotH"));
			transform.Translate((transform.parent.right*Input.GetAxisRaw("Horizontal")*coef
				+transform.parent.forward*Input.GetAxisRaw("Vertical")
				+transform.parent.up*Input.GetAxisRaw("Jump")
			)*coef);
			transform.Rotate(transform.parent.up*Input.GetAxisRaw("rotH")*coef);
			//rb.AddForce(transform.parent.right*Input.GetAxisRaw("Horizontal")*coef,ForceMode.Impulse);
			//rb.AddForce(transform.parent.forward*Input.GetAxisRaw("Vertical")*coef,ForceMode.Impulse);
			//rb.AddForce(transform.parent.up*Input.GetAxisRaw("Jump")*coef,ForceMode.Impulse);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour {
	private bool hitting=false;
	private Rigidbody targetRb;
	private Collider targetCollider;
	private Collider myCollider;
	private Vector3 curr;
    private float forceMax =1024;
    private float forceCoef =2048;
    private int CDFrame =15;
	private int CDFCnt=0;
    private int historyRefCnt =3;
	private int hrc=1;
	public string targetName="";
	private Vector3[] history; // queue, total space will be hrc=historyRefCnt+1, 1 space is not used
	private int historyStart=0; // not included(=curr), ++er, older
	private GameObject target;
	void setHistory(Vector3 rhs){
		for(int x=0;x<hrc;x++)history[x]=rhs;
	}
	void updateHistory(Vector3 rhs){
		history[historyStart]=rhs;
		historyStart=(historyStart+hrc-1)%hrc;
	}
	Vector3 getHistory(int n){ // history: 1 <= n < hrc, current: n=0
		return history[(historyStart+n+hrc)%hrc];
	}
	// Use this for initialization
	void Start () {
		myCollider=gameObject.GetComponent<Collider>();
		hrc=historyRefCnt+1;
		curr=transform.position;
		history=new Vector3[hrc];
		setHistory(curr);
		CDFCnt=CDFrame;
		target=GameObject.Find(targetName);
		if(target==null) return;
		targetRb=target.GetComponent<Rigidbody>();
		targetCollider=target.transform.GetChild(0).GetComponent<Collider>();
	}
	// Update is called once per frame
	void Update () {
		history[historyStart]=curr=transform.position;
		Debug.Log(CDFCnt+" "+hitting);
		if(hitting){ 
			if(CDFCnt>0) CDFCnt--; else{
				CDFCnt=CDFrame;
				hitting=false;
			}
		}else{
			Physics.IgnoreCollision(myCollider,targetCollider,false);
			hitWithoutTouch();
		}
        Debug.Log("aaa "+(getHistory(0) - getHistory(1)).magnitude*1024);

        updateHistory(curr);
	}
	bool projectedOnTable(Vector3 rhs){
		Vector3 tmp=targetCollider.transform.InverseTransformPoint(rhs);
		return -0.5<tmp.x && tmp.x<0.5 && -0.5<tmp.z && tmp.z<0.5;
    }
    void crossAct(){
		if(hitting) return;
		Physics.IgnoreCollision(myCollider,targetCollider,true);
		hitting=true;
		Debug.Log("cross "+Time.frameCount+" ? "+transform.position+""+target.transform.position);
		Vector3 dh=(getHistory(0)-getHistory(1));
		targetRb.AddForceAtPosition(dh.normalized*(dh.magnitude * dh.magnitude * 8192+512), curr);
		setHistory(curr);
		Debug.Log("crossAct");
	}
	void hitWithoutTouch(){
		if(target==null) return;
		Vector3 tpos=target.transform.position;
		Vector3 tup=target.transform.up;
		Debug.Log(tup+" "+tpos);
		bool flag=false;
		Vector3 preA=getHistory(0);
		Vector3 preV=preA-tpos;
		Vector3 preP=Vector3.Project(preV,tup);
		float prePL=preP.magnitude;
		for(int x=1;x<hrc;x++){
			Vector3 nA=getHistory(x);
			Vector3 nV=nA-tpos;
			Vector3 nP=Vector3.Project(nV,tup);
			float nPL=nP.magnitude;

			Vector3 da=preA-nA;
			if(Vector3.Dot(preP,nP)<=0 && ( prePL+nPL<=0 || projectedOnTable(da*nPL/(prePL+nPL)+nA) ) ){
				flag=true;
			}

			preA=nA;
			preV=nV;
			preP=nP;
		}
		if(flag) crossAct();
	}
}

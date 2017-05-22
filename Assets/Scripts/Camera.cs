using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject toFollow;

	private Vector3 offset;

	void Start () {
		offset = transform.position - toFollow.transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = offset + toFollow.transform.position;
	}
}

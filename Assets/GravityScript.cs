using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {

	public float acceleration = 9.81f;
	private Vector3 distanceToNearestPlanet;
	private GameObject[] planets;


	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		distanceToNearestPlanet = (NearestPlanet().transform.position - this.transform.position);

		this.gameObject.GetComponent<Rigidbody>().AddForce(distanceToNearestPlanet.normalized * acceleration);

		//TODO: läuft leider nicht
		this.transform.rotation = Quaternion.FromToRotation(NearestPlanet().transform.position, this.transform.position);

		/*
		for (int i = 0; i < planets.Length; i++){
			this.gameObject.GetComponent<Rigidbody>().AddForce((planets[i].transform.position - this.transform.position).normalized * acceleration * 100 / Vector3.Distance(planets[i].transform.position, this.transform.position));
		}
		*/
	
	}

	private GameObject NearestPlanet(){
		float minDistance = 1000000f;
		float currentDistance = 0;
		GameObject nearestPlanet = null;

		for (int i = 0; i < planets.Length; i++){
			currentDistance = Vector3.Distance(planets[i].transform.position, this.transform.position);

			if (currentDistance < minDistance){
				nearestPlanet = planets[i];
				minDistance = currentDistance;
			}
		}

		return nearestPlanet;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawRay(this.transform.position, distanceToNearestPlanet);
	}
	
}

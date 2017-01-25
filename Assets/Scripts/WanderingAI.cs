using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	public float pVisionRange = 7.0f;
	private bool _alive;

	[SerializeField] private float askewAngle = 15.0f;
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	void Start () {
		_alive = true;
	}

	void Update () {
		if (_alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);

			lookAhead ();
			lookAskew (askewAngle);
			lookAskew (-askewAngle);
		}
	}

	private void lookAhead() {
		//IMP: Ray from start point to direction
		//trasnsform.forward only gives the direction in the global scale, not an enpoint
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		// draw the ray for debugging
		//IMP: DebugLine from start point to end point
		//trasnsform.forward only gives the direction in the global scale, not an enpoint
		Debug.DrawLine(transform.position, transform.position + (transform.forward * obstacleRange), Color.yellow);
		Debug.DrawLine(transform.position + (transform.forward * obstacleRange), transform.position + (transform.forward * obstacleRange) + (0.4f * transform.forward), Color.yellow);

		if (Physics.SphereCast (ray, 0.75f, out hit)) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.GetComponent<PlayerCharacter> ()) {
				if (_fireball == null) {
					_fireball = Instantiate (fireballPrefab) as GameObject;
					_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
					_fireball.transform.rotation = transform.rotation;
				}
			}

			if (hit.distance < obstacleRange) {
				if (hit.transform.gameObject.tag != "blueTag") {
					// sees something that's not a blue post so runaway and change to red
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
					gameObject.GetComponent<Renderer> ().material.color = Color.red;
				} else {
					// sees a blue tag post
					Debug.Log ("Looks like a blue tag is in front me");
					gameObject.GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.startEating);
//					GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Eating);
					gameObject.GetComponent<Renderer> ().material.color = Color.cyan;

					//Collided with a blue destoryable object
					if (hit.distance < 2.0f) {
						hit.transform.gameObject.GetComponent<DestroyablePillar> ().ReactToHit ();
						gameObject.GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.stopEating);
					}
				}
			} else {
				//doesn't see anything nearby so make it yellow
				gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
			}
		}
	}

	private void lookAskew(float angle) {
		Vector3 rayAngle = Quaternion.Euler (0, angle, 0) * transform.forward;

		Ray ray = new Ray (transform.position, rayAngle);
		RaycastHit hit;

		Debug.DrawLine (transform.position, transform.position + (rayAngle * pVisionRange), Color.green);

		if (Physics.SphereCast (ray, 0.1f, out hit)) {
			if ((hit.transform.gameObject.tag == "blueTag") && (hit.distance < pVisionRange)) {
				//look at pole in peripheral vision
				Vector3 tmpTargetPosition;
				tmpTargetPosition = hit.transform.position;
				tmpTargetPosition.y = transform.position.y;

				transform.LookAt (tmpTargetPosition);
//				transform.Rotate (0, angle, 0);
			}
		}
	}

	public void SetAlive(bool alive) {
		_alive = alive;
	}
}

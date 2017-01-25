using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit() {
		WanderingAI behavior = GetComponent<WanderingAI> ();
		if (behavior != null) {
			behavior.SetAlive (false);
		}
		StartCoroutine (Die ());
	}

	private IEnumerator Die() {

		GetComponent<AnimationState> ().UpdateAnimation (AnimationState.CurrentAnimation.Dying);
//		this.transform.Rotate (-75, 0, 0);

		yield return new WaitForSeconds (1.0f);

		Destroy (this.gameObject);
	}
}

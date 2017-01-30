using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePillar : MonoBehaviour {
	private WanderingAI reference;

	public void ReactToHit(WanderingAI reff) {
		reference = reff;
		StartCoroutine (Die ());
	}

	private IEnumerator Die() {
		this.transform.Translate (0, -8, 0);

		yield return new WaitForSeconds (1.5f);
		reference.bluePolesLeft--;
		Destroy (this.gameObject);

	}
}

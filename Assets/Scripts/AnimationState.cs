using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : MonoBehaviour {

	public enum CurrentAnimation {Walking, Eating, Dying, startEating, stopEating};
	public CurrentAnimation curAnim;

	void Start () {
		curAnim = CurrentAnimation.Walking;
	}

	public void UpdateAnimation(CurrentAnimation _curAnim) {
		curAnim = _curAnim;

		if (curAnim == CurrentAnimation.Walking) {
			GetComponent<Animator> ().SetTrigger ("walking");
		}

//		if (curAnim == CurrentAnimation.Eating) {
//			GetComponent<Animator> ().SetTrigger ("eatingTrigger");
//		}

		if (curAnim == CurrentAnimation.Dying) {
			GetComponent<Animator> ().SetTrigger ("dyingTrigger");
		}

		if (curAnim == CurrentAnimation.startEating) {
			GetComponent<Animator> ().SetBool ("isEating", true);
		}

		if (curAnim == CurrentAnimation.stopEating) {
			GetComponent<Animator> ().SetBool ("isEating", false);
		}
	}
}

using System;
using TMPro;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class PreviewExplorer : MonoBehaviour
{
	[SerializeField] public TMP_Text characterSpeech;
	[HideInInspector] public Animator explorerAnimations;

	public Action PreviewExplored;
	private void Start()
	{
		explorerAnimations = GetComponent<Animator>();
	}

	public void StartExplore()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += Commander;
		characterSpeech.text = "WELCOME TO A SPACE ADVENTURE CALLED [GAME NAME]";
	}

	private void Commander(Finger finger)
	{
		Touch.onFingerDown -= Commander;
		Touch.onFingerDown += Drones;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "WILL YOU TEST YOURSELF AS A SPACE COMMANDER? THEN I WILL TELL YOU HOW!";
	}

	public void Drones(Finger finger)
	{
		Touch.onFingerDown -= Drones;
		Touch.onFingerDown += LeftDrone;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "YOU HAVE TWO SPACE DRONES AT YOUR DISPOSAL, WHICH SHOULD BE CONTROLLED BY PRESSING ON THE SCREEN OF YOUR DEVICE";

	}

	public void LeftDrone(Finger finger)
	{
		Touch.onFingerDown -= LeftDrone;
		Touch.onFingerDown += RightDrone;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "HOLD THE LEFT OF THE SCREEN SO THAT THE LEFT DRONE STARTS CHANGING ITS SIZE, THEREFORE CHANGING ITS SPEED!";

	}

	public void RightDrone(Finger finger)
	{
		Touch.onFingerDown -= RightDrone;
		Touch.onFingerDown += WhiteSpheres;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "DO THE SAME FOR THE RIGHT DRONE BY HOLDING ON THE RIGHT SIDE OF THE SCREEN!";

	}

	public void WhiteSpheres(Finger finger)
	{
		Touch.onFingerDown -= WhiteSpheres;
		Touch.onFingerDown += Match;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "THESE ACTIONS ARE NECESSARY TO SELECT THE SIZES OF YOUR DRONES SO THAT THEY MATCH THE WHITE SPHERES APPEARING ABOVE!";

	}

	public void Match(Finger finger)
	{
		Touch.onFingerDown -= Match;
		Touch.onFingerDown += OutOfRange;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "IF THE SIZE OF THE DRONE DOES NOT MATCH THE SIZE OF THE SPHERE, YOU WILL LOSE. MATCH THE REQUIRED NUMBER OF SPHERES TO PASS THE LEVEL!";
	}

	public void OutOfRange(Finger finger)
	{
		Touch.onFingerDown -= OutOfRange;
		Touch.onFingerDown += PreviewExploredAction;
		explorerAnimations.SetTrigger("PreviewTrigger");
		characterSpeech.text = "BE CAREFUL! IF ONE OF THE DRONES FLY OFF THE SCREEN, YOU WILL LOSE! GOOD LUCK!";
	}

	public void PreviewExploredAction(Finger finger)
	{
		PreviewExplored();
		gameObject.SetActive(false);

		Touch.onFingerDown -= Commander;
		Touch.onFingerDown -= Drones;
		Touch.onFingerDown -= LeftDrone;
		Touch.onFingerDown -= RightDrone;
		Touch.onFingerDown -= Match;
		Touch.onFingerDown -= PreviewExploredAction;
		Touch.onFingerDown -= OutOfRange;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= Commander;
		Touch.onFingerDown -= Drones;
		Touch.onFingerDown -= LeftDrone;
		Touch.onFingerDown -= RightDrone;
		Touch.onFingerDown -= Match;
		Touch.onFingerDown -= PreviewExploredAction;
		Touch.onFingerDown -= OutOfRange;
	}
}

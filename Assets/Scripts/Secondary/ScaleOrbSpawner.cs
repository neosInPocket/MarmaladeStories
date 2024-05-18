using UnityEngine;

public class ScaleOrbSpawner : MonoBehaviour
{
	[SerializeField] private ScaleOrb scaleOrbPref;
	[SerializeField] private FlameController flamePref;
	[SerializeField] private Vector2 appearDistances;
	[SerializeField] private CameraTargetChoose cam;
	[SerializeField] private Transform leftTarget;
	[SerializeField] private Transform rightTarget;
	[SerializeField] private ScaleOrb firstLeft;
	[SerializeField] private ScaleOrb firstRight;
	[SerializeField] private FlameController firstFlameLeft;
	[SerializeField] private FlameController firstFlameRight;
	private ScaleOrb currentLeft;
	private ScaleOrb currentRight;
	private FlameController currentLeftFlame;
	private FlameController currentRightFlame;

	public void Initialize()
	{
		currentLeft = firstLeft;
		currentRight = firstRight;
		currentLeftFlame = firstFlameLeft;
		currentRightFlame = firstFlameRight;

		currentLeft.InitializeScaleOrb(-cam.cameraSize.x / 2);
		currentRight.InitializeScaleOrb(cam.cameraSize.x / 2);
		firstFlameLeft.transform.position = new Vector2(-cam.cameraSize.x, firstFlameLeft.transform.position.y);
		firstFlameRight.transform.position = new Vector2(cam.cameraSize.x, firstFlameRight.transform.position.y);
		firstFlameRight.transform.eulerAngles = new Vector3(0, 180, 0);
	}

	private void Update()
	{
		CheckLeft();
		CheckRight();
		CheckLeftFlame();
		CheckRightFlame();
	}

	public void CheckLeftFlame()
	{
		if (leftTarget.transform.position.y + 2 * cam.cameraSize.y > currentLeftFlame.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentLeftFlame.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentLeftFlame.transform.position.y + difference;
			var left = Instantiate(flamePref, orbPosition, Quaternion.identity, transform);

			currentLeftFlame = left;
		}
	}

	public void CheckRightFlame()
	{
		if (rightTarget.transform.position.y + 2 * cam.cameraSize.y > currentRightFlame.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentRightFlame.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentRightFlame.transform.position.y + difference;
			var right = Instantiate(flamePref, orbPosition, Quaternion.identity, transform);
			right.transform.eulerAngles = new Vector3(0, 180, 0);

			currentRightFlame = right;
		}
	}

	public void CheckRight()
	{
		if (rightTarget.transform.position.y + 2 * cam.cameraSize.y > currentRight.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentRight.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentRight.transform.position.y + difference;
			var right = Instantiate(scaleOrbPref, orbPosition, Quaternion.identity, transform);

			right.InitializeScaleOrb(orbPosition.x);
			currentRight = right;
		}
	}

	public void CheckLeft()
	{
		if (leftTarget.transform.position.y + 2 * cam.cameraSize.y > currentLeft.transform.position.y)
		{
			Vector2 orbPosition;
			orbPosition.x = currentLeft.transform.position.x;
			var difference = Random.Range(appearDistances.x, appearDistances.y);
			orbPosition.y = currentLeft.transform.position.y + difference;
			var left = Instantiate(scaleOrbPref, orbPosition, Quaternion.identity, transform);

			left.InitializeScaleOrb(orbPosition.x);
			currentLeft = left;
		}
	}
}

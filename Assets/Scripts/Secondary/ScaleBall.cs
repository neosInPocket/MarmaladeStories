using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchInput = UnityEngine.InputSystem.EnhancedTouch.Touch;
using FingerInput = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class ScaleBall : MonoBehaviour
{
	[SerializeField] public SpriteRenderer scaleRenderer;
	[SerializeField] public Rigidbody2D scaleBallRigid;
	[SerializeField] public Vector2 minMaxScale;
	[SerializeField] public Vector2 minMaxSpeed;
	[SerializeField] public DefaultUpgradeValues defaultUpgradeValues;
	[SerializeField] public bool isRightBall;
	[SerializeField] public CameraTargetChoose cameraTargetChoose;
	[SerializeField] private Vector2 rotateAmplitues;
	private float rotateAmplitude;
	private int rotateDir;
	private Vector3 rotValue;

	public float Scale
	{
		get => transform.localScale.x;
		set
		{
			var currentScale = transform.localScale;
			currentScale.x = value;
			currentScale.y = value;
			currentScale.z = value;
			transform.localScale = currentScale;
		}
	}

	public float Speed
	{
		get => scaleBallRigid.velocity.y;
		set
		{
			var currentSpeed = scaleBallRigid.velocity;
			currentSpeed.y = value;
			scaleBallRigid.velocity = currentSpeed;
		}
	}

	private float scaleSpeed;
	private bool isScaleChangeActive;
	private int currentScaleDirection;
	private FingerInput currentFinger;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		Scale = (minMaxScale.x + minMaxScale.y) / 2;
		Speed = (minMaxSpeed.x + minMaxSpeed.y) / 2;
		currentScaleDirection = 1;
		scaleSpeed = defaultUpgradeValues.ScaleSpeedUpgrade;
		rotateDir = Random.Range(0, 2) == 0 ? -1 : 1;
		rotateAmplitude = Random.Range(rotateAmplitues.x, rotateAmplitues.y);

		if (isRightBall)
		{
			transform.position = new Vector2(cameraTargetChoose.cameraSize.x / 2, 0);
		}
		else
		{
			transform.position = new Vector2(-cameraTargetChoose.cameraSize.x / 2, 0);
		}
	}

	private void Update()
	{
		if (!isScaleChangeActive) return;

		Scale += currentScaleDirection * scaleSpeed * Time.deltaTime;
		if (Scale < minMaxScale.x)
		{
			Scale = minMaxScale.x;
			currentScaleDirection = 1;
		}

		if (Scale > minMaxScale.y)
		{
			Scale = minMaxScale.y;
			currentScaleDirection = -1;
		}

		MapSpeedValue();
		rotValue.z += rotateDir * Time.deltaTime * rotateAmplitude;
		transform.eulerAngles = rotValue;
	}

	public void ActivateBall()
	{
		TouchInput.onFingerDown += OnScaleChangeStart;
		TouchInput.onFingerUp += OnScaleChangeEnd;
	}

	public void DeactivateBall()
	{
		TouchInput.onFingerDown -= OnScaleChangeStart;
		TouchInput.onFingerUp -= OnScaleChangeEnd;
	}

	public void BlowScaleBall()
	{
		scaleBallRigid.velocity = Vector3.zero;
	}

	private void MapSpeedValue()
	{
		Speed = (Scale - minMaxScale.x) / (minMaxScale.y - minMaxScale.x) * (minMaxSpeed.y - minMaxSpeed.x) + minMaxSpeed.x;
	}

	private void OnScaleChangeStart(FingerInput input)
	{
		bool isRightSided = IsOnRightSide(input);

		if ((isRightBall && isRightSided) || (!isRightBall && !isRightSided))
		{
			isScaleChangeActive = true;
			currentFinger = input;
		}
	}

	public void OnScaleChangeEnd(FingerInput input)
	{
		if (input == currentFinger)
		{
			isScaleChangeActive = false;
		}
	}

	public bool IsOnRightSide(FingerInput fingerInput)
	{
		if (fingerInput.screenPosition.x > Screen.width / 2)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void OnDestroy()
	{
		TouchInput.onFingerDown -= OnScaleChangeStart;
		TouchInput.onFingerUp -= OnScaleChangeEnd;
	}
}

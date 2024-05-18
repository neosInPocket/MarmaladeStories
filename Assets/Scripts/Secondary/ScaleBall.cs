using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchInput = UnityEngine.InputSystem.EnhancedTouch.Touch;
using FingerInput = UnityEngine.InputSystem.EnhancedTouch.Finger;
using Action = System.Action;

public class ScaleBall : MonoBehaviour
{
	[SerializeField] public SpriteRenderer scaleRenderer;
	[SerializeField] public Rigidbody2D scaleBallRigid;
	[SerializeField] public Vector2 minMaxScale;
	[SerializeField] public DefaultUpgradeValues defaultUpgradeValues;
	[SerializeField] public bool isRightBall;
	[SerializeField] public CameraTargetChoose cameraTargetChoose;
	[SerializeField] private Vector2 rotateAmplitues;
	[SerializeField] private GameObject scaleBallBlow;
	private Vector2 minMaxSpeed;
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
	public Action BallFitAction { get; set; }
	public Action UnFitAction { get; set; }

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}
	public void Initialize()
	{
		minMaxSpeed = defaultUpgradeValues.VerticalSpeedUpgrade;

		Scale = (minMaxScale.x + minMaxScale.y) / 2;

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
		Speed = (minMaxSpeed.x + minMaxSpeed.y) / 2;
	}

	public void DeactivateBall()
	{
		TouchInput.onFingerDown -= OnScaleChangeStart;
		TouchInput.onFingerUp -= OnScaleChangeEnd;
		scaleBallRigid.velocity = Vector3.zero;
		scaleBallRigid.constraints = RigidbodyConstraints2D.FreezeAll;
		isScaleChangeActive = false;
	}

	public void BlowScaleBall()
	{
		scaleBallBlow.SetActive(true);
		DeactivateBall();
		isScaleChangeActive = false;
		scaleRenderer.enabled = false;
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
			currentScaleDirection *= -1;
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

	private bool triggerEnter;

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.TryGetComponent<FlameController>(out FlameController component))
		{
			UnFitAction?.Invoke();
		}

		if (transform.position.y < collider.transform.position.y || triggerEnter) return;

		if (collider.TryGetComponent<ScaleOrb>(out ScaleOrb orb))
		{
			triggerEnter = true;
			if (orb.CheckScaleBallFit(transform))
			{
				BallFitAction?.Invoke();
			}
			else
			{
				UnFitAction?.Invoke();
			}

			return;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		triggerEnter = false;
	}

	private void OnDestroy()
	{
		TouchInput.onFingerDown -= OnScaleChangeStart;
		TouchInput.onFingerUp -= OnScaleChangeEnd;
	}
}

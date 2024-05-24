using System.Collections;
using UnityEngine;

public class FlameController : MonoBehaviour
{
	[SerializeField] public ParticleSystem mainParticle;
	[SerializeField] public BoxCollider2D colliderBox;
	[SerializeField] public float stopEmitSpeed;
	[SerializeField] public float startEmitSpeed;
	[SerializeField] public float pulse;
	[SerializeField] public float inactivePulseTime;

	private void Start()
	{
		StartCoroutine(PulseDelay());
	}

	public void DisableFlame()
	{
		colliderBox.enabled = false;
		var main = mainParticle.main;
		main.startSpeed = stopEmitSpeed;
	}

	public void EnableFlame()
	{
		colliderBox.enabled = true;
		var main = mainParticle.main;
		main.startSpeed = startEmitSpeed;
	}

	public IEnumerator PulseDelay()
	{
		EnableFlame();
		yield return new WaitForSeconds(pulse);

		DisableFlame();
		yield return new WaitForSeconds(inactivePulseTime);

		StartCoroutine(PulseDelay());
	}
}

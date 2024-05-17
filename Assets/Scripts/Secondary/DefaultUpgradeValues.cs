using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Values")]
public class DefaultUpgradeValues : ScriptableObject
{
	[SerializeField] private float[] _upgradesForSpeed;
	[SerializeField] private Vector2[] _upgradesForVerticalSpeed;
	public float ScaleSpeedUpgrade => _upgradesForSpeed[SafeKeeper.Entry.secondary];
	public Vector2 VerticalSpeedUpgrade => _upgradesForVerticalSpeed[SafeKeeper.Entry.initial];
}

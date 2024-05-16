using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Values")]
public class DefaultUpgradeValues : ScriptableObject
{
	[SerializeField] private float[] _upgradesForSpeed;
	public float ScaleSpeedUpgrade => _upgradesForSpeed[SafeKeeper.Entry.initial];
}

using UnityEngine;

[CreateAssetMenu(menuName = "Defaults")]
public class DefaultKeeper : ScriptableObject
{
	[SerializeField] private bool deleteKeeper;
	[SerializeField] private int layer;
	[SerializeField] private int currency;
	[SerializeField] private int initial;
	[SerializeField] private int secondary;
	[SerializeField] private bool musicInit;
	[SerializeField] private bool soundsInit;
	[SerializeField] private bool schooling;

	public bool DeleteKeeper => deleteKeeper;
	public int Layer => layer;
	public int Currency => currency;
	public int Initial => initial;
	public int Secondary => secondary;
	public int MusicInit => musicInit ? 1 : 0;
	public int SoundsInit => soundsInit ? 1 : 0;
	public int Schooling => schooling ? 1 : 0;
}

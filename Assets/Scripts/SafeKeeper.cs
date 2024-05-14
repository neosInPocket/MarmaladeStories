using UnityEngine;

public class SafeKeeper : MonoBehaviour
{
	[SerializeField] private DefaultKeeper defaultKeeper;
	public static SafeKeeper Entry { get; private set; }

	[HideInInspector] public int layer;
	[HideInInspector] public int currency;
	[HideInInspector] public int initial;
	[HideInInspector] public int secondary;
	[HideInInspector] public int musicInit;
	[HideInInspector] public int soundsInit;
	[HideInInspector] public int schooling;

	private void Awake()
	{
		if (Entry != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Entry = this;
			DontDestroyOnLoad(gameObject);
		}

		if (!defaultKeeper.DeleteKeeper)
		{
			AwakeKeeperValues();
		}
		else
		{
			DefaultKeeper();
			Safer();
		}
	}

	public void DefaultKeeper()
	{
		layer = defaultKeeper.Layer;
		currency = defaultKeeper.Currency;
		initial = defaultKeeper.Initial;
		secondary = defaultKeeper.Secondary;
		musicInit = defaultKeeper.MusicInit;
		soundsInit = defaultKeeper.SoundsInit;
		schooling = defaultKeeper.Schooling;
	}

	public void Safer()
	{
		PlayerPrefs.SetInt("layer", layer);
		PlayerPrefs.SetInt("currency", currency);
		PlayerPrefs.SetInt("initial", initial);
		PlayerPrefs.SetInt("secondary", secondary);
		PlayerPrefs.SetInt("musicInit", musicInit);
		PlayerPrefs.SetInt("soundsInit", soundsInit);
		PlayerPrefs.SetInt("schooling", schooling);
		PlayerPrefs.Save();
	}

	public void AwakeKeeperValues()
	{
		layer = PlayerPrefs.GetInt("layer", defaultKeeper.Layer);
		currency = PlayerPrefs.GetInt("currency", defaultKeeper.Currency);
		initial = PlayerPrefs.GetInt("initial", defaultKeeper.Initial);
		secondary = PlayerPrefs.GetInt("secondary", defaultKeeper.Secondary);
		musicInit = PlayerPrefs.GetInt("musicInit", defaultKeeper.MusicInit);
		soundsInit = PlayerPrefs.GetInt("soundsInit", defaultKeeper.SoundsInit);
		schooling = PlayerPrefs.GetInt("schooling", defaultKeeper.Schooling);
	}
}

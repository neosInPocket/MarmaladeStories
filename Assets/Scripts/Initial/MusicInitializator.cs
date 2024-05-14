using UnityEngine;

public class MusicInitializator : MonoBehaviour
{
	public static MusicInitializator Initializator { get; private set; }
	[SerializeField] public AudioSource initializatorSource;
	public bool MusicInit => SafeKeeper.Entry.musicInit > 0;
	public bool EffectInit => SafeKeeper.Entry.soundsInit > 0;

	private void Awake()
	{
		if (Initializator != null)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Initializator = this;
		}
	}

	private void Start()
	{
		initializatorSource.volume = SafeKeeper.Entry.musicInit > 0 ? 1 : 0;
	}

	public void InitializeMusicToggle()
	{
		ToggleAudioSource(initializatorSource);
	}

	public void ToggleAudioSource(AudioSource audioSource)
	{
		audioSource.volume = audioSource.volume > 0 ? 0 : 1;

		SafeKeeper.Entry.musicInit = (int)audioSource.volume;
		SafeKeeper.Entry.Safer();
	}

	public void InitializeEffectsToggle()
	{
		if (SafeKeeper.Entry.soundsInit != 0)
		{
			SafeKeeper.Entry.soundsInit = 0;
		}
		else
		{
			SafeKeeper.Entry.soundsInit = 1;
		}

		SafeKeeper.Entry.Safer();
	}
}

using UnityEngine;
using UnityEngine.UI;

public class SettingsInitializator : MonoBehaviour
{
	[SerializeField] private Image musicApp;
	[SerializeField] private Image effectsApp;
	[SerializeField] private Color settingsDisactiveColor;
	[SerializeField] private Color settingsActiveColor;

	private void Start()
	{
		SetInitial();
	}

	public void SetInitial()
	{
		musicApp.color = MusicInitializator.Initializator.MusicInit ? settingsActiveColor : settingsDisactiveColor;
		effectsApp.color = MusicInitializator.Initializator.EffectInit ? settingsActiveColor : settingsDisactiveColor;
	}

	public void Music()
	{
		MusicInitializator.Initializator.InitializeMusicToggle();
		musicApp.color = MusicInitializator.Initializator.MusicInit ? settingsActiveColor : settingsDisactiveColor;
	}

	public void Sounds()
	{
		MusicInitializator.Initializator.InitializeEffectsToggle();
		effectsApp.color = MusicInitializator.Initializator.EffectInit ? settingsActiveColor : settingsDisactiveColor;
	}
}

using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
  public AudioSource src;
  public AudioClip powerupSound, bombSound, gameOverSound;

  public void PlayPowerupSound()
  {
    src.clip = powerupSound;
    src.Play();
  }

  public void PlaybombSound()
  {
    src.clip = bombSound;
    src.Play();
  }

  public void PlayGameOverSound()
  {
    src.clip = gameOverSound;
    src.Play();
  }

}

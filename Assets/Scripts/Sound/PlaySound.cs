using UnityEngine;

public class PlaySound : MonoBehaviour
{
   public void PlaySoundFootstep()
   {
        SoundManager.PlaySound(SoundType.Footstep);
   }

    public void PlaySoundClimbing()
    {
        SoundManager.PlaySound(SoundType.Climbing);
    }
}

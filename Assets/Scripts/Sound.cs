using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public void MuteGameAudio()
    {
        // Set the AudioListener's volume to 0 to mute all audio
        AudioListener.volume = 0f;
    }

    public void UnmuteGameAudio()
    {
        // Set the AudioListener's volume back to 1 to restore normal volume
        AudioListener.volume = 1f;
    }
}

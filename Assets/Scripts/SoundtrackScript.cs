using System.Threading.Tasks;
using UnityEngine;

public class SoundtrackScript : MonoBehaviour
{
    [SerializeField] private AudioClip _prefix;
    [SerializeField] private AudioClip _soundtrack;
    
    private async Task Awake()
    {
        if (FindObjectsByType<SoundtrackScript>(FindObjectsSortMode.None).Length > 1) {
            Destroy(gameObject);
        }
        GetComponent<AudioSource>().clip = _prefix;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);
        await PlaySoundtrack();
    }

    private async Task PlaySoundtrack() {
        await Task.Delay(923);
        GetComponent<AudioSource>().clip = _soundtrack;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }
}

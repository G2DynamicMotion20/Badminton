using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM;

    private Dictionary<string, AudioClip> sounds;
    private AudioSource sfx;
    private AudioSource bgm;


    // Start is called before the first frame update
    void Awake()
    {
        SM = this;
        sounds = new Dictionary<string, AudioClip>();
        sfx = GameObject.Find("PlayerRacket").GetComponent<AudioSource>();
        bgm = GetComponent<AudioSource>();
        sfx.loop = false;

        // Load Audio
        AudioClip woosh = Resources.Load("racket_woosh", typeof(AudioClip)) as AudioClip;
        AudioClip hit = Resources.Load("badminton_shuttlecock_hit_with_racket", typeof(AudioClip)) as AudioClip;
        AudioClip score = Resources.Load("score_sound", typeof(AudioClip)) as AudioClip;
        AudioClip error = Resources.Load("error_sound", typeof(AudioClip)) as AudioClip;
        AudioClip floor = Resources.Load("birdie_hittingfloor", typeof(AudioClip)) as AudioClip;

        // Input into dictionary
        sounds.Add("woosh", woosh);
        sounds.Add("hit", hit);
        sounds.Add("score", score);
        sounds.Add("error", error);
        sounds.Add("floor", floor);
    }

    public void PlaySound(string soundID, Vector3 pos, float vol = 1)
    {
        try
        {
            if ( pos == Vector3.zero)
            {
                sfx.PlayOneShot(sounds[soundID], vol);
            }
            else
            {
                AudioSource.PlayClipAtPoint(sounds[soundID], pos, vol);
            }
            
        }
        catch
        {
            throw new System.Exception("Could not locate sound - " + soundID);
        }
    }
}

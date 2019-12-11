using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip ATK1, ATK2, Death, Hurt, Skill;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        ATK1 = Resources.Load<AudioClip>("JackATK1");
        ATK2 = Resources.Load<AudioClip>("JackATK2");
        Death = Resources.Load<AudioClip>("JackDeath");
        Hurt = Resources.Load<AudioClip>("JackHurt");
        Skill = Resources.Load<AudioClip>("JackSkill1");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
    {
        switch(clip)
        {
            case "ATK1":
                audioSrc.PlayOneShot(ATK1);
                break;
            case "ATK2":
                audioSrc.PlayOneShot(ATK2);
                break;
            case "Death":
                audioSrc.PlayOneShot(Death);
                break;
            case "Hurt":
                audioSrc.PlayOneShot(Hurt);
                break;
            case "Skill":
                audioSrc.PlayOneShot(Skill);
                break;
        }
    }
}

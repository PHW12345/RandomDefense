using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;

    public enum Sfx { Arrow, Sword, Boom, Magic, Thunder};
    int sfxCursor;

    public void SfxPlay(Sfx type)
    {
        switch (type)
        {
            case Sfx.Arrow:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.Sword:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.Boom:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.Magic:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.Thunder:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                break;
        }
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int towerType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DieSkill()
    {
        Destroy(this.gameObject);
    }
    public void ProjectileSound()
    {
        AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();

        if (towerType == 1)
        {
            manager.SfxPlay(AudioManager.Sfx.Arrow);
        }
        else if (towerType == 2)
        {
            manager.SfxPlay(AudioManager.Sfx.Sword);
        }
        else if (towerType == 3)
        {
            manager.SfxPlay(AudioManager.Sfx.Boom);
        }
        else if (towerType == 4)
        {
            manager.SfxPlay(AudioManager.Sfx.Magic);
        }
    }
    public void SoundStart()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("frozen");
    }
    public void SoundStart03()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("fireboom");
    }
    public void SoundStart04()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("FireBat");
    }
    public void SoundStart05()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("FireNuclear");
    }
    
    public void SoundStart06()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("FireFlower");
    }
    public void SoundStart07()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("Thunder");
    }
    public void SoundStart08()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("Eff02");
    }
    public void SoundStart09()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("WaterBoom");
    }
    public void SoundStart10()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("Nuclear");
    }
}

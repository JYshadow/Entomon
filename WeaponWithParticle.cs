using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWithParticle : Weapon
{
    [Header("Public")]
    public ParticleSystem[] particles;

    [HideInInspector] public bool playable = true;

    private void Start()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();

        if (particle != null)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Stop();
            }
        }
    }

    public void PlayParticle()
    {
        if (playable == true)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].isPlaying == false)
                {
                    particles[i].Play();
                    //print("played");
                }
            }
        }
    }

    public void StopParticle()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
            //print("stopped");
        }
    }
}

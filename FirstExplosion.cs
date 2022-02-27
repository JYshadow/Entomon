using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstExplosion : MonoBehaviour
{
    public AudioSource firstExplosion;

    public IEnumerator PlayFirstExplosion()
    {
        yield return new WaitForSeconds(1f);
        firstExplosion.Play();
    }
}

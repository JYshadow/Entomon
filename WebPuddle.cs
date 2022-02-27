using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPuddle : MonoBehaviour
{
    GameObject player;
    SpriteRenderer webEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        webEffect = GameObject.FindGameObjectWithTag("WebEffect").GetComponent<SpriteRenderer>();

        StartCoroutine(DestroyPuddle());
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 12)  //12 = Player
        {
            //print(col.name);
            player.GetComponent<LivingEntity>().Slow(35);
            webEffect.enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 12) //12 = Player 
        {
            player.GetComponent<LivingEntity>().ResetMovespeed(35);
            webEffect.enabled = false;
        }
    }

    private IEnumerator DestroyPuddle()
    {
        yield return new WaitForSeconds(7f);
        player.GetComponent<LivingEntity>().ResetMovespeed(35);
        webEffect.enabled = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem circle;
    [SerializeField] ParticleSystem soul;
    [SerializeField] GameObject inventoryUI_1;
    [SerializeField] GameObject inventoryUI_2;


    PlayerMovement playerMovement;

    void Start(){
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void PlayParticleCircle()
    {
        circle.Play();
    }
    void PlayParticleSoul()
    {
        soul.Play();
    }
    IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(5f);
        PlayParticleCircle();
        yield return new WaitForSeconds(2f);
        PlayParticleSoul();
        yield return new WaitForSeconds(8f);
        soul.Stop();
        circle.Stop();
        inventoryUI_1.SetActive(false);
        inventoryUI_2.SetActive(false);
        yield return new WaitForSeconds(1f);
        playerMovement.Spray();

    }

    public void PlayParticle(){
        StartCoroutine(WaitForPlay());
    }
}

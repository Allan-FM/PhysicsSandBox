using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{   
    [SerializeField] private float explosionDelay = 3;
    [SerializeField] private Color explosionColorBomb = Color.red;
    [SerializeField] private ParticleSystem explosionParticles;
    private bool isCountDown = false;
    private Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.EnableKeyword("_EMISSION!");
    }
    private void OnCollisionEnter(Collision other)
    {
        if(!isCountDown)
        {
            isCountDown = true;
            StartCoroutine(CountDownAndExplode());
        }
    }
    private IEnumerator CountDownAndExplode()
    {
        var exlosionTime = Time.time + explosionDelay;
        while (Time.time < exlosionTime)
        {
            var cuePercent = Mathf.PingPong(Time.time, 1);
            var cueColor = Color.Lerp(Color.black, explosionColorBomb, cuePercent);
            rend.material.SetColor("_EMISSION COLOR", cueColor);
            yield return null;
        }

        explosionParticles.gameObject.SetActive(true);
        explosionParticles.transform.SetParent(null);
        explosionParticles.Play();

        var explosionFxTime = explosionParticles.main.duration;
        Destroy(gameObject, explosionFxTime);
        Destroy(explosionParticles.gameObject, explosionFxTime);

        gameObject.SetActive(true);
    }
}

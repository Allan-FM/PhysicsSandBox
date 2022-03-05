using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{   
    [SerializeField] private float explosionDelay = 3;
    [SerializeField] private Color explosionColorBomb = Color.red;
    [SerializeField] private float explosionRadius = 2;
    [SerializeField] private float explosionForce = 100;
    [SerializeField] private float upswardModifier = 0;
    [SerializeField] private LayerMask explosionLayer;
    
    [SerializeField] private ParticleSystem explosionParticles;

    private Collider[] colliderInRange = new Collider[20];
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

        Explode();
    }
    private void Explode()
    {
        PlayExplosionEffects();
        var colliderCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            explosionRadius,
            colliderInRange,
            explosionLayer,
            QueryTriggerInteraction.Ignore
        );


        for (int i = 0; i < colliderCount; i++)
        {
            var collider = colliderInRange[i];
            if(collider.TryGetComponent<Rigidbody>(out var rb))
            {
                // var toRB = rb.position - transform.position;
                // var percent  = Mathf.Clamp01((explosionRadius - toRB.magnitude) / explosionRadius);
                // var force = explosionForce * toRB.normalized * percent;
                // rb.AddForce(force, ForceMode.Impulse);
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upswardModifier, ForceMode.Impulse);
            }
        }
    }

    private void PlayExplosionEffects()
    {
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.transform.SetParent(null);
        explosionParticles.Play();

        var explosionFxTime = explosionParticles.main.duration;
        Destroy(gameObject, explosionFxTime);
        Destroy(explosionParticles.gameObject, explosionFxTime);

        gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

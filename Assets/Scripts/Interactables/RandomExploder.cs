using System.Collections;
using System.Collections.Generic;
using NuiN.NExtensions;
using NuiN.NExtensions.Editor;
using NuiN.SpleenTween;
using UnityEngine;

public class RandomExploder : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    [SerializeField] float explosionRadius;
    [SerializeField] float minExplosionForce;
    [SerializeField] float maxExplosionForce;

    [SerializeField] ParticleSystem particles;

    [Header("Prefab Spawning")]
    [SerializeField] PoolObjectPrefabReferenceSO prefabToSpawn;
    [SerializeField] Transform[] prefabSpawnPoints;
    [SerializeField] float cloneScaleMult;
    [SerializeField] float finalizeClonesDuration;
    [SerializeField] float cloneToKeepScaleDuration;
    [SerializeField] Ease cloneToKeepScaleEase;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        Explode();
    }

    void Explode()
    {
        List<PoolObject> spawnedObjs = new();
        foreach (var spawnPoint in prefabSpawnPoints)
        {
            PoolObject obj = Instantiate(prefabToSpawn.Prefab, spawnPoint.position, spawnPoint.rotation);
            SpleenTween.Scale(obj.transform, Vector3.zero, obj.transform.localScale *= cloneScaleMult, cloneToKeepScaleDuration).SetEase(cloneToKeepScaleEase);
            spawnedObjs.Add(obj);
        }

        SpleenTween.DoAfter(finalizeClonesDuration, () =>
        {
            PoolObject cloneToKeep = spawnedObjs.RandomItem();
            foreach (var spawnedObj in spawnedObjs)
            {
                if (spawnedObj == cloneToKeep)
                {
                    //SpleenTween.Scale(spawnedObj.transform, prefabToSpawn.Prefab.transform.localScale, cloneToKeepScaleDuration).SetEase(cloneToKeepScaleEase);
                }
                else
                {
                    SpleenTween.Scale(spawnedObj.transform, Vector3.zero, cloneToKeepScaleDuration)
                        .SetEase(Ease.InExpo)
                        .OnComplete(() => Destroy(spawnedObj.gameObject));
                }
            }
        });
        
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out Rigidbody2D hitRB))
            {
                Vector2 explosionVector = (col.transform.position - transform.position);
                Vector2 explosionDirection = (col.transform.position - transform.position).normalized;
                
                float distLerp = explosionVector.sqrMagnitude / explosionRadius;
                float explosionForce = Mathf.Lerp(maxExplosionForce, minExplosionForce, distLerp);
                
                hitRB.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
            }
        }*/
        
        Destroy(gameObject);
    }
    
    void OnDrawGizmosSelected()
    {
        GizmoUtils.DrawCircle(transform.position, explosionRadius, Color.red);
    }
}
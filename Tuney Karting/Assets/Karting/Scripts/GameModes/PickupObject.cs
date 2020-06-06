using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// This class inherits from TargetObject and represents a PickupObject.
/// </summary>
public class PickupObject : TargetObject
{
    [Serializable]
    public struct CheckpointFX
    {
        public ParticleSystem CompleteVFX;
    }
    public CheckpointFX fx;

    [Header("PickupObject")]

    [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    public GameObject spawnPrefabOnPickup;

    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;
    
    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;

    public int chkptIndex;

    public AudioPlayer audioPlayer;

    void Start() {
        Register();
    }

    void OnCollect()
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }
        if (spawnPrefabOnPickup)
        {
            var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, destroySpawnPrefabDelay);
        }
               
        Objective.OnUnregisterPickup(this);

        TimeManager.OnAdjustTime(TimeGained);

        //HEAVY TRIGGER
        audioPlayer.triggerLayer(chkptIndex);

        //vfx trigger
        fx.CompleteVFX.transform.parent = null;
        fx.CompleteVFX.Play();

        Destroy(gameObject, collectDuration);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }
}

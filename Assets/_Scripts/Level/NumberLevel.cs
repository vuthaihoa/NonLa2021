using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLevel : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    //private SpriteRenderer visual;
    //private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake()
    {
        //visual = this.GetComponentInChildren<SpriteRenderer>();
        //collectParticle = this.GetComponentInChildren<ParticleSystem>();
        //collectParticle.Stop();
    }

    public void LoadData(GameData data)
    {
        data.NumberLevel.TryGetValue(id, out collected);
        if (collected)
        {
            //visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.NumberLevel.ContainsKey(id))
        {
            data.NumberLevel.Remove(id);
        }
        data.NumberLevel.Add(id, collected);
    }

    private void OnTriggerEnter2D()
    {
        if (!collected)
        {
            //collectParticle.Play();
            CollectCoin();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collected)
        {
            //collectParticle.Play();
            CollectCoin();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collected)
        {
            //collectParticle.Play();
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        collected = true;
        //visual.gameObject.SetActive(false);
        GameEventsManager.instance.CoinCollected();
    }

}


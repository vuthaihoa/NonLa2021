using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject item;
    [Range(0.01f, 100f)]
    public float DropRate;
    public int minQuantity;
    public int maxQuantity;
}
public class OjbectDropItems : MonoBehaviour
{
    public Loot[] loots;
    
    public void DropItem()
    {
        foreach(Loot loot in loots)
        {
            float SpawnChance = Random.Range(-0.01f, 100f);
            if(SpawnChance <= loot.DropRate)
            {
                int spawnAmount = Random.Range(loot.minQuantity, loot.maxQuantity);
                for(int i =0;i<spawnAmount;i++)
                {
                    GameObject CurrentDrop = Instantiate(loot.item, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(transform.rotation.y - 40, transform.rotation.y + 40))));
                    CurrentDrop.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 50), ForceMode2D.Impulse);
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
/// <summary>
/// This class triggers a lava fountain
/// </summary>
public class SpawnDragonEnemy : MonoBehaviour 
{
    /// <summary>
    /// switches state after dragon spawned
    /// </summary>
    public bool dragonWasSpawned;
    private GameObject dragon;
    private Transform trans;

	// Use this for initialization
	void Start () 
    {
        dragonWasSpawned = false;
        dragon = (GameObject)Resources.Load("Prefabs/Enemy/EnemyDragon");
        trans = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    /// <summary>
    /// throws an empty game object on the killing plane, which spawns a lava fountain
    /// </summary>
    private void gameObjectSpawnTrigger()
    {
        Instantiate(dragon, trans.position, Quaternion.identity);  
    }

    void OnCollisionEnter(Collision collisionCollider)
    {
    }

    void OnTriggerEnter(Collider c)
    {
        switch (c.gameObject.tag)
        {
            case "Player":
                Debug.Log("playercolision");
                dragonWasSpawned = true;
                gameObjectSpawnTrigger();
                Destroy(gameObject);
                break;
        }
    }
}

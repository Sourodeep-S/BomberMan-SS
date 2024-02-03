using UnityEngine;

public class Destructible : MonoBehaviour
{
  private float destructionTime = 1f;

  [Range(0f, 1f)]
  public float spawnchance = 0.2f;
  public GameObject[] spawnableItems;

  private void Start()
  {
    Destroy(gameObject, destructionTime);
  }

  private void OnDestroy()
  {
    if (spawnableItems.Length > 0 && Random.value < spawnchance)
    {
      int index = Random.Range(0, spawnableItems.Length);
      Instantiate(spawnableItems[index], transform.position, Quaternion.identity);
    }
  }
}

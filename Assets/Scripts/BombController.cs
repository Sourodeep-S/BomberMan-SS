using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
  [Header("Bomb")]
  public GameObject bombPrefab;
  public int bombAmount = 1;
  public KeyCode DropBomb = KeyCode.Space;
  private int bombsRemaining;
  private float bomsFuseTime = 3f;

  [Header("Explosion")]
  public Explosion explosionPrefab;
  public int explosionRadius = 1;
  public float explosionDuration = 1f;
  public LayerMask explosionLayerMask;

  private void Awake()
  {
    bombsRemaining = bombAmount;
  }

  private void Update()
  {
    if (bombsRemaining > 0 && Input.GetKeyDown(DropBomb))
    {
      StartCoroutine(PlaceBomb());
    }
  }

  private IEnumerator PlaceBomb()
  {
    Vector2 position = transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);
    GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
    bombsRemaining--;

    yield return new WaitForSeconds(bomsFuseTime);

    position = bomb.transform.position;
    position.x = Mathf.Round(position.x);
    position.y = Mathf.Round(position.y);

    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.SetActiveRenderer(explosion.start);
    explosion.DestroyAfter(explosionDuration);

    Explode(position, Vector2.up, explosionRadius);
    Explode(position, Vector2.down, explosionRadius);
    Explode(position, Vector2.left, explosionRadius);
    Explode(position, Vector2.right, explosionRadius);

    Destroy(bomb);
    bombsRemaining++;
  }

  private void Explode(Vector2 position, Vector2 direction, int length)
  {
    if (length <= 0)
      return;

    position += direction;

    if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
      return;

    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
    explosion.SetDirection(direction);
    explosion.DestroyAfter(explosionDuration);

    Explode(position, direction, length - 1);

  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
      other.isTrigger = false;
  }
}

using UnityEngine;

public class Explosion : MonoBehaviour
{
  public AnimatedSpritesRenderer start;
  public AnimatedSpritesRenderer middle;
  public AnimatedSpritesRenderer end;

  public void SetActiveRenderer(AnimatedSpritesRenderer renderer)
  {
    start.enabled = renderer == start;
    middle.enabled = renderer == middle;
    end.enabled = renderer == end;
  }

  public void SetDirection(Vector2 direction)
  {
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  public void DestroyAfter(float seconds)
  {
    Destroy(gameObject, seconds);
  }
}

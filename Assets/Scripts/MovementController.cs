using UnityEngine;

public class MovementController : MonoBehaviour
{
  private Rigidbody2D rigidBody;
  private SoundEffectsPlayer sound;

  public Vector2 direction { get; private set; }
  public float speed = 5f;

  public KeyCode Up = KeyCode.UpArrow;
  public KeyCode Left = KeyCode.LeftArrow;
  public KeyCode Down = KeyCode.DownArrow;
  public KeyCode Right = KeyCode.RightArrow;

  public AnimatedSpritesRenderer spritesRendererUp;
  public AnimatedSpritesRenderer spritesRendererDown;
  public AnimatedSpritesRenderer spritesRendererLeft;
  public AnimatedSpritesRenderer spritesRendererRight;
  public AnimatedSpritesRenderer spritesRendererDeath;
  private AnimatedSpritesRenderer activespritesRenderer;

  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    activespritesRenderer = spritesRendererDown;
    sound = GetComponent<SoundEffectsPlayer>();
  }

  private void Update()
  {
    if (Input.GetKey(Up))
    {
      SetDirection(Vector2.up, spritesRendererUp);
    }
    else if (Input.GetKey(Left))
    {
      SetDirection(Vector2.left, spritesRendererLeft);
    }
    else if (Input.GetKey(Down))
    {
      SetDirection(Vector2.down, spritesRendererDown);
    }
    else if (Input.GetKey(Right))
    {
      SetDirection(Vector2.right, spritesRendererRight);
    }
    else
    {
      SetDirection(Vector2.zero, activespritesRenderer);
    }
  }

  private void FixedUpdate()
  {
    Vector2 position = rigidBody.position;
    Vector2 translation = direction * speed * Time.fixedDeltaTime;
    rigidBody.MovePosition(position + translation);
  }

  private void SetDirection(Vector2 newDirection, AnimatedSpritesRenderer animatedSpritesRenderer)
  {
    direction = newDirection;

    spritesRendererUp.enabled = animatedSpritesRenderer == spritesRendererUp;
    spritesRendererDown.enabled = animatedSpritesRenderer == spritesRendererDown;
    spritesRendererLeft.enabled = animatedSpritesRenderer == spritesRendererLeft;
    spritesRendererRight.enabled = animatedSpritesRenderer == spritesRendererRight;

    activespritesRenderer = animatedSpritesRenderer;
    activespritesRenderer.idle = direction == Vector2.zero;

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Explosion"))
    {
      OnDeathSequence();
    }
    else if (other.CompareTag("Item"))
    {
      sound.PlayPowerupSound();
    }
  }

  private void OnDeathSequence()
  {
    sound.PlayGameOverSound();
    enabled = false;
    GetComponent<BombController>().enabled = false;
    spritesRendererDown.enabled = false;
    spritesRendererRight.enabled = false;
    spritesRendererUp.enabled = false;
    spritesRendererLeft.enabled = false;

    spritesRendererDeath.enabled = true;

    Invoke(nameof(EndDeathSequence), 1.5f);

  }

  private void EndDeathSequence()
  {
    gameObject.SetActive(false);
    FindAnyObjectByType<GameManager>().CheckWinState();
  }
}

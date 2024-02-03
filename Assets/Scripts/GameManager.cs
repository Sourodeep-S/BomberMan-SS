using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
  public GameObject[] players;
  public TextMeshProUGUI winText;

  public void CheckWinState()
  {
    foreach (GameObject player in players)
    {
      if (player.activeSelf)
      {
        winText.text = player.name + " wins!";
      }
    }

    winText.gameObject.SetActive(true);
    Invoke(nameof(NewGame), 2f);
  }

  private void NewGame()
  {
    winText.gameObject.SetActive(false);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}

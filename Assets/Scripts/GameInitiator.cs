using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private GameRoundManager roundUpdater;

    private void Awake()
    {
        if (roundUpdater != null && !roundUpdater.gameObject.activeSelf)
        {
            roundUpdater.gameObject.SetActive(true);
        }

        // Removed `SetUpRound()` from here, handled in GameRoundManager Start()
    }
}

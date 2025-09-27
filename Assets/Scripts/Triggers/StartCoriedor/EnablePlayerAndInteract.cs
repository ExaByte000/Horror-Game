using UnityEngine;

public class EnablePlayerAndInteract : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerInteract;
    [SerializeField] private GameObject cutsceneCamera;

    public void ActivatePostCutsceneObjects()
    {
        playerInteract.SetActive(true);
        player.SetActive(true);
        cutsceneCamera.SetActive(false);
    }
}

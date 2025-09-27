using FpsHorrorKit;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private GameObject playerInteract;
    [SerializeField] private GameObject candle;
    [SerializeField] private FpsController controller;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        playerInteract.SetActive(false);
        CutsceneManager.Instance.StartCutscene("WakeUp");
        animator.Rebind();
        animator.Update(0f);
        candle.SetActive(false);
        other.gameObject.SetActive(false);
        FpsAssetsInputs.Instance.move = Vector2.zero;
        FpsAssetsInputs.Instance.look = Vector2.zero;
        controller.CameraPich = 0f;
        controller.Velocity = Vector3.zero;

        other.gameObject.transform.position = new Vector3(-9.4f, 1f, -7.34f);
        other.gameObject.transform.rotation = Quaternion.Euler(0f, -90, 0f);

        
    }
}

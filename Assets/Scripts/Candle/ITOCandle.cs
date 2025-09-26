using FpsHorrorKit;
using UnityEngine;

public class ITOCandle : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject candle;
    private string interactText = "Take candle [E]";

    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText);
    }

    public void Interact()
    {
        candle.SetActive(true);
        Destroy(gameObject);
    }

    public void UnHighlight(){}

    public void HoldInteract(){}
}

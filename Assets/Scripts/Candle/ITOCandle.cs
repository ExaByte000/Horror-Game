using FpsHorrorKit;
using System;
using UnityEngine;

public class ITOCandle : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject candle;
    private string interactText = "Take candle [E]";
    public static Action CandleTaken; 

    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText);
    }

    public void Interact()
    {
        candle.SetActive(true);
        CandleTaken.Invoke();
        Destroy(gameObject);
    }

    public void UnHighlight(){}

    public void HoldInteract(){}
}

using UnityEngine;

public class VanishDoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject wall3x1;
    [SerializeField] private GameObject wall3x3;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(door);
        Destroy(wall3x1);
        wall3x3.SetActive(true);
    }
}

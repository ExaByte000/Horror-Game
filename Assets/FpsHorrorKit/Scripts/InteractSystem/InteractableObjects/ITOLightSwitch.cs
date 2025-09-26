namespace FpsHorrorKit
{
    using System.Collections;
    using UnityEngine;

    public class ITOLightSwitch : MonoBehaviour, IInteractable
    {
        [Header("Light Settings")]
        public Transform lightSwitchButton;

        [Header("Rotation Settings")]
        public float onRotationAngle = -30f;
        public float offRotationAngle = 30f;
        public float rotationSpeed = 200f;

        [Header("Interact UI")]
        [SerializeField] private string interactText = "Light Open/Close [E]";

        private bool isLightOn = false;
        private bool isRotating = false;

        private void Start()
        {
            // Устанавливаем начальное положение (выключено)
            lightSwitchButton.localEulerAngles = new Vector3(offRotationAngle, 0, 0);
        }

        public void Interact()
        {
            if (isRotating) return;

            // Переключаем состояние
            isLightOn = !isLightOn;

            // Определяем целевой угол в зависимости от нового состояния
            float targetRotation = isLightOn ? onRotationAngle : offRotationAngle;

            // Получаем текущий угол
            float currentRotation = lightSwitchButton.localEulerAngles.x;

            // Нормализуем углы для корректного сравнения
            if (currentRotation > 180f) currentRotation -= 360f;

            StartCoroutine(RotateToTarget(targetRotation, currentRotation));
        }

        private IEnumerator RotateToTarget(float targetRotation, float startRotation)
        {
            isRotating = true;

            float currentRotation = startRotation;
            float rotationDirection = Mathf.Sign(targetRotation - startRotation);

            while (Mathf.Abs(targetRotation - currentRotation) > 0.1f)
            {
                float step = rotationSpeed * Time.deltaTime * rotationDirection;
                currentRotation += step;

                // Проверяем, не переходим ли мы через целевое значение
                if (rotationDirection > 0 && currentRotation >= targetRotation ||
                    rotationDirection < 0 && currentRotation <= targetRotation)
                {
                    currentRotation = targetRotation;
                }

                lightSwitchButton.localEulerAngles = new Vector3(currentRotation, 0, 0);
                yield return null;
            }

            // Устанавливаем точное финальное значение
            lightSwitchButton.localEulerAngles = new Vector3(targetRotation, 0, 0);
            isRotating = false;

            Debug.Log($"Light Switcher: {(isLightOn ? "ON" : "OFF")} - Angle: {targetRotation}");
        }

        public void Highlight()
        {
            PlayerInteract.Instance.ChangeInteractText(interactText);
        }

        public void HoldInteract() { }
        public void UnHighlight() { }
    }
}
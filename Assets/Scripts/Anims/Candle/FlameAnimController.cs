using FpsHorrorKit;
using UnityEngine;

public class FlameAnimController : MonoBehaviour
{
    [SerializeField] private FpsAssetsInputs input;
    [SerializeField] private float velocityDumpTime;
    [SerializeField] private float lookDumpTime;

    private Animator candleAnimator;

    private void Start()
    {
        candleAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        candleAnimator.SetFloat("VelocityX", input.move.x, velocityDumpTime, Time.deltaTime);
        candleAnimator.SetFloat("VelocityZ", input.move.y, velocityDumpTime, Time.deltaTime);

        candleAnimator.SetFloat("LookX", SignWithZero(input.look.x), lookDumpTime, Time.deltaTime);
        candleAnimator.SetFloat("LookY", SignWithZero(input.look.y), lookDumpTime, Time.deltaTime);

    }
    private float SignWithZero(float value)
    {
        if (value > 0) return 1f;
        if (value < 0) return -1f;
        return 0f;
    }
}
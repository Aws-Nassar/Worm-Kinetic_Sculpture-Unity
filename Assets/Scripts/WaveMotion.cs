using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    public Transform[] leftWings;
    public Transform[] rightWings;

    public float initialAngle = 90f;
    public float maxRotation = 80f;
    public float minRotation = 40f;
    public float delayBetweenPairs = 0.3f;
    public float maxRotationSpeed = 0.7f;
    public float minRotationSpeed = 0.3f;
    private float timeCounter = 0f;

    void Update()
    {
        timeCounter += Time.deltaTime;

        for (int i = 0; i < leftWings.Length; i++)
        {
            float pairDelay = i * delayBetweenPairs;

            if (timeCounter > pairDelay)
            {
                float targetRotation = CalculateRotationAngle(i);
                float cycleTime = (timeCounter - pairDelay);
                float oscillationProgress = cycleTime / (i * delayBetweenPairs);
                float rotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, oscillationProgress);
                float oscillation = Mathf.PingPong(cycleTime * rotationSpeed, 1f);
                float rightAngle = initialAngle - oscillation * (initialAngle - targetRotation);
                float leftAngle = initialAngle + oscillation * (initialAngle - targetRotation);

                leftWings[i].localRotation = Quaternion.Euler(0, 0, leftAngle);
                rightWings[i].localRotation = Quaternion.Euler(0, 0, rightAngle);
            }
        }
    }

    float CalculateRotationAngle(int index)
    {
        int cycleIndex = index % 9;
        float[] rotationSequence = { 80f, 70f, 60f, 50f, 40f, 50f, 60f, 70f, 80f };

        return rotationSequence[cycleIndex];
    }
}
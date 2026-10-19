using System.Collections;
using TMPro;
using UnityEngine;


[ExecuteInEditMode]

public class TextAnimatior : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private float stringAnimationDuration;
    [SerializeField] TextMeshProUGUI animatedText;
    [SerializeField] private AnimationCurve sizeCurve;
    [SerializeField] private float sizeScale;
    [SerializeField, Range(0.0001f, 1f)] private float charAnimationDuration;
    [SerializeField] private float editorTValue;


    private float timeElapsed;

    private void Start()
    {
        StartCoroutine(RunAnimation(3));
    }

    private void Update()
    {
        EvaluateRichText(editorTValue);
    }

    public void EvaluateRichText(float t)
    {
        animatedText.text = "";

        for (int i = 0; i < message.Length; i++)
        {
            animatedText.text += EvaluateCharRichText(message[i], message.Length, i, t);
        }
    }

    IEnumerator RunAnimation(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        float t = 0;
        while (t <= 1f)
        {
            EvaluateRichText(t);
            t = timeElapsed / stringAnimationDuration;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    // Change return type from 'void' to 'string'
    public string EvaluateCharRichText(char c, int sLength, int cPosition, float t)
    {
        float startPoint = (1 - charAnimationDuration) / (sLength - 1) * cPosition;
        float endPoint = startPoint + charAnimationDuration;

        float subT = t.map(startPoint, endPoint, 0, 1);

        string sizeStart = $"<size={sizeCurve.Evaluate(subT) * sizeScale}%>";
        string sizeEnd = "</size>";

        // Return the combined string including the character 'c'
        return $"{sizeStart}{c}{sizeEnd}";
    }
}

public static class Extensions
{
    public static float map(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;

    }
}
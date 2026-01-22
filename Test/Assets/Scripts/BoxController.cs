using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float animationDuration = 1f;

    public RectTransform personHolder;

    RectTransform rt;

    Vector2 startSize;
    Vector2 targetSize;

    Vector3 startPersonSize;
    Vector3 targetPersonSize;

    float timer;
    bool animating;
    bool animatingPerson;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        startPersonSize = personHolder.sizeDelta;
    }

    public void AnimateTo(Vector2 newTargetSize)
    {
        startSize = rt.sizeDelta;
        targetSize = newTargetSize;
        timer = 0f;
        animating = true;
    }

    public void AnimatePersonTo(Vector3 newTargetPersonSize)
    {
        startPersonSize = personHolder.localScale;
        targetPersonSize = newTargetPersonSize;
        animatingPerson = true;
    }

    void Update()
    {
        if (!animating) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / animationDuration);
        float eased = Mathf.Sin(t * Mathf.PI * 0.5f);

        rt.sizeDelta = Vector3.Lerp(startSize, targetSize, eased);
        if (animatingPerson)
        {
            personHolder.localScale = Vector3.Lerp(startPersonSize, targetPersonSize, eased);
        }

        if (t >= 1f)
            animating = false;
    }
}

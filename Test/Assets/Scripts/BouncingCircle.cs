using UnityEngine;

public class BouncingCircle : MonoBehaviour
{
    public Vector2 velocity = new Vector2(5f, 4f);
    public RectTransform mover;
    public float animationDuration = 1f;
    public float pulsateAmplitude = 0.1f;

    SpriteRenderer sr;
    RectTransform rt;

    Camera cam;
    float radius;
    float timer;
    Vector3 startScale;
    Vector3 endScale;
    float pulsationMultiplier = 1f;
    void Start()
    {
        cam = Camera.main;
        rt = GetComponent<RectTransform>();
        
        // Assumes uniform scale + SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
        radius = sr.bounds.extents.x;
        timer = 0f;

        // Starting pulsation
        startScale = rt.localScale.x*Vector3.one;
        endScale = (startScale.x + pulsateAmplitude)*Vector3.one;
    }

    void Update()
    {
        Pulsate();
        checkCollisions();
    }

    private void Pulsate() 
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / animationDuration);
        float eased = Mathf.Sin(t * Mathf.PI * 0.5f);
        rt.localScale = Vector3.Lerp(startScale, endScale, eased);
        if (t >= 1f) 
        {
            timer = 0f;
            pulsateAmplitude *= -1;
            startScale = rt.localScale.x * Vector3.one;
            endScale = (startScale.x + pulsateAmplitude) * Vector3.one;
        }
        radius = sr.bounds.extents.x;
    }

    private void checkCollisions()
    {
        mover.transform.position += (Vector3)(velocity * Time.deltaTime);

        Vector3 pos = transform.position;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        if (pos.x - radius <= min.x || pos.x + radius >= max.x)
            velocity.x *= -1;

        if (pos.y - radius <= min.y || pos.y + radius >= max.y)
            velocity.y *= -1;
    }
}

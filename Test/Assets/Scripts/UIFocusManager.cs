using UnityEngine;

public class UIFocusManager : MonoBehaviour
{
    public BoxController topLeft;
    public BoxController topRight;
    public BoxController bottomLeft;
    public BoxController bottomRight;

    public float totalWidth;
    public float totalHeight;


    public float maxWidth;
    public float maxHeight;

    public float personMaxSize;
    public float personMinSize;

    private float minWidth => totalWidth - maxWidth;
    private float minHeight => totalHeight - maxHeight;

    BoxController currentFocus;

    void Start()
    {
        ResetAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Focus(topLeft);
        if (Input.GetKeyDown(KeyCode.G)) Focus(topRight);
        if (Input.GetKeyDown(KeyCode.B)) Focus(bottomLeft);
        if (Input.GetKeyDown(KeyCode.Y)) Focus(bottomRight);

        if (Input.GetKeyDown(KeyCode.Escape))
            ResetAll();
    }

    void ResetAll()
    {
        currentFocus = null;

        Vector2 neutral = new Vector2(
            (minWidth + maxWidth) * 0.5f,
            (minHeight + maxHeight) * 0.5f
        );

        AnimateAll(neutral, neutral, neutral, neutral);
    }

    void Focus(BoxController selected)
    {
        currentFocus = selected;

        Vector2 maxMax = new(maxWidth, maxHeight);
        Vector2 minMin = new(minWidth, minHeight);
        Vector2 maxMin = new(maxWidth, minHeight);
        Vector2 minMax = new(minWidth, maxHeight);

        Vector3 personMax = new(personMaxSize, personMaxSize, personMaxSize);
        Vector3 personMin = new(personMinSize, personMinSize, personMinSize);

        if (selected == topLeft)
        {
            AnimateAll(maxMax, minMax, maxMin, minMin);
            topLeft.AnimatePersonTo(personMax);
            topRight.AnimatePersonTo(personMin);
            bottomLeft.AnimatePersonTo(personMin);
            bottomRight.AnimatePersonTo(personMin);
        }
        else if (selected == topRight)
        {
            AnimateAll(minMax, maxMax, minMin, maxMin);
            topRight.AnimatePersonTo(personMax);
            topLeft.AnimatePersonTo(personMin);
            bottomLeft.AnimatePersonTo(personMin);
            bottomRight.AnimatePersonTo(personMin);
        }
        else if (selected == bottomLeft)
        {
            AnimateAll(maxMin, minMin, maxMax, minMax);
            bottomLeft.AnimatePersonTo(personMax);
            topLeft.AnimatePersonTo(personMin);
            topRight.AnimatePersonTo(personMin);
            bottomRight.AnimatePersonTo(personMin);
        }
        else if (selected == bottomRight)
        {
            AnimateAll(minMin, maxMin, minMax, maxMax);
            bottomRight.AnimatePersonTo(personMax);
            topLeft.AnimatePersonTo(personMin);
            topRight.AnimatePersonTo(personMin);
            bottomLeft.AnimatePersonTo(personMin);
        }
    }

    void AnimateAll(
        Vector2 tl,
        Vector2 tr,
        Vector2 bl,
        Vector2 br)
    {
        topLeft.AnimateTo(tl);
        topRight.AnimateTo(tr);
        bottomLeft.AnimateTo(bl);
        bottomRight.AnimateTo(br);
    }
}

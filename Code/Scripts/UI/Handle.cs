using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        Scrollbar scrollbar;
        RectTransform rectTransform;

        rectTransform = transform.GetComponent<RectTransform>();
        scrollbar = transform.parent.transform.parent.GetComponent<Scrollbar>();
        scrollbar.size = 0;

        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -5);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 5);

        scrollbar.size = 0.5f;

        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -5);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 5);
    }

}
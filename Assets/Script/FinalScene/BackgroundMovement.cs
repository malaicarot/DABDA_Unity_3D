using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] Image pain;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Button button;
    [SerializeField] float duration = 1f;


    void Start()
    {
        ActiveContent(false);

    }
    void Update()
    {
        Movement(pain, duration);
    }

    void ActiveContent(bool active)
    {
        textMesh.gameObject.SetActive(active);
        button.gameObject.SetActive(active);
    }

    void Movement(Image targetImage, float duration)
    {
        float newScale = Mathf.PingPong(Time.time * duration, 1f);
        Vector3 currentScale = targetImage.transform.localScale;
        if (targetImage.transform.localScale.x <= 0.98)
        {
            currentScale.x = newScale;
            targetImage.transform.localScale = currentScale;
        }else{
            ActiveContent(true);
        }
    }
}

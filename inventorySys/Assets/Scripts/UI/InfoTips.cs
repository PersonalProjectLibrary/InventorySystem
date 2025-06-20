
using UnityEngine;
using UnityEngine.UI;

public class InfoTips : MonoBehaviour
{
    #region 单例模式
    private static InfoTips _instance;
    public static InfoTips Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InfoTips").GetComponent<InfoTips>();
            }
            return _instance;
        }
    }
    #endregion

    private float targetAlpha;
    private float smoothing = 1f;
    private CanvasGroup canvasGroup;
    private Text tipsText;
    private Text infoText;
    private void Init()
    {
        tipsText = GetComponent<Text>();
        infoText = transform.Find("TxtContent").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        targetAlpha = Constant.HideAlpha;
        smoothing = Constant.AlphaSmoothing;
    }
    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    public void Show(string info)
    {
        tipsText.text = info;
        infoText.text = info;
        targetAlpha = Constant.ShowAlpha;
    }
    public void Hide()
    {
        targetAlpha = Constant.HideAlpha;
    }

    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFishGame : MonoBehaviour
{
    [SerializeField] private FishGameResult gameResult;
    [Header("GameStart")]
    [SerializeField] private RectTransform gameStartRect;
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private TMP_Text startBaitText;

    [Header("GamePlay")]
    [SerializeField] private RectTransform gamePlayRect;
    [SerializeField] private Button castBtn;
    [SerializeField] private Button gameExitBtn;
    [SerializeField] private TMP_Text baitText;
    [SerializeField] private TMP_Text pointText;

    [Header("Result")]
    [SerializeField] private RectTransform resultRect;
    [SerializeField] private Button okBtn;

    private int baitNum = 4;
    private int pointNum = 0;


    void Start()
    {
        SetBtnListener();
    }

    // �̳��� ���� ��� 
    void GameReStart()
    {
        startBaitText.text = baitNum.ToString();
        ChangeRect(gamePlayRect, gameStartRect);
        gameStartBtn.enabled = false;   
    }

    void SetBtnListener()
    {
        // ���ӽ��� ��ư
        gameStartBtn.onClick.AddListener(() => 
        {
            ChangeRect(gameStartRect, gamePlayRect);
            SetBaitAndPoint(baitNum, pointNum);
        });

        // ���� ���� ��ư
        gameExitBtn.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        // ĳ��Ʈ ��ư
        castBtn.onClick.AddListener(OnClickCastBtn);
    }

    void OnClickCastBtn()
    {
        castBtn.enabled = false;
        resultRect.gameObject.SetActive(true);  

        baitNum--;
        pointNum += gameResult.ReturnPoint();
        SetBaitAndPoint(baitNum, pointNum);

        okBtn.onClick.AddListener(() =>
        {
            castBtn.enabled = true;
            resultRect.gameObject.SetActive(false);
            
            // �̳��� ������ ���� ��ŸƮ ȭ������ �̵�
            if(baitNum == 0)
            {
                GameReStart();
            }
        });
    }

    void SetBaitAndPoint(int baitNum, int pointNum)
    {
        baitText.text = string.Format("X{0}", baitNum);
        pointText.text = string.Format("X{0}", pointNum);
    }

    void ChangeRect(RectTransform hide, RectTransform show)
    {
        hide.gameObject.SetActive(false);
        show.gameObject.SetActive(true);
    }
}

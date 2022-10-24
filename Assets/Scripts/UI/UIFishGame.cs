using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFishGame : MonoBehaviour
{
    [SerializeField] private FishGameManager fishGameManager;
    [Header("GameStart")]
    [SerializeField] private RectTransform gameStartRect;
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private TMP_Text startBaitText;

    [Header("GamePlay")]
    [SerializeField] private RectTransform gamePlayRect;
    [SerializeField] private Button castBtn;
    [SerializeField] private Button gameExitBtn;
    [SerializeField] private Button rewardBtn;
    [SerializeField] private TMP_Text baitText;
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private TMP_Text recycleText;

    [Header("Result")]
    [SerializeField] private RectTransform resultRect;
    [SerializeField] Image itemImage;
    [SerializeField] Sprite[] itemImages;
    [SerializeField] Image comboImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPointText;
    [SerializeField] private Button okBtn;

    void Start()
    {
        SetBtnListener();
        fishGameManager.onComboAction += ShowComboEffect;   // action
    }

    void SetBtnListener()
    {
        // 게임시작 버튼
        gameStartBtn.onClick.AddListener(() => 
        {
            fishGameManager.UpdateGgameState(GameState.start);
            ChangeGameUI();
        });

        // 게임 종료 버튼
        gameExitBtn.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        // 캐스트 버튼
        castBtn.onClick.AddListener(OnClickCastBtn);

        // 리사이클 보상 버튼
        rewardBtn.onClick.AddListener(() =>
        {
            fishGameManager.RewardRecycleItem();
            UpdateRecycleAndPointUI();
        });
    }

    void OnClickCastBtn()
    {
        castBtn.enabled = false;
        resultRect.gameObject.SetActive(true);

        // 랜덤 아이템 생성
        fishGameManager.StartFishing((fish)=>
        {
            ShowItemResult(fish);
        });  
        
        okBtn.onClick.AddListener(() =>
        {
            castBtn.enabled = true;
            resultRect.gameObject.SetActive(false);
            ChangeGameUI();
        });
    }

    void SetGameStartUI()
    {
        startBaitText.text = fishGameManager.Bait.ToString();
    }

    void ShowItemResult(FishItem fish)
    {
        itemNameText.text = fish.name;   
        itemImage.sprite = itemImages[fish.imageIndex];

        // 리사이클일 경우 포인트 대신 eco 작성
        if(fish.type == FishItemType.recycle)
        {
            itemPointText.text = "eco";
            UpdateRecycleAndPointUI();
        }
        else
        {
            itemPointText.text = fish.randomPoint.ToString();
        }
            
        UpdateBaitAndPointUI();
    }

    void UpdateBaitAndPointUI()
    {
        // 남은 미끼, 총 포인트 표시
        baitText.text = string.Format("X{0}", fishGameManager.Bait.ToString() );
        pointText.text = string.Format("X{0}", fishGameManager.Point.ToString());
    }

    void UpdateRecycleAndPointUI()
    {
        // 리사이클 수 보상 포인트 업데이트
        recycleText.text = fishGameManager.Recycle.ToString();
        pointText.text = string.Format("X{0}", fishGameManager.Point.ToString());
    }

    void ShowComboEffect(bool isTrue)
    {
        comboImage.gameObject.SetActive(isTrue);
    }

    void ChangeGameUI()
    {
        GameState state = fishGameManager.ReturnGameState();
    
        switch (state)
        {
            case GameState.ready:
                gamePlayRect.gameObject.SetActive(false);
                gameStartRect.gameObject.SetActive(true);
                SetGameStartUI();
                break;
            case GameState.start:
                gameStartRect.gameObject.SetActive(false);
                gamePlayRect.gameObject.SetActive(true);
                UpdateBaitAndPointUI();
                break;
            case GameState.end:
                gamePlayRect.gameObject.SetActive(false);
                gameStartRect.gameObject.SetActive(true);
                gameStartBtn.enabled = false;
                SetGameStartUI();
                break;
        }
    }
}
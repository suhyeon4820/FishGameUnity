using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFishGame : MonoBehaviour
{
    [SerializeField] FishGameManager fishGameManager;

    [Header("Menu")]
    [SerializeField] Button storeBtn;


    [Header("GameStart")]
    [SerializeField] RectTransform gameStartRect;
    [SerializeField] Button gameStartBtn;

    [Header("GamePlay")]
    [SerializeField] RectTransform gamePlayRect;
    [SerializeField] Button castBtn;
    [SerializeField] Button gameExitBtn;
    [SerializeField] Button rewardBtn;
    [SerializeField] TMP_Text baitText;
    [SerializeField] TMP_Text pointText;
    [SerializeField] TMP_Text recycleText;

    [Header("Result")]
    [SerializeField] RectTransform resultRect;
    [SerializeField] Image itemImage;
    [SerializeField] Sprite[] itemImages;
    [SerializeField] Image comboImage;
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemPointText;
    [SerializeField] Button okBtn;

    [Header("Store")]
    [SerializeField] RectTransform storeRect;
    [SerializeField] Button upBtn;
    [SerializeField] Button downBtn;
    [SerializeField] Button purchaseBtn;
    [SerializeField] Button cancleBtn;
    [SerializeField] TMP_Text addedBaitCountText;
    [SerializeField] TMP_Text warningText;
    int addedBaitCount = 0;

    void Start()
    {
        SetBtnListener();
        SetStoreBtnListener();
        fishGameManager.onComboAction += ShowComboEffect;   // action

        fishGameManager.UpdateGgameState(GameState.ready);
        UpdateGameUIByGameState();
    }

    #region // fishing
    void SetBtnListener()
    {
        // 게임시작 버튼
        gameStartBtn.onClick.AddListener(() =>
        {
            fishGameManager.UpdateGgameState(GameState.start);
            UpdateGameUIByGameState();
        });

        // 게임 종료 버튼
        gameExitBtn.onClick.AddListener(() =>
        {
            fishGameManager.UpdateGgameState(GameState.ready);
            UpdateGameUIByGameState();
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
        fishGameManager.StartFishing((fish) =>
        {
            ShowItemResult(fish);
        });

        okBtn.onClick.AddListener(() =>
        {
            castBtn.enabled = true;
            resultRect.gameObject.SetActive(false);
            UpdateGameUIByGameState(); // bait 0일 때 화면 전환
        });
    }

    void ShowItemResult(FishItem item)
    {
        itemNameText.text = item.name;
        itemImage.sprite = itemImages[item.imageIndex];

        // 리사이클일 경우 포인트 대신 eco 작성
        if (item.type == FishItemType.recycle)
        {
            itemPointText.text = "eco";
            UpdateRecycleAndPointUI();
        }
        else
        {
            itemPointText.text = item.randomPoint.ToString();
        }
        UpdateBaitAndPointUI();
    }
    #endregion

    #region // store
    void SetStoreBtnListener()
    {
        // 상점 버튼 - 초기화
        storeBtn.onClick.AddListener(() =>
        {
            addedBaitCount = 0;
            addedBaitCountText.text = addedBaitCount.ToString();
            warningText.gameObject.SetActive(false);
            storeRect.gameObject.SetActive(true);
        });
        // 수량 증가
        upBtn.onClick.AddListener(() =>
        {
            OnClickAddOrMinusBaitCount(1);
        });
        // 수량 감소
        downBtn.onClick.AddListener(() =>
        {
            OnClickAddOrMinusBaitCount(-1);
        });
        // 구매하기
        purchaseBtn.onClick.AddListener(() =>
        {
            fishGameManager.PurchaseBait(addedBaitCount, () =>
            {
                storeRect.gameObject.SetActive(false);
                UpdateGameUIByGameState();
            });
            warningText.gameObject.SetActive(true); // 포인트 없을 때 경고표시
            
        });
        // store창 비활성화
        cancleBtn.onClick.AddListener(() =>
        {
            storeRect.gameObject.SetActive(false);
        });
    }

    void OnClickAddOrMinusBaitCount(int num)
    {
        addedBaitCount += num;

        if (addedBaitCount <= 0)
            addedBaitCount = 0;

        addedBaitCountText.text = addedBaitCount.ToString();
    }

    #endregion

    #region // updateUI

    void UpdateBaitAndPointUI()
    {
        // 남은 미끼, 총 포인트 표시
        baitText.text = string.Format("X{0}", fishGameManager.Bait.ToString());
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

    void UpdateGameUIByGameState()
    {
        GameState state = fishGameManager.ReturnGameState();

        switch (state)
        {
            case GameState.ready:
                gamePlayRect.gameObject.SetActive(false);
                gameStartRect.gameObject.SetActive(true);
                // 미끼 0일 때 시작버튼 비활성화
                if(fishGameManager.Bait == 0)
                    gameStartBtn.enabled = false;
                else
                    gameStartBtn.enabled = true;

                break;
            case GameState.start:
                gameStartRect.gameObject.SetActive(false);
                gamePlayRect.gameObject.SetActive(true);
                break;
        }

        UpdateBaitAndPointUI();
    }
    #endregion
}
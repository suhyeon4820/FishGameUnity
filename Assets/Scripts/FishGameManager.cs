using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    ready,
    start,
    end
}

public class FishGameManager : MonoBehaviour
{
    GameState gameState = new GameState();
    List<FishItem> gameItemList = new List<FishItem>();
    public event Action<bool> onComboAction;
    
    // 미끼 
    int baitCount;
    public int Bait
    {
        get { return baitCount; }
        private set 
        { 
            if(value == 0)
            {
                baitCount = 0;
                UpdateGgameState(GameState.end);
            }
            else
            {
                baitCount = value;
            }
        }
    }

    // 포인트
    public int Point { get; private set; }
    // 리사이클
    public int Recycle { get; private set; }

    int comboCount = 0;

    private void Awake()
    {
        SetGameItemList();
    }
    private void Start()
    {
        UpdateGgameState(GameState.ready);
    }

    // UIFishGame에서 호출
    public void StartFishing(Action<FishItem> onFish)
    {
        // 1. 미끼 감소
        --Bait;
        // 2. 아이템 랜덤 생성
        FishItem randomItem = ReturnRandomItem();
        // 3. 아이템 포인트 계산(3콤보 포함) 및 적용
        int randomItemPoint = CalRandomItemPoint(randomItem);
        
        onFish(randomItem);
    }

    // 랜덤 아이템의 포인트를 계산 + 3콤보시 포인트 2배 + 리사이클 포인트 적립 X
    int CalRandomItemPoint(FishItem fishingRandomItem)
    {
        // 포인트 가져오기
        fishingRandomItem.SetPoint();
        int currentPoint = fishingRandomItem.randomPoint;

        // 3콤보 카운트
        if (fishingRandomItem.type == FishItemType.fish)
            comboCount++;
        else
            comboCount = 0;

        // 리사이클 포인트 따로 보상
        if (fishingRandomItem.type == FishItemType.recycle)
        {
            Recycle++;
            currentPoint = 0;
        }

        // 3콤보 포인트 2배
        if (comboCount == 3)
        {
            currentPoint *= 2;              // 3콤보 포인트 2배
            comboCount = 0;                 // 콤보 카운트 초기화
            onComboAction?.Invoke(true);    // X2 이미지 보여지기
        }
        else
        {
            onComboAction?.Invoke(false);   // 3콤보 이후에 다시 이미지 비활성화
        }
        Point += currentPoint;   // 포인트 적용

        Debug.Log("currentPoint : " + currentPoint + ", totalPoint : " + Point); // 포인트 확인용

        return currentPoint;
    }

    // 랜덤으로 게임 아이템 리턴(물고기, 젤리, 리사이클 중 1개)
    FishItem ReturnRandomItem()
    {
        FishItem randomItem = null; 

        // 가중치    : 물고기1(5%) | 물고기2(10%) | 물고기3(20%) | 물고기4(40%) | 젤리(10%) | 재활용1(5%) | 재활용2(5%) | 재활용3(5%) | 
        // 가중치 합 : 물고기1(5%) | 물고기2(15%) | 물고기3(35%) | 물고기4(75%) | 젤리(85%) | 재활용1(90%) | 재활용2(95%) | 재활용3(100%) |
        List<FishItem> randomItemList = gameItemList;

        // 가중치 랜덤 뽑기 - 각각의 요소가 뽑힐 확률 : (가중치) / (전체 가중치 합)
        int weight = 0;
        int selectNum = UnityEngine.Random.Range(1, 101); // 1 ~ 100

        for (int i = 0; i < randomItemList.Count; i++)
        {
            weight += randomItemList[i].rate; // 가중치 합
            if (selectNum <= weight) // 가중치가 선택한 수 이내에 있으면 선택
            {
                randomItem = randomItemList[i];
                break;
            }
        }

        return randomItem;
    }
    public void RewardRecycleItem()
    {
        Recycle--;      // 수량 감소
        Point += 10;    // 10 포인트 획득
    }

    public void UpdateGgameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.ready:
                OnGameStateReady();
                break;
            case GameState.start:
                OnGameStateStart();
                break;
            case GameState.end:
                OnGameStateEnd();
                break;
        }
    }

    void OnGameStateReady()
    {
        baitCount = 5;
    }

    void OnGameStateStart()
    {
        baitCount = 5;
        Point = 0;
    }

    void OnGameStateEnd()
    {
        baitCount = 0;
    }

    public GameState ReturnGameState()
    {
        return gameState;
    }

    void SetGameItemList()
    {
        // 생성한 아이템 인스턴스 리스트화
        FishItem fishItem1 = new FishItem(FishItemType.fish,    "Fish1",    5, 100, 100, 0);
        FishItem fishItem2 = new FishItem(FishItemType.fish,    "Fish2",   10,  45,  60, 1);
        FishItem fishItem3 = new FishItem(FishItemType.fish,    "Fish3",   20,  24,  40, 2);
        FishItem fishItem4 = new FishItem(FishItemType.fish,    "Fish4",   40,   5,  20, 3);
        FishItem jelly     = new FishItem(FishItemType.jelly,   "Jelly",   10,  50,  50, 4);
        FishItem recycle1  = new FishItem(FishItemType.recycle, "recycle1", 5,  10,  10, 5);
        FishItem recycle2  = new FishItem(FishItemType.recycle, "recycle2", 5,  10,  10, 6);
        FishItem recycle3  = new FishItem(FishItemType.recycle, "recycle3", 5,  10,  10, 7);

        List<FishItem> tempItemList = new List<FishItem>();
        tempItemList.Add(fishItem1);
        tempItemList.Add(fishItem2);
        tempItemList.Add(fishItem3);
        tempItemList.Add(fishItem4);
        tempItemList.Add(jelly);
        tempItemList.Add(recycle1);
        tempItemList.Add(recycle2);
        tempItemList.Add(recycle3);

        gameItemList = tempItemList;
    }
}
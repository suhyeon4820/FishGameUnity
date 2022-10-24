using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public FishItem getFishItem;

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
    int totalPoint;
    public int currentPoint;
    public int Point
    {
        get { return totalPoint; }
        private set { totalPoint = value; }
    }

    private void Awake()
    {
        SetGameItemList();
    }
    private void Start()
    {
        UpdateGgameState(GameState.ready);
    }

    // UIFishGame에서 호출
    public void SpawnRandomItem()
    {
        --Bait;

        // 아이템 랜덤 생성
        FishItem randomItem = null;
        List<FishItem> randomItemList = gameItemList;

        // 가중치 랜덤 뽑기 
        int weight = 0;
        int selectNum = 0;
        selectNum = Mathf.RoundToInt(Random.Range(0.01f, 1f) * 100);
        
        for (int i =0; i< randomItemList.Count;i++)
        {
            weight += randomItemList[i].rate;
            if(selectNum<=weight)
            {
                randomItem = randomItemList[i];
                break;
            }
        }
        // 포인트 계산
        currentPoint = randomItem.GetPoint();
        totalPoint += currentPoint;
        Point = totalPoint;

        getFishItem = randomItem;
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
        totalPoint = 0;
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
        FishItem fishItem1 = new FishItem(FishItemType.fish, "Fish1", 5, 100, 100);
        FishItem fishItem2 = new FishItem(FishItemType.fish, "Fish2", 10, 45, 60);
        FishItem fishItem3 = new FishItem(FishItemType.fish, "Fish3", 20, 24, 40);
        FishItem fishItem4 = new FishItem(FishItemType.fish, "Fish4", 40, 5, 20);
        FishItem jelly = new FishItem(FishItemType.jelly, "Jelly", 10, 50, 50);
        FishItem recycle1 = new FishItem(FishItemType.recycle, "recycle1", 5, 10, 10);
        FishItem recycle2 = new FishItem(FishItemType.recycle, "recycle2", 5, 10, 10);
        FishItem recycle3 = new FishItem(FishItemType.recycle, "recycle3", 5, 10, 10);

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
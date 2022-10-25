# FishGameUnity

- 게임 시스템 : MVC Design pattern

  - **Model(FishItem)** : 게임 아이템 오브젝트의 정보(종류, 획득 포인트, 획득 확률)를 다룸 

  - **Vie(UIFishGame)** : 낚시 성공 시 잡은 아이템 이름/포인트를 나타내고 총 미끼 수와, 포인트 정보를 나타냄 

  - **Controller(FishGameManager)** : FishItem에서 게임 아이템 정보를 가져와 게임 상태 전환 및 게임을 운영하고 게임 정보를  UIFishGame에 전달

    <img src = "https://github.com/suhyeon4820/FishGameUnity/blob/main/readmeImg/designpattern.png">



- 코드리뷰(10/20)
  - `MVC 패턴` 
  - 유니티 `가중치 랜덤`



- 코드리뷰(10/24)

  1. 프로퍼티 수정 

  - get , set 접근자 안에 아무것도 안쓰는 경우 프로퍼티 자체를 사용

  - 정보은닉을 위한 private 사용 :  해당 변수가 선언된 클래스 외부에서 접근이 불가능하게 함

    ```c#
    int totalPoint;
    public int currentPoint;
    public int Point
    {
        get { return totalPoint; }
        private set { totalPoint = value; }
    }
    
    // 아래로 수정
    public int Point { get; private set; }
    
    Point += currentPoint;
    ```

  2. 콜백 함수 : 액션함수를 이용한 콜백함수 적용하기

  - item 오브젝트 생성 후 안전하게 그 다음 실행

  - 이렇게 하면 생성된 item을 전달 받아 전달 가능

    ```
    fishGameManager.SpawnRandomItem();  
    ShowItemResult();
    
    // 아래로 수정
    fishGameManager.SpawnRandomItem((fish)=>
    {
        ShowItemResult(fish);
    });  
    ```

  
  3. MVC패턴 장점 
     - Model : data 전용 class로 분리 시 장점 -> 확장성, 수정, 재사용이 용이함
  
  

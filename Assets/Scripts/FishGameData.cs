using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGameData : MonoBehaviour
{
    // 미끼

    // 낚시 object
    // 종류 : 물고기(4), 젤리(1), 리사이클(3)
    // 확률 
    // 포인트 : 최소, 최대

    public enum FishObjectType { fish, jelly, recycle };

    class FishObject
    {
        FishObjectType type;
        int rate;
        int minPoint;
        int maxPoint;

        public FishObject(FishObjectType type, int rate, int minPoint, int maxPoint)
        {
            this.type = type;
            this.rate = rate;
            this.minPoint = minPoint;
            this.maxPoint = maxPoint;
        }
    }
}

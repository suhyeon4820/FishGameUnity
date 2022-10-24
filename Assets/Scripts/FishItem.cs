using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishItemType { fish, jelly, recycle };
public class FishItem 
{
    public FishItemType type;
    public string name;
    public int rate;
    public int minPoint;
    public int maxPoint;
   
    public FishItem(FishItemType type, string name, int rate, int minPoint, int maxPoint)
    {
        this.type = type;
        this.name = name;
        this.rate = rate;
        this.minPoint = minPoint;
        this.maxPoint = maxPoint;
    }

    public int GetPoint()
    {
        int randomPoint = Random.Range(minPoint, maxPoint) / 5;
        int resultPoint = randomPoint * 5;
        return resultPoint;
    }
}

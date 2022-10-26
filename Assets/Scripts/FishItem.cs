using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FishItemType { fish, jelly, recycle };
public class FishItem
{
    public FishItemType type;
    public string name;
    public int rate;
    public int minPoint;
    public int maxPoint;
    public int imageIndex;
    public int randomPoint;
   

    public FishItem(FishItemType type, string name, int rate, int minPoint, int maxPoint, int imageIndex)
    {
        this.type = type;
        this.name = name;
        this.rate = rate;
        this.minPoint = minPoint;
        this.maxPoint = maxPoint;
        this.imageIndex = imageIndex;
    }
    
    public void SetPoint()
    {
        int offSet = Random.Range(0, 4) * 5;
        int randomPoint = minPoint + offSet;
        if (randomPoint >= maxPoint)
            randomPoint = maxPoint;
        
        this.randomPoint = randomPoint;
    }
}

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
    public int offSet;
    public int imageIndex;

    public int randomPoint;
   

    public FishItem(FishItemType type, string name, int rate, int offSet, int imageIndex, bool random)
    {
        this.type = type;
        this.name = name;
        this.rate = rate;
        this.offSet = offSet;
        this.imageIndex = imageIndex;

        if (random)
            this.randomPoint = SetPoint();
        else
            this.randomPoint = offSet;
    }
    
    public int SetPoint()
    {
        int randomNum = Random.Range(1, 5);
        int resultPoint = (randomNum * 5) + offSet;
        return resultPoint;
    }
}

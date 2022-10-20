using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FishGameResult : MonoBehaviour
{
    [SerializeField] private TMP_Text fishNameText;
    [SerializeField] private TMP_Text fishPointText;

    string[] randomObjArr = new string[100];    // 랜덤 obj 배열
    string[] objName = new string[] { "fish1", "fish2", "fish3", "fish4", "jelly", "recycle1", "recycle2", "recycle3" }; // 8개 : 물고기4, 젤리1, 리사이클3
    Dictionary<string, List<int>> objPointDict = new Dictionary<string, List<int>>();

    int point = 0;

    int num = 0;
    private void Awake()
    {
        SetRandomObjArray();
        SetObjPointDict();
    }

    void SetRandomObjArray()
    {
        string[] tempRandomObjArr = new string[100];
        // 배열 생성
        for (int i = 0; i < 5; i++)
        {
            tempRandomObjArr[i] = objName[0];
        }
        for (int i = 5; i < 15; i++)
        {
            tempRandomObjArr[i] = objName[1];
        }
        for (int i = 15; i < 35; i++)
        {
            tempRandomObjArr[i] = objName[2];
        }
        for (int i = 35; i < 75; i++)
        {
            tempRandomObjArr[i] = objName[3];
        }
        for (int i = 75; i < 85; i++)
        {
            tempRandomObjArr[i] = objName[4];
        }
        for (int i = 85; i < 90; i++)
        {
            tempRandomObjArr[i] = objName[5];
        }
        for (int i = 90; i < 95; i++)
        {
            tempRandomObjArr[i] = objName[6];
        }
        for (int i = 95; i < 100; i++)
        {
            tempRandomObjArr[i] = objName[7];
        }
        randomObjArr = tempRandomObjArr;
        // 배열 섞어주기
        for(int i = 0; i<10000; i++)
        {
            // random index두 개의 값 swap
            int index1 = Random.Range(0, 100);
            int index2 = Random.Range(0, 100);

            string tempNum = randomObjArr[index1];
            randomObjArr[index1] = randomObjArr[index2];
            randomObjArr[index2] = tempNum;
        }
        // 출력
        for(int i = 0; i<randomObjArr.Length; i++)
        {
            Debug.Log(randomObjArr[i]);
        }
    }

    void SetObjPointDict()
    {
        List<int> fish1Arr = new List<int> { 100, 100 };
        List<int> fish2Arr = new List<int> { 45, 60 };
        List<int> fish3Arr = new List<int> { 25, 40 };
        List<int> fish4Arr = new List<int> { 5, 20 };
        List<int> jelly = new List<int> { 50, 50 };
        List<int> recycle = new List<int> { 10, 10 };
        
        objPointDict.Add(objName[0], fish1Arr);
        objPointDict.Add(objName[1], fish2Arr);
        objPointDict.Add(objName[2], fish3Arr);
        objPointDict.Add(objName[3], fish4Arr);
        objPointDict.Add(objName[4], jelly);
        objPointDict.Add(objName[5], recycle);
        objPointDict.Add(objName[6], recycle);
        objPointDict.Add(objName[7], recycle);
    }

    private void OnEnable()
    {
        num++;
        Debug.Log("----------------------" + num);

        // 랜덤으로 뽑기
        int randomNum = Random.Range(0, 100);
        // 이름
        string objName = randomObjArr[randomNum].ToString();
        Dictionary<string, List<int>> tempObjPointDict = objPointDict;
        // 포인트
        List<int> objPoint = tempObjPointDict[objName];
        int objPointNum = Random.Range(objPoint[0], objPoint[1]) / 5;
        point = objPointNum * 5;
        // 출력
        fishNameText.text = objName;
        fishPointText.text = point.ToString();
    }

    public int ReturnPoint()
    {
        return point;
    }
}

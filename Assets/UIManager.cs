using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System;
using LeTai.Asset.TranslucentImage;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Sprite[] sprs;
    [SerializeField] List<GameObject> ImgTrans;
    [SerializeField] GameObject perfab;
    [SerializeField] GameObject Panel;
    [SerializeField] int totalImageCount = 5;


    //public List<TranslucentImage> imgs = new List<TranslucentImage>();

    [Header("左右间距")]
    [SerializeField] float spaceX = 1f;
    [Header("前后间距")]
    [SerializeField] float spaceZ = 0.3f;


    int imgIndex = 0;
    void Start()
    {
        Debug.Log("start");

        for (int i = 0; i < totalImageCount; i++)
        {
            imgIndex++;
            GameObject obj = Instantiate(perfab, transform);
            TranslucentImage img = obj.GetComponentInChildren<TranslucentImage>();
            img.sprite = sprs[i];

            ImgTrans.Add(obj);
        }

        Reposition();
    }

    Image del;


    public void GetNewImg()
    {
        //RemoveOld
        Vector3 pos = ImgTrans[ImgTrans.Count - 1].transform.position;
        pos.x += spaceX;
        pos.z -= spaceZ;
        GameObject old = ImgTrans[ImgTrans.Count - 1];
        ImgTrans.Remove(old);
        old.transform.DOMove(pos, 1f).OnComplete(() => { Destroy(old); });
        old.GetComponentInParent<CanvasGroup>().DOFade(0f, 1f);

        //AddNew
        Vector3 newPos = ImgTrans[0].transform.position;
        newPos.x -= spaceX;
        newPos.z -= spaceZ;

        GameObject obj = Instantiate(perfab, newPos, Quaternion.identity);
        obj.transform.parent = transform;
        TranslucentImage img = obj.GetComponentInChildren<TranslucentImage>();
        img.sprite = sprs[imgIndex];
        imgIndex++;
        ImgTrans.Insert(0, obj);


        //Reposition
        Reposition();

    }

    void Reposition()
    {
        int offest = ImgTrans.Count / 2;
        int index = 0;
        foreach (GameObject _img in ImgTrans)
        {
            //Vector3 start = transform.position;
            Vector3 start = Vector3.zero;

            int posInLine = Mathf.Abs(index - offest);
            Debug.Log("posInLine" + posInLine);

            start.x -= spaceX * (index - offest);
            start.z += spaceZ * posInLine;

            _img.GetComponent<CanvasGroup>().DOFade(1f - 0.2f * posInLine, 1f);
            float scale = posInLine == 0 ? 1.3f : 1f;
             scale = posInLine == 1 ? 1.1f : 1f;
            _img.GetComponent<ImgEffect>().SetTargetValue(1f - 0.2f * posInLine);
            _img.transform.DOLocalMove(start, 1f);
            _img.transform.GetChild(0).transform.DOScale(Vector3.one * scale, 1f);

           index++;
        }
    }


    //public void GoToNext(int i)
    //{
    //    //生成
    //    TranslucentImage newImg = Instantiate(perfab, Panel.transform).GetComponentInChildren<TranslucentImage>();
    //    newImg.sprite = sprs[i];

    //    newImg.transform.parent.transform.DOMove(imgs[imgs.Count - 1].transform.parent.transform.position, 1f);
    //    newImg.transform.GetComponentInParent<CanvasGroup>().DOFade(
    //        imgs[imgs.Count - 1].transform.parent.transform.GetComponentInParent<CanvasGroup>().alpha,
    //            1f);
    //    //摧毁
    //    del = imgs[imgs.Count - 1];
    //    Vector3 start = Vector3.zero;
    //    start.x -= spaceX ;
    //    start.z -= spaceZ;
    //    imgs[imgs.Count - 1].transform.parent.transform.DOMove(
    //        imgs[imgs.Count - 1].transform.parent.position + start,
    //        1f);
    //    imgs[imgs.Count - 1].transform.GetComponentInParent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => Destroy(del));


    //    int offest = imgs.Count / 2;

    //    for (int index = 4; index > 1; index--)
    //    {
    //        imgs[index].transform.parent.transform.DOMove(imgs[index - 1].transform.parent.transform.position, 1f);

    //        imgs[index].transform.GetComponentInParent<CanvasGroup>().DOFade(
    //            imgs[index - 1].transform.parent.transform.GetComponentInParent<CanvasGroup>().alpha,
    //            1f);
    //    }
    //    imgs.RemoveAt(imgs.Count - 1);
    //    imgs.Sort();
    //    imgs.Add(newImg);   
    //}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //           GoToNext(1);
            GetNewImg();

        }
    }
}

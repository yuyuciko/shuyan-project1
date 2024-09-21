using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Sprite[] sprs;
    [SerializeField] GameObject perfab;
    [SerializeField] GameObject Panel;

    public List<Image> imgs = new List<Image>();

    [Header("左右间距")]
    [SerializeField] float spaceX = 100f;
    [Header("前后间距")]
    [SerializeField] float spaceZ = 30f;
    void Start()
    {

        Debug.Log("start");

        for (int i = 0; i < 5; i++)
        {
            Image img = Instantiate(perfab, Panel.transform).GetComponentInChildren<Image>();
            img.sprite = sprs[i];
            imgs.Add(img);
        }

        int offest = imgs.Count / 2;


        int index = 0;

        foreach (Image img in imgs)
        {
            //Vector3 start = transform.position;
            Vector3 start = Vector3.zero;
            start.x -= spaceX * (index - offest);
            start.z -= spaceZ * Mathf.Abs(index - offest);

            img.transform.GetComponentInParent<CanvasGroup>().DOFade(1f - 0.2f * Mathf.Abs(index - offest), 1f);
            img.transform.parent.transform.DOLocalMove(start, 1f);

            index++;
        }
    }
    Image del;
    public void GoToNext(int i)
    {
        //生成
        Image newImg = Instantiate(perfab, Panel.transform).GetComponentInChildren<Image>();
        newImg.sprite = sprs[i];

        newImg.transform.parent.transform.DOMove(imgs[imgs.Count - 1].transform.parent.transform.position, 1f);
        newImg.transform.GetComponentInParent<CanvasGroup>().DOFade(
            imgs[imgs.Count - 1].transform.parent.transform.GetComponentInParent<CanvasGroup>().alpha,
                1f);
        //摧毁
        del = imgs[imgs.Count - 1];
        Vector3 start = Vector3.zero;
        start.x -= spaceX ;
        start.z -= spaceZ;
        imgs[imgs.Count - 1].transform.parent.transform.DOMove(
            imgs[imgs.Count - 1].transform.parent.position + start,
            1f);
        imgs[imgs.Count - 1].transform.GetComponentInParent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => Destroy(del));


        int offest = imgs.Count / 2;

        for (int index = 4; index > 0; index--)
        {
            imgs[index].transform.parent.transform.DOMove(imgs[index - 1].transform.parent.transform.position, 1f);

            imgs[index].transform.GetComponentInParent<CanvasGroup>().DOFade(
                imgs[index - 1].transform.parent.transform.GetComponentInParent<CanvasGroup>().alpha,
                1f);
        }
        imgs.RemoveAt(imgs.Count - 1);
        imgs.Sort();
        imgs.Add(newImg);   
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GoToNext(1);
 }
    }
}

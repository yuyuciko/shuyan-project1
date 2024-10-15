using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeTai.Asset.TranslucentImage;


public class ImgEffect : MonoBehaviour
{
    public TranslucentImage translucentImage;
    public float targetBlend;
    public float targetLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        targetBlend = 1f;
        targetLocalScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        translucentImage.spriteBlending = Mathf.Lerp(translucentImage.spriteBlending, targetBlend, Time.deltaTime * 3f);
        //transform.GetChild(0).gameObject.transform.localScale =
        //    Vector3.Lerp(transform.GetChild(0).gameObject.transform.localScale,
        //    Vector3.one * targetLocalScale,
        //    Time.deltaTime * 3f);
    }

    public void SetTargetValue(float _targetBlend)
    {
        targetBlend = _targetBlend;
        //targetLocalScale = _targetLocalScale;
    }
}

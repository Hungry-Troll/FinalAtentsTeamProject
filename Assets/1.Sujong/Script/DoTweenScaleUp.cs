using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DoTweenScaleUp : MonoBehaviour
{
    Sequence mySequence;
    public Transform TR;
    void Start()
    {

    }

    void OnEnable()
    {
        mySequence.Restart();
    }

    public void A()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() => {TR.localScale = Vector3.zero; })
        .Append(TR.DOScale(1, 1).SetEase(Ease.OutBounce))
        .SetDelay(0.5f);
    }

    public void B()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() => { TR.DOScale(0, 1f).SetEase(Ease.OutCirc); })        
        .SetDelay(0.5f);
    }
}

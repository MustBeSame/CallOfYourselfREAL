using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardPreview : MonoBehaviour {

    public Vector3 TargetPosition;
    public float TargetScale;

    public GameObject card;

    public bool OverCollider { get; set; }

    void OnMouseEnter()
    {
        OverCollider = true;

        Debug.Log("Mouse esta em cima");

        PreviewThisObject();
    }

    void PreviewThisObject()
    {
  
            // 1) clone this card 
            // first disable the previous preview if there is one already
            //StopAllPreviews();
            // 2) save this HoverPreview as curent
            //currentlyViewing = this;
            //// 3) enable Preview game object
            //previewGameObject.SetActive(true);
            //// 4) disable if we have what to disable
            //if (TurnThisOffWhenPreviewing != null)
            //    TurnThisOffWhenPreviewing.SetActive(false);
            // 5) tween to target position
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = Vector3.one;

            card.transform.DOLocalMove(TargetPosition, 1f).SetEase(Ease.OutQuint);
            card.transform.DOScale(TargetScale, 1f).SetEase(Ease.OutQuint);
        }
    }


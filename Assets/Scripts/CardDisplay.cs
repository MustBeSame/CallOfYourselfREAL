using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public Card card;

    public Image art;

    //public Text nameText;
    //public Text descText;

	// Use this for initialization
	void Start () {
        //nameText.text = card.title;
        //descText.text = card.description;
        if (card.artWork != null)
        {
            art.sprite = card.artWork;
        }
      
	}

}

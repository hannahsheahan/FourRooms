﻿//Attach this script to an Image GameObject and set its Source Image to the Sprite you would like.
//Press the space key to change the Sprite. Remember to assign a second Sprite in this script's section of the Inspector.

using UnityEngine;
using UnityEngine.UI;

public class DisplayRewardCue : MonoBehaviour
{
    public Image rewardImage;
    //Set this in the Inspector
    public Sprite wineImage;
    public Sprite cheeseImage;
    public Sprite martiniImage;
    public Sprite bananaImage;
    public Sprite watermelonImage;
    private string cue;

    // ********************************************************************** //

    void Start()
    {
        //Fetch the Image from the GameObject
        rewardImage = GetComponent<Image>();
        rewardImage.enabled = false;
    }

    // ********************************************************************** //

    void Update()
    {
        if ( GameController.control.displayCue )
        {
            cue = GameController.control.rewardType;
            switch (cue)
            {
                case "wine":
                    rewardImage.sprite = wineImage;
                    break;
                case "cheese":
                    rewardImage.sprite = cheeseImage;
                    break;
                case "martini":
                    rewardImage.sprite = martiniImage;
                    break;
                case "banana":
                    rewardImage.sprite = bananaImage;
                    break;
                case "watermelon":
                    rewardImage.sprite = watermelonImage;
                    break;
            }
            rewardImage.enabled = true;
        }
        else
        {
            rewardImage.enabled = false;
        }
    }

    // ********************************************************************** //

}
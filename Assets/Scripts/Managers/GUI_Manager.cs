using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Manager : MonoBehaviour
{
    public Image HPBarObject;

    public Transform PlayerObject;

    Player gaia;

    // Start is called before the first frame update
    void Start()
    {
        gaia = PlayerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar();
    }

    void HPBar()
    {
        HPBarObject.fillAmount = (gaia.Healt / 100.0f);
    }
}

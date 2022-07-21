using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Bar")]

    public Image BArPower;
    public Image Bar2Power;
    public Image HEALTBAR;

    [Header("Icon")]
    public Image IconImageHealt;

    public float Timer;

    public bool IsTimer;

    public Player player;
    void Start()
    {

        Timer = 5.0f;

        var tempColor = IconImageHealt.color;
        tempColor.a = 0.1f;
        IconImageHealt.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Health < 100)
        {
            var tempColor = IconImageHealt.color;
            tempColor.a = 1.0f;
            IconImageHealt.color = tempColor;
        }

        if(IsTimer)
        {
            Timer -= Time.deltaTime;
            var tempColor = IconImageHealt.color;
            tempColor.a = 0.1f;
            IconImageHealt.color = tempColor;
        }

        if(Timer <= 0)
        {
            var tempColor = IconImageHealt.color;
            tempColor.a = 1.0f;
            IconImageHealt.color = tempColor;
            IsTimer = false;
            Timer = 5.0f;
        }

    }
    public void SetHealtBar1(float _fill)
    {
        BArPower.fillAmount = _fill;

    }
    public void SetHealtBar2(float _fill)
    {
        Bar2Power.fillAmount = _fill;

    }
    public void SetHealt(float _fill)
    {
        HEALTBAR.fillAmount = _fill;

    }

    public void RestoredHealt()
    {
        IsTimer = true;
    }

}

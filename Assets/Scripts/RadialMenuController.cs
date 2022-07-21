using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuController : MonoBehaviour
{
    GameObject theMenu;
    Vector2 moveInputs;
    public Text[] options;

    public Color normalColor, HighlightedColor, SelectedColorLeft, SelectColorRight;

    public int selectedOption;
    public int CountLeft, CountRight;
    public int Limit = 1;

    [Header("Player")]
    public Player Power_Player;

    public bool SelectedIcEeft, SelectedFORESTLeft, SelectedWATERLeft, SelectedSTONELeft, SelectedFIRELeft;
    public bool SelectedICERight, SelectedFORESTRight, SelectedWATERRight, SelectedSTONERight, SelectedFIRERight;
    public bool MenuActive = false;
    public bool Repeat = false;
    public float Timer = 0.1f;

    [Header("Powers")]
    public PowerSelectedCanvas powerSelectedCanvas;

    [Header("Images")]
    public Image GameObjecticonLeft;
    public Image BackgroundLeft;
    public Image GameObjecticonLeft2;
    public Image BackgroundLeft2;

    [Header("TextRadial")]
    public Text[] TextRadialMenuL;
    public Text[] TextRadialMenuR;

    [Header("HUD")]
    public HUD hud;

    [Header("Sound Selected")]
    public AudioSource Selected;

    private Player player;

    void Start()
    {
        Application.targetFrameRate = 60;
        theMenu = transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        options[3].color = SelectedColorLeft;
        options[3].text = "L";

        options[6].color = SelectColorRight;
        options[6].text = "R";
    }

    void Update()
    {

        if (Repeat)
        { Timer -= Time.deltaTime; }

        if (Timer <= 0)
        {
            Repeat = false;
            Timer = 0.1f;
        }

        if (CountLeft >= Limit)
        { CountLeft = 1; }

        if (CountRight >= Limit)
        { CountRight = 1; }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0;
            if (Power_Player.triggerAnimator == false)
            {
                theMenu.SetActive(true);
                MenuActive = true;
            }
        }
        else if(player.dead == false)
        {
            Time.timeScale = 1;
            theMenu.SetActive(false);
            MenuActive = false;
        }

        if (theMenu.activeInHierarchy)
        {
            moveInputs.x = Input.mousePosition.x - (Screen.width / 2f);
            moveInputs.y = Input.mousePosition.y - (Screen.height / 2f);
            moveInputs.Normalize();

            if (moveInputs != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInputs.y, -moveInputs.x) / Mathf.PI;

                //Debug.Log("angle  " + angle);
                angle *= 180;
                angle += 90f;

                if (angle < 0)
                { angle += 360; }

                for (int i = 0; i < options.Length; i++)
                {
                    if (angle > i * 72 && angle < (i + 1) * 72)
                    {
                        options[i].color = HighlightedColor;
                        selectedOption = i;
                    }
                    else
                    {
                        options[i].color = normalColor;
                        //SELECTED LEFT
                        if (SelectedSTONELeft == true)
                        { options[0].color = SelectedColorLeft; }

                        if (SelectedFIRELeft == true)
                        { options[3].color = SelectedColorLeft; }

                        if (SelectedIcEeft == true)
                        { options[4].color = SelectedColorLeft; }

                        if (SelectedFORESTLeft == true)
                        { options[2].color = SelectedColorLeft; }

                        if (SelectedWATERLeft == true)
                        { options[1].color = SelectedColorLeft; }

                        //SELECTED RIGHT
                        if (SelectedSTONERight == true)
                        { options[5].color = SelectColorRight; }

                        if (SelectedFIRERight == true)
                        { options[8].color = SelectColorRight; }

                        if (SelectedICERight == true)
                        { options[9].color = SelectColorRight; }

                        if (SelectedFORESTRight == true)
                        { options[7].color = SelectColorRight; }

                        if (SelectedWATERRight == true)
                        { options[6].color = SelectColorRight; }

                    }
                }
                //Debug.Log("seleccioando  " + selectedOption);
            }

            if (Input.GetMouseButtonDown(0))
            {
                switch (selectedOption)
                {
                    case 0:
                        Selected.Play();
                        SwitchStone();
                        break;
                    case 1:
                        Selected.Play();
                        SwitchFire();
                        break;
                    case 2:
                        Selected.Play();
                        SwitchIce();
                        break;
                    case 3:
                        Selected.Play();
                        SwitchForest();
                        break;
                    case 4:
                        Selected.Play();
                        SwitchWater();
                        break;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                switch (selectedOption)
                {
                    case 0:
                        Selected.Play();
                        SwitchStoneRight();
                        break;
                    case 1:
                        Selected.Play();
                        SwitchFireRight();
                        break;
                    case 2:
                        Selected.Play();
                        SwitchIceRight();
                        break;
                    case 3:
                        Selected.Play();
                        SwitchForestRight();
                        break;
                    case 4:
                        Selected.Play();
                        SwitchWaterRight();
                        break;
                }
            }
        }
    }
    public void SwitchStone()
    {
        if (SelectedSTONERight == true && CountRight == 1)
        {
            SelectedSTONELeft = false;
            options[0].color = normalColor;
            CountRight = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft.sprite = powerSelectedCanvas.icon[4];
            BackgroundLeft.color = powerSelectedCanvas.Colors[4];
            Power_Player.selectedPowers[0] = Power.EPower.stone;

            for (int i = 0; i < TextRadialMenuL.Length; i++)
            {
                TextRadialMenuL[i].text = " ";
            }

            TextRadialMenuL[0].text = "L";
        }

        CountLeft++;
        if (CountLeft == Limit)
        { options[0].color = normalColor; }


        if (SelectedFIRELeft == true)
        {
            if (Repeat == false)
            {
                SelectedSTONELeft = true;
                SelectedFIRELeft = false;

                options[1].color = normalColor;
            }
        }

        if (SelectedIcEeft == true)
        {
            if (Repeat == false)
            {
                SelectedSTONELeft = true;
                SelectedIcEeft = false;

                options[2].color = normalColor;
            }
        }

        if (SelectedFORESTLeft == true)
        {
            if (Repeat == false)
            {
                SelectedSTONELeft = true;
                SelectedFORESTLeft = false;

                options[3].color = normalColor;
            }
        }

        if (SelectedWATERLeft == true)
        {
            if (Repeat == false)
            {
                SelectedSTONELeft = true;
                SelectedWATERLeft = false;

                options[4].color = normalColor;
            }
        }

    }
    public void SwitchFire()
    {

        if (SelectedFIRERight == true && CountRight == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            SelectedFIRELeft = false;
            options[1].color = normalColor;
            CountRight = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft.sprite = powerSelectedCanvas.icon[0];
            BackgroundLeft.color = powerSelectedCanvas.Colors[0];
            Power_Player.selectedPowers[0] = Power.EPower.fire;

            for (int i = 0; i < TextRadialMenuL.Length; i++)
            {
                TextRadialMenuL[i].text = " ";
            }

            TextRadialMenuL[3].text = "L";
        }

        CountLeft++;

        if (CountLeft == Limit)
        { options[1].color = normalColor; }


        if (SelectedSTONELeft == true)
        {
            if (Repeat == false)
            {
                SelectedFIRELeft = true;
                SelectedSTONELeft = false;
                options[0].color = normalColor;

            }
        }
        if (SelectedIcEeft == true)
        {
            if (Repeat == false)
            {
                SelectedFIRELeft = true;
                SelectedIcEeft = false;
                options[2].color = normalColor;

            }
        }

        if (SelectedFORESTLeft == true)
        {
            if (Repeat == false)
            {
                SelectedFIRELeft = true;
                SelectedFORESTLeft = false;
                options[3].color = normalColor;

            }
        }

        if (SelectedWATERLeft == true)
        {
            Debug.Log("fefse");
            if (Repeat == false)
            {
                Debug.Log("fefse");
                SelectedFIRELeft = true;
                SelectedWATERLeft = false;
                options[4].color = normalColor;

            }
        }

    }

    public void SwitchIce()
    {
        if (SelectedICERight == true && CountRight == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            Debug.Log("fs");
            SelectedIcEeft = false;
            options[2].color = normalColor;
            CountRight = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft.sprite = powerSelectedCanvas.icon[2];
            BackgroundLeft.color = powerSelectedCanvas.Colors[2];
            Power_Player.selectedPowers[0] = Power.EPower.ice;

            for (int i = 0; i < TextRadialMenuL.Length; i++)
            {
                TextRadialMenuL[i].text = " ";
            }

            TextRadialMenuL[4].text = "L";
        }


        CountLeft++;
        if (CountLeft == Limit)
        { options[2].color = normalColor; }


        if (SelectedSTONELeft == true)
        {
            if (Repeat == false)
            {
                SelectedIcEeft = true;
                SelectedSTONELeft = false;
                options[0].color = normalColor;

            }
        }

        if (SelectedFIRELeft == true)
        {
            if (Repeat == false)
            {
                SelectedIcEeft = true;
                SelectedFIRELeft = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedFORESTLeft == true)
        {
            Debug.Log("g55");
            if (Repeat == false)
            {
                SelectedIcEeft = true;
                SelectedFORESTLeft = false;
                options[3].color = normalColor;

            }
        }

        if (SelectedWATERLeft == true)
        {
            if (Repeat == false)
            {
                SelectedIcEeft = true;
                SelectedWATERLeft = false;
                options[4].color = normalColor;

            }
        }

    }

    public void SwitchForest()
    {
        if (SelectedFORESTRight == true && CountRight == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            SelectedFORESTLeft = false;
            options[3].color = normalColor;
            CountRight = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft.sprite = powerSelectedCanvas.icon[1];
            BackgroundLeft.color = powerSelectedCanvas.Colors[1];
            Power_Player.selectedPowers[0] = Power.EPower.forest;

            for (int i = 0; i < TextRadialMenuL.Length; i++)
            {
                TextRadialMenuL[i].text = " ";
            }

            TextRadialMenuL[2].text = "L";
        }

        CountLeft++;

        if (CountLeft == Limit)
        { options[3].color = normalColor; }

        if (SelectedSTONELeft == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTLeft = true;
                SelectedSTONELeft = false;
                options[0].color = normalColor;

            }
        }

        if (SelectedFIRELeft == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTLeft = true;
                SelectedFIRELeft = false;
                options[1].color = normalColor;

            }
        }

        if (SelectedIcEeft == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTLeft = true;
                SelectedIcEeft = false;
                options[2].color = normalColor;

            }
        }

        if (SelectedWATERLeft == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTLeft = true;
                SelectedWATERLeft = false;
                options[4].color = normalColor;

            }
        }

    }

    public void SwitchWater()
    {
        if (SelectedWATERRight == true && CountRight == 1) //SI ESTÁ SELECCIONA EL PODER RIGHT, QUE NO PUEDAS SELECCIONARLO CON LEFT
        {
            SelectedWATERLeft = false;
            options[1].color = normalColor;
            CountLeft = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft.sprite = powerSelectedCanvas.icon[5];
            BackgroundLeft.color = powerSelectedCanvas.Colors[5];
            Power_Player.selectedPowers[0] = Power.EPower.water;

            for (int i = 0; i < TextRadialMenuL.Length; i++)
            {
                TextRadialMenuL[i].text = " ";
            }

            TextRadialMenuL[1].text = "L";
        }

        CountLeft++;

        if (CountLeft == Limit)
        { options[4].color = normalColor; }

        if (SelectedSTONELeft == true)
        {
            if (Repeat == false)
            {
                SelectedWATERLeft = true;
                SelectedSTONELeft = false;
                options[0].color = normalColor;
            }
        }

        if (SelectedFIRELeft == true)
        {
            if (Repeat == false)
            {
                SelectedWATERLeft = true;
                SelectedFIRELeft = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedIcEeft == true)
        {
            if (Repeat == false)
            {
                SelectedWATERLeft = true;
                SelectedIcEeft = false;
                options[2].color = normalColor;
            }
        }

        if (SelectedFORESTLeft == true)
        {
            if (Repeat == false)
            {
                SelectedWATERLeft = true;
                SelectedFORESTLeft = false;
                options[3].color = normalColor;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------
    public void SwitchStoneRight()
    {
        if (SelectedSTONELeft == true && CountLeft == 1)
        {
            SelectedSTONERight = false;
            options[0].color = normalColor;
            CountLeft = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft2.sprite = powerSelectedCanvas.icon[4];
            BackgroundLeft2.color = powerSelectedCanvas.Colors[4];
            Power_Player.selectedPowers[1] = Power.EPower.stone;

            for (int i = 0; i < TextRadialMenuR.Length; i++)
            {
                TextRadialMenuR[i].text = " ";
            }
            TextRadialMenuR[0].text = "R";
        }
        CountRight++;
        if (CountRight == Limit)
        { options[0].color = normalColor; }


        if (SelectedFIRERight == true)
        {
            if (Repeat == false)
            {
                SelectedSTONERight = true;
                SelectedFIRERight = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedICERight == true)
        {
            if (Repeat == false)
            {
                SelectedSTONERight = true;
                SelectedICERight = false;
                options[2].color = normalColor;
            }
        }

        if (SelectedFORESTRight == true) //SI ESTÁ SELECCIONADO EL FUEGO,Y SELECCIONAS EL STONE QUE SE CAMBIE AUTOMATICAMENTE
        {
            if (Repeat == false)
            {
                SelectedSTONERight = true;
                SelectedFORESTRight = false;
                options[3].color = normalColor;
            }
        }

        if (SelectedWATERRight == true) //SI ESTÁ SELECCIONADO EL FUEGO,Y SELECCIONAS EL STONE QUE SE CAMBIE AUTOMATICAMENTE
        {
            if (Repeat == false)
            {
                SelectedSTONERight = true;
                SelectedWATERRight = false;
                options[4].color = normalColor;
            }
        }

    }
    public void SwitchFireRight()
    {
        if (SelectedFIRELeft == true && CountLeft == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            Debug.Log("chance");
            SelectedFIRERight = false;
            options[1].color = normalColor;
            CountLeft = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft2.sprite = powerSelectedCanvas.icon[0];
            BackgroundLeft2.color = powerSelectedCanvas.Colors[0];
            Power_Player.selectedPowers[1] = Power.EPower.fire;

            for (int i = 0; i < TextRadialMenuR.Length; i++)
            {
                TextRadialMenuR[i].text = " ";
            }

            TextRadialMenuR[3].text = "R";
        }
        CountRight++;
        if (CountRight == Limit)
        { options[1].color = normalColor; }


        if (SelectedSTONERight == true)
        {
            if (Repeat == false)
            {
                SelectedFIRERight = true;
                SelectedSTONERight = false;
                options[0].color = normalColor;
            }
        }

        if (SelectedICERight == true)
        {
            if (Repeat == false)
            {
                SelectedFIRERight = true;
                SelectedICERight = false;
                options[2].color = normalColor;
            }
        }

        if (SelectedFORESTRight == true)
        {
            if (Repeat == false)
            {
                SelectedFIRERight = true;
                SelectedFORESTRight = false;
                options[3].color = normalColor;
            }
        }

        if (SelectedWATERRight == true)
        {
            if (Repeat == false)
            {
                SelectedFIRERight = true;
                SelectedWATERRight = false;
                options[4].color = normalColor;
            }
        }
    }

    public void SwitchIceRight()
    {
        if (SelectedIcEeft == true && CountLeft == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            SelectedICERight = false;
            options[2].color = normalColor;
            CountLeft = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft2.sprite = powerSelectedCanvas.icon[2];
            BackgroundLeft2.color = powerSelectedCanvas.Colors[2];
            Power_Player.selectedPowers[1] = Power.EPower.ice;

            for (int i = 0; i < TextRadialMenuR.Length; i++)
            {
                TextRadialMenuR[i].text = " ";
            }

            TextRadialMenuR[4].text = "R";
        }
        CountRight++;
        if (CountRight == Limit)
        { options[3].color = normalColor; }


        if (SelectedSTONERight == true)
        {
            if (Repeat == false)
            {
                SelectedICERight = true;
                SelectedSTONELeft = false;
                options[0].color = normalColor;
            }
        }

        if (SelectedFIRERight == true)
        {
            if (Repeat == false)
            {
                SelectedICERight = true;
                SelectedFIRERight = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedFORESTRight == true)
        {
            if (Repeat == false)
            {
                SelectedICERight = true;
                SelectedFORESTRight = false;
                options[3].color = normalColor;
            }
        }

        if (SelectedWATERRight == true)
        {
            if (Repeat == false)
            {
                SelectedICERight = true;
                SelectedWATERRight = false;
                options[4].color = normalColor;
            }
        }

    }

    public void SwitchForestRight()
    {
        if (SelectedFORESTLeft == true && CountLeft == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {
            SelectedFORESTRight = false;
            options[3].color = normalColor;
            CountLeft = 1;
            Repeat = true;
        }
        else
        { Repeat = false; }
        if (Repeat == false)
        {
            GameObjecticonLeft2.sprite = powerSelectedCanvas.icon[1];
            BackgroundLeft2.color = powerSelectedCanvas.Colors[1];
            Power_Player.selectedPowers[1] = Power.EPower.forest;

            for (int i = 0; i < TextRadialMenuR.Length; i++)
            {
                TextRadialMenuR[i].text = " ";
            }

            TextRadialMenuR[2].text = "R";
        }
        CountRight++;
        if (CountRight == Limit)
        { options[3].color = normalColor; }


        if (SelectedSTONERight == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTRight = true;
                SelectedSTONERight = false;
                options[0].color = normalColor;
            }
        }

        if (SelectedFIRERight == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTRight = true;
                SelectedFIRERight = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedICERight == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTRight = true;
                SelectedICERight = false;
                options[2].color = normalColor;
            }
        }

        if (SelectedWATERRight == true)
        {
            if (Repeat == false)
            {
                SelectedFORESTRight = true;
                SelectedWATERRight = false;
                options[4].color = normalColor;
            }
        }
    }

    public void SwitchWaterRight()
    {
        if (SelectedWATERLeft == true && CountLeft == 1) //SI ESTÁ SELECCIONA EL PODER LEFT, QUE NO PUEDAS SELECCIONARLO CON RIGHT
        {

            SelectedWATERRight = false;
            options[4].color = normalColor;
            CountLeft = 1;
            Repeat = true;
            TextRadialMenuR[1].text = "R";
        }
        else
        { Repeat = false; }

        if (Repeat == false)
        {
            GameObjecticonLeft2.sprite = powerSelectedCanvas.icon[5];
            BackgroundLeft2.color = powerSelectedCanvas.Colors[5];
            Power_Player.selectedPowers[1] = Power.EPower.water;

            for (int i = 0; i < TextRadialMenuR.Length; i++)
            {
                TextRadialMenuR[i].text = " ";
            }

            TextRadialMenuR[1].text = "R";
        }
        CountRight++;

        if (CountRight == Limit)
        { options[4].color = normalColor; }


        if (SelectedSTONERight == true)
        {
            if (Repeat == false)
            {
                SelectedWATERRight = true;
                SelectedSTONERight = false;
                options[4].color = normalColor;
            }
        }

        if (SelectedFIRERight == true)
        {
            if (Repeat == false)
            {
                SelectedWATERRight = true;
                SelectedFIRERight = false;
                options[1].color = normalColor;
            }
        }

        if (SelectedICERight == true)
        {
            if (Repeat == false)
            {
                SelectedWATERRight = true;
                SelectedICERight = false;
                options[2].color = normalColor;
            }
        }

        if (SelectedFORESTRight == true)
        {
            if (Repeat == false)
            {
                SelectedWATERRight = true;
                SelectedFORESTRight = false;
                options[3].color = normalColor;
            }
        }
    }
}
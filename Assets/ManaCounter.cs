using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaCounter : MonoBehaviour
{
    public TextMeshProUGUI manaText;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        manaText.text = (""+player.GetMana());
    }
}

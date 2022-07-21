using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEffects, IDamage
{
    Rigidbody2D _rigid;
    public Camera MainCamera;
    [SerializeField]
    float _jumpForce = 8.0f;
    int limiteSalto = 2;
    int saltos;
    [SerializeField]
    float _speed = 10.0f;
    const float MAX_SPEED = 10.0f;
    public bool hit = false;
    public float MaxRayCastDistance = 3;
    public Transform poderObject;
    bool facingLeft = false;
    int CAmeraZoomLimit = 12;
    public bool triggerAnimator;

    [Header("Amount of Mana Gathered")]
    [SerializeField]
    int manaCoin;

    [Header("HUD Object")]
    public HUD hUD;

    [Header("SelectedPowers")]
    public Power.EPower[] selectedPowers = { Power.EPower.fire, Power.EPower.water };
    bool[] isPowerClicked = { false, false };

    Power powerManager;
    public RadialMenuController radialMenuController;

    public float Health { get; set; }
    [Header("Healt")]
    public float maxHealt = 100;
    public float CdRestoredHealt = 5;
    public float RestoredHealt = 10f;
    public float DamageHealt = 10f;

    [Header("AnimationLetter")]
    public GameObject TextLeterShift;
    public GameObject TextLetterPower;

    [Header("Resistances")]
    public float physicalRes;
    public float natureRes;
    public float fireRes;
    public float waterRes;


    [Header("Status Effects")]
    public bool burned;
    public bool poisoned;
    public bool slowed;
    public bool frozen;
    public Material[] statusMaterials;
    public Material standardMaterial;


    //Variables para los efectos de estado
    protected float burnDuration;
    protected float burnPower;

    protected float poisonDuration;
    protected float poisonPower;

    protected float slowDuration;
    protected float slowPower;

    protected float frozenDuration;

    protected bool CRFire_Started;
    protected bool CRPoison_Started;
    protected bool CRDeath_Started;

    public EndGameTrigger endGameTrigger;

    [Header("Powers")]
    float[] powerTimer = { 0, 0, 0, 0, 0, 0 };
    float[] powerCooldown = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
    float[] powerMana = { 0, 0, 0, 0, 0, 0 };
    float[] powerMana2 = { 0, 0, 0, 0, 0, 0 };
    float[] powerManaCost = { 50, 50, 50, 0.5f, 50, 50 };
    int[] powerManaMAX = { 300, 300, 300, 300, 300, 300 }; 
    int[] powerManaMAX2 = { 300, 300, 300, 300, 300, 300 };

    //handle to PlayerAnimation
    SpriteRenderer _playerSprite;
    Animator _animator;
    public enum InputState
    {
        clicked,
        pressed,
        unclicked,
        inactive
    }
    InputState[] inputState = { InputState.inactive, InputState.inactive };


    public int Healt { get; set; }

    Vector3 MousePos;
    Vector3 playerToMouseDirection;
    public bool dead = false;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        manaCoin = 0;
        saltos = 0;
        powerManager = poderObject.GetComponent<Power>();
        Health = maxHealt;
        DamageHealt = 10;
        for (int i = 0; i < 6; i++)
        {
          powerMana[i] = powerManaMAX[i];
          powerMana2[i] = powerManaMAX2[i];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (saltos < limiteSalto)
            {
                _animator.SetTrigger("StartJump");
                _animator.SetBool("IsJumping", true);

                _rigid.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                saltos++;
            }
        }

        if (_rigid.velocity.y < -0.1f)
        { 
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsFalling", true);
        }

        if (!hUD.IsTimer) //Para que solo pueda pulsar E cuando el timer se acabe,para no spamear la E
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hUD.RestoredHealt();
                float SUM = Health + RestoredHealt;
                Health += RestoredHealt;
                Debug.Log("fdsfsd " + SUM);
                hUD.SetHealt(SUM);
            }
        }

        hUD.SetHealt(Health / maxHealt);


        if (triggerAnimator == true)
        {
            _animator.SetBool("walking", false);
        }

        if (triggerAnimator == true)
        {
            MainCamera.orthographicSize += Time.deltaTime;

            if (MainCamera.orthographicSize >= CAmeraZoomLimit)
            {
                MainCamera.orthographicSize = CAmeraZoomLimit;
            }
        }
        // FIRE LEFT-----------------------------

        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        playerToMouseDirection = MousePos - transform.position;

        if (radialMenuController.MenuActive == false && dead == false && endGameTrigger.win == false)
        {
            Movement();
        }


        if (radialMenuController.MenuActive)
        {
            _animator.SetBool("walking", false);
        }


        for (int i = 0; i < 6; i++)
        {
            powerTimer[i] -= Time.deltaTime;
            if (powerMana[i] >= powerManaMAX[i])
            {
                powerMana[i] = powerManaMAX[i];
            }
            else
                powerMana[i] += (Time.deltaTime * 10);

            if (powerMana[i] <= 0)
            {
                powerMana[i] = 0;
            }
        }

        if (!radialMenuController.MenuActive && !triggerAnimator)
        {
            if (Input.GetMouseButtonDown(0) && inputState[0] == InputState.inactive)
                inputState[0] = InputState.clicked;
            else if (Input.GetMouseButton(0) && inputState[0] == InputState.clicked)
                inputState[0] = InputState.pressed;
            else if (Input.GetMouseButtonUp(0) && (inputState[0] == InputState.pressed || inputState[0] == InputState.clicked))
                inputState[0] = InputState.unclicked;

            if (Input.GetMouseButtonDown(1) && inputState[1] == InputState.inactive)
                inputState[1] = InputState.clicked;
            else if (Input.GetMouseButton(1) && inputState[1] == InputState.clicked)
                inputState[1] = InputState.pressed;
            else if (Input.GetMouseButtonUp(1) && (inputState[1] == InputState.pressed || inputState[1] == InputState.clicked))
                inputState[1] = InputState.unclicked;
        }
        float angle = Vector2.SignedAngle(transform.right, playerToMouseDirection);


        // 0 = bosque
        // 1 = hielo
        // 2 = piedra
        // 3 = fuego 
        // 4 = metal
        // 5 = agyua

        hUD.SetHealtBar1(powerMana[(int)selectedPowers[0]] / powerManaMAX[(int)selectedPowers[0]]);

        hUD.SetHealtBar2(powerMana[(int)selectedPowers[1]] / powerManaMAX[(int)selectedPowers[1]]);

        PowerManagement(angle);

        StatusEffects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "suelo" || collision.collider.tag == "poderes")
        {
            saltos = 0;
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsFalling", false);
            _animator.ResetTrigger("StartJump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TriggerAnimator")
        {
            triggerAnimator = true;
            Debug.Log(MainCamera.orthographicSize);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AnimationLetter")
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
            Debug.Log("open");
                TextLeterShift.SetActive(false);
                TextLetterPower.SetActive(true);
            }
        }
    }

    void PowerManagement(float angle)
    {
        if (inputState[1] == InputState.inactive) //input left power individual 
        {
            if (inputState[0] == InputState.clicked)
            {
                if (powerTimer[(int)selectedPowers[0]] <= 0 && powerMana[(int)selectedPowers[0]] > powerManaCost[(int)selectedPowers[0]])
                {
                    if (selectedPowers[0] == Power.EPower.forest || selectedPowers[0] == Power.EPower.fire)
                    {
                        powerMana[(int)selectedPowers[0]] -= powerManaCost[(int)selectedPowers[0]];
                        powerManager.PowerInvocation(selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.metal)
                    {
                        powerManager.PowerInvocation(selectedPowers[0], facingLeft, angle);
                    }
                }
                else
                {
                    inputState[0] = InputState.inactive;
                }
            }
            if (inputState[0] == InputState.pressed)
            {
                if (selectedPowers[0] == Power.EPower.fire)
                {
                    if (powerMana[(int)selectedPowers[0]] > powerManaCost[(int)selectedPowers[0]])
                    {
                        powerMana[(int)selectedPowers[0]] -= powerManaCost[(int)selectedPowers[0]];
                        powerManager.PowerUpdate(selectedPowers[0], facingLeft, angle);
                    }
                    else
                        inputState[0] = InputState.unclicked;
                }
                if (selectedPowers[0] == Power.EPower.metal)
                {
                    if (powerMana[(int)selectedPowers[0]] > powerManaCost[(int)selectedPowers[0]])
                    {
                        powerMana[(int)selectedPowers[0]] -= powerManaCost[(int)selectedPowers[0]];
                        powerManager.PowerUpdate(selectedPowers[0], facingLeft, angle);
                    }
                    else
                        inputState[0] = InputState.unclicked;
                }
            }
            if (inputState[0] == InputState.unclicked)
            {
                if (selectedPowers[0] != Power.EPower.forest)
                {
                    powerManager.PowerDeinvocation(selectedPowers[0], facingLeft, angle);
                    powerMana[(int)selectedPowers[0]] -= powerManaCost[(int)selectedPowers[0]];
                    powerTimer[(int)selectedPowers[0]] = powerCooldown[(int)selectedPowers[0]];
                }
                if (selectedPowers[0] == Power.EPower.metal)
                {

                }
            }
        }

        if (inputState[0] == InputState.inactive) //input right power individual 
        {
            if (inputState[1] == InputState.clicked)
            {
                if (powerTimer[(int)selectedPowers[1]] <= 0 && powerMana[(int)selectedPowers[1]] > powerManaCost[(int)selectedPowers[1]])
                {
                    if (selectedPowers[1] == Power.EPower.forest || selectedPowers[1] == Power.EPower.fire)
                    {
                        powerMana[(int)selectedPowers[1]] -= powerManaCost[(int)selectedPowers[1]];
                        powerManager.PowerInvocation(selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.metal)
                    {
                        powerManager.PowerInvocation(selectedPowers[1], facingLeft, angle);
                    }
                }
                else
                {
                    inputState[1] = InputState.inactive;
                }
            }
            if (inputState[1] == InputState.pressed)
            {
                if (selectedPowers[1] == Power.EPower.fire)
                {
                    if (powerMana[(int)selectedPowers[1]] > powerManaCost[(int)selectedPowers[1]])
                    {
                        powerMana[(int)selectedPowers[1]] -= powerManaCost[(int)selectedPowers[1]];
                        powerManager.PowerUpdate(selectedPowers[0], facingLeft, angle);
                    }
                    else
                        inputState[1] = InputState.unclicked;
                }
                if (selectedPowers[1] == Power.EPower.metal)
                {
                    if (powerMana[(int)selectedPowers[1]] > powerManaCost[(int)selectedPowers[1]])
                    {
                        powerMana[(int)selectedPowers[1]] -= powerManaCost[(int)selectedPowers[1]];
                        powerManager.PowerUpdate(selectedPowers[1], facingLeft, angle);
                    }
                    else
                        inputState[1] = InputState.unclicked;
                }
            }
            if (inputState[1] == InputState.unclicked)
            {
                if (selectedPowers[1] != Power.EPower.forest)
                {
                    powerManager.PowerDeinvocation(selectedPowers[1], facingLeft, angle);
                    powerMana[(int)selectedPowers[1]] -= powerManaCost[(int)selectedPowers[1]];
                    powerTimer[(int)selectedPowers[1]] = powerCooldown[(int)selectedPowers[1]];
                }
                if (selectedPowers[1] == Power.EPower.metal)
                {

                }
            }
        }


        if (inputState[0] == InputState.pressed && inputState[1] == InputState.clicked)
        {
            if ((powerTimer[(int)selectedPowers[0]] <= 0 && powerMana[(int)selectedPowers[0]] > powerManaCost[(int)selectedPowers[0]]) &&
                    (powerTimer[(int)selectedPowers[1]] <= 0 && powerMana[(int)selectedPowers[1]] > powerManaCost[(int)selectedPowers[1]]))
            {
                powerMana[(int)selectedPowers[0]] -= powerManaCost[(int)selectedPowers[0]];
                powerMana[(int)selectedPowers[1]] -= powerManaCost[(int)selectedPowers[1]];

                if (selectedPowers[0] == Power.EPower.stone)
                {
                    if (selectedPowers[1] == Power.EPower.fire)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.forest)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                }
                if (selectedPowers[0] == Power.EPower.ice)
                {
                    if (selectedPowers[1] == Power.EPower.stone)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.water)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.fire)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                }
                if (selectedPowers[0] == Power.EPower.water)
                {
                    if (selectedPowers[1] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }

                }
                if (selectedPowers[0] == Power.EPower.metal)
                {
                    if (selectedPowers[1] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[1] == Power.EPower.stone)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                }
                if ((selectedPowers[0] != Power.EPower.fire || selectedPowers[1] != Power.EPower.fire) && (selectedPowers[0] != Power.EPower.ice || selectedPowers[1] != Power.EPower.ice))
                {
                    inputState[0] = InputState.inactive;
                    inputState[1] = InputState.inactive;
                }
            }
            if (selectedPowers[0] == Power.EPower.fire)
            {
                powerManager.PowerDeinvocation(selectedPowers[0], facingLeft, angle);
            }
        }
        if (inputState[0] == InputState.clicked && inputState[1] == InputState.pressed)
        {
            if ((powerTimer[(int)selectedPowers[0]] <= 0 && powerMana[(int)selectedPowers[0]] > powerManaCost[(int)selectedPowers[0]]) &&
                    (powerTimer[(int)selectedPowers[1]] <= 0 && powerMana[(int)selectedPowers[1]] > powerManaCost[(int)selectedPowers[1]]))
            {
                if (selectedPowers[1] == Power.EPower.stone)
                {
                    if (selectedPowers[0] == Power.EPower.fire)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.forest)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                }
                if (selectedPowers[1] == Power.EPower.ice)
                {
                    if (selectedPowers[0] == Power.EPower.stone)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.water)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.fire)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                    }
                }
                if (selectedPowers[1] == Power.EPower.water)
                {
                    if (selectedPowers[0] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                }
                if (selectedPowers[1] == Power.EPower.metal)
                {
                    if (selectedPowers[0] == Power.EPower.ice)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                    if (selectedPowers[0] == Power.EPower.stone)
                    {
                        powerManager.PowerMixInvocation(selectedPowers[1], selectedPowers[0], facingLeft, angle);
                    }
                }
                if ((selectedPowers[0] != Power.EPower.fire && selectedPowers[1] != Power.EPower.ice) && (selectedPowers[0] != Power.EPower.ice && selectedPowers[1] != Power.EPower.fire))
                {
                    inputState[0] = InputState.inactive;
                    inputState[1] = InputState.inactive;
                }
            }
            if (selectedPowers[1] == Power.EPower.fire)
            {
                powerManager.PowerDeinvocation(selectedPowers[1], facingLeft, angle);
            }
        }

        if (inputState[0] == InputState.pressed && inputState[1] == InputState.pressed)
        {
            if ((selectedPowers[0] == Power.EPower.fire && selectedPowers[1] == Power.EPower.ice) || (selectedPowers[0] == Power.EPower.ice && selectedPowers[1] == Power.EPower.fire))
            {
                if ((powerTimer[(int)selectedPowers[0]] <= 0 && powerMana[(int)selectedPowers[0]] > 0.5f) &&
                    (powerTimer[(int)selectedPowers[1]] <= 0 && powerMana[(int)selectedPowers[1]] > 0.5f))
                {
                    powerMana[(int)selectedPowers[0]] -= 0.5f;
                    powerMana[(int)selectedPowers[1]] -= 0.5f;
                    powerManager.PowerMixUpdate(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                }
                else
                {
                    inputState[0] = InputState.inactive;
                    inputState[1] = InputState.inactive;
                    powerManager.PowerMixDeinvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);
                }
            }
        }

        if ((inputState[0] == InputState.unclicked && inputState[1] == InputState.pressed) || (inputState[1] == InputState.unclicked && inputState[0] == InputState.pressed))
        {
            powerManager.PowerMixDeinvocation(selectedPowers[0], selectedPowers[1], facingLeft, angle);

            inputState[0] = InputState.inactive;
            inputState[1] = InputState.inactive;
        }

        if (inputState[0] == InputState.unclicked)
            inputState[0] = InputState.inactive;
        if (inputState[1] == InputState.unclicked)
            inputState[1] = InputState.inactive;
    }

    void Movement()
    {
        float Move = Input.GetAxisRaw("Horizontal");

        if (triggerAnimator == false)
        {
            if (Move > 0)
            {
                facingLeft = false;
                Flip(facingLeft);

                _animator.SetBool("walking", true);
            }
            else if (Move < 0)
            {
                facingLeft = true;
                Flip(facingLeft);
                _animator.SetBool("walking", true);
            }
            else
            {
                _animator.SetBool("walking", false);
            }


            _rigid.velocity = new Vector2(Move * _speed, _rigid.velocity.y);
        }
    }

    void Flip(bool FaceLeft)
    {
        _playerSprite.flipX = FaceLeft;

    }

    //**********EFECTOS DE ESTADO Y DAMAGE


    //Funcion para poder recibir una cantidad de daño de un tipo determinado 
    public virtual void TakeDamage(DamageInfo damageinfo)
    {
        float healthLost;
        switch (damageinfo.type)
        {
            case (DamageInfo.DamageType.physical):
                if(physicalRes != 0)
                    healthLost = damageinfo.amount - (damageinfo.amount * (physicalRes / 100.0f));
                else
                    healthLost = damageinfo.amount;
                Health -= healthLost;
                if (physicalRes > 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                else if (physicalRes == 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                else //if physicalRes<0
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                break;

            case (DamageInfo.DamageType.fire):
                if (fireRes != 0)
                    healthLost = damageinfo.amount - (damageinfo.amount * (fireRes / 100.0f));
                else
                    healthLost = damageinfo.amount;
                Health -= healthLost;
                if (fireRes > 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                else if (fireRes == 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                else //if fireRes<0
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                break;

            case (DamageInfo.DamageType.nature):
                if (natureRes != 0)
                    healthLost = damageinfo.amount - (damageinfo.amount * (natureRes / 100.0f));
                else
                    healthLost = damageinfo.amount;
                Health -= healthLost;
                if (natureRes > 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                else if (natureRes == 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                else //if natureRes<0
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                break;

            case (DamageInfo.DamageType.water):
                if (waterRes != 0)
                    healthLost = damageinfo.amount - (damageinfo.amount * (waterRes / 100.0f));
                else
                    healthLost = damageinfo.amount;
                Health -= healthLost;
                if (waterRes > 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.resisted);
                else if (waterRes == 0)
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.normal);
                else //if waterRes<0
                    DamagePopup.Create(transform.position, (int)healthLost, damageinfo.type, DamageInfo.DamageEffectiveness.vulnerable);
                break;
        }
        if (Health <= 0)
        {
            Time.timeScale = 0;
            endGameTrigger.ActivateCanvas(false);
            dead = true;
        }
    }

    //Funcion para poder recibir un efecto de estado
    public void GetStatusEffect(EffectInfo effectinfo)
    {
        switch (effectinfo.type)
        {
            case (EffectInfo.EffectType.burn):
                if (!poisoned)
                {
                    burned = true;
                    burnDuration = effectinfo.duration;
                    burnPower = effectinfo.power;
                }
                break;
            case (EffectInfo.EffectType.freeze):
                frozen = true;
                frozenDuration = effectinfo.duration;
                break;
            case (EffectInfo.EffectType.poison):
                if (!burned)
                {
                    poisoned = true;
                    poisonDuration = effectinfo.duration;
                    poisonPower = effectinfo.power;
                }
                break;
            case (EffectInfo.EffectType.slow):
                if (!frozen)
                {
                    slowed = true;
                    slowDuration = effectinfo.duration;
                    slowPower = effectinfo.power;
                }
                break;
        }
    }

    //Funcion que, una vez recibido un efecto de estado, produce sus efectos en el Player
    public void StatusEffects()
    {
        if (burned)
        {
            _playerSprite.material = statusMaterials[(int)EffectInfo.EffectType.burn];
            DamageInfo burnDamage = new DamageInfo(burnPower, DamageInfo.DamageType.fire);
            if (!CRFire_Started)
                StartCoroutine(BurnDamage(burnDamage));
            burnDuration -= Time.deltaTime;
            if (burnDuration <= 0)
            {
                burned = false;
                _playerSprite.material = standardMaterial;
            }

        }
        if (poisoned)
        {
            _playerSprite.material = statusMaterials[(int)EffectInfo.EffectType.poison];
            DamageInfo poisonDamage = new DamageInfo(burnPower, DamageInfo.DamageType.nature);
            if (!CRPoison_Started)
                StartCoroutine(PoisonDamage(poisonDamage));
            poisonDuration -= Time.deltaTime;
            if (poisonDuration <= 0)
            {
                poisoned = false;
                _playerSprite.material = standardMaterial;
            }
        }
        if (slowed)
        {
            _playerSprite.material = statusMaterials[(int)EffectInfo.EffectType.slow];
            _speed = MAX_SPEED * (slowPower / 100);
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0)
            {
                slowed = false;
                _playerSprite.material = standardMaterial;
                _speed = MAX_SPEED;
            }
        }
        if (frozen)
        {
            _playerSprite.material = statusMaterials[(int)EffectInfo.EffectType.freeze];
            _speed = 0;
            frozenDuration -= Time.deltaTime;
            if (frozenDuration <= 0)
            {
                frozen = false;
                _playerSprite.material = standardMaterial;
                _speed = MAX_SPEED;
            }
        }
    }

    //Corutinas para el daño en el tiempo de fuego y veneno
    IEnumerator BurnDamage(DamageInfo burnDamage)
    {
        CRFire_Started = true;
        yield return new WaitForSeconds(2.0f);
        TakeDamage(burnDamage);
        CRFire_Started = false;
    }

    IEnumerator PoisonDamage(DamageInfo poisonDamage)
    {
        CRPoison_Started = true;
        yield return new WaitForSeconds(2.0f);
        TakeDamage(poisonDamage);
        CRFire_Started = false;
    }


    //Mana Coin
    public void PickUpMana(int manaPickedUp)
    {
        manaCoin += manaPickedUp;
    }

    public int GetMana()
    {
        return manaCoin;
    }

}
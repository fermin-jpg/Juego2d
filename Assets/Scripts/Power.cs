using UnityEngine;

public class Power : MonoBehaviour
{
    public Transform playerTransform;

    public GameObject iceBlockPrefab;
    public GameObject waterPrefab;
    public GameObject stonePrefab;
    public GameObject stoneIcePrefab;
    public GameObject stoneFirePrefab;
    public GameObject waterIcePrefab;
    public GameObject charchoPrefab;

    public AudioSource AudioFire;
    public AudioSource AudioIce;
    public AudioSource AudioStone;
    public AudioSource Audiowater;
    public AudioSource AudioForest;

    Animator[] powerAnimators;

    public enum EPower
    {
        forest = 0,
        ice = 1,
        stone = 2,
        fire = 3,
        metal = 4,
        water = 5,
    }

    void Start()
    {
        //animators
        powerAnimators = new Animator[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            powerAnimators[i] = transform.GetChild(i).GetComponent<Animator>();
    }

    public void PowerInvocation(EPower selectedPower, bool _facingLeft, float _angle)
    {
        switch (selectedPower)
        {
            case EPower.forest:
                AudioForest.Play();
                transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                transform.GetChild(0).position = playerTransform.position;
                powerAnimators[0].SetTrigger("activated");
                break;

            case EPower.fire:
                AudioFire.Play();
                transform.GetChild(1).rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                transform.GetChild(1).position = playerTransform.position;
                powerAnimators[1].SetBool("activated", true);
                break;

            case EPower.metal:
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(2).position = playerTransform.position;
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        transform.Find("forest").position = playerTransform.position;
        transform.Find("fire").position = playerTransform.position;
        transform.Find("forestWithFire").position = playerTransform.position;
    }

    public void PowerUpdate(EPower selectedPower, bool _facingLeft, float _angle)
    {
        switch (selectedPower)
        {
            case EPower.forest:
                transform.GetChild(0).position = playerTransform.position;
                break;

            case EPower.fire:
                
                transform.GetChild(1).position = playerTransform.position;
                transform.Find("fire").transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                break;

            case EPower.metal:
                transform.GetChild(2).position = playerTransform.position;
                break;

            default:
                break;
        }
    }

    public void PowerDeinvocation(EPower selectedPower, bool _facingLeft, float _angle)
    {
        switch (selectedPower)
        {
            case EPower.ice:
              
                CreateIceBlock(_facingLeft);
                break;

            case EPower.forest:
                break;

            case EPower.fire:
                AudioFire.Stop();
                powerAnimators[1].SetBool("activated", false);
                break;

            case EPower.metal:
                transform.GetChild(2).gameObject.SetActive(false);
                break;

            case EPower.stone:
              
                CreateStone(_angle);
                break;

            case EPower.water:
               
                CreateWaterWave(_facingLeft);
                break;

            default:
                break;
        }
    }
    public void PowerMixInvocation(EPower selectedPower1, EPower selectedPower2, bool _facingLeft, float _angle)
    {
        if (selectedPower1 == EPower.stone || selectedPower2 == EPower.stone)
        {
            if (selectedPower1 == EPower.ice || selectedPower2 == EPower.ice)
            {
                GameObject stone = Instantiate(stoneIcePrefab, playerTransform.position, Quaternion.identity, transform.parent);
                stone.GetComponent<StoneIce>().InitializePush(_angle);
            }
            if (selectedPower1 == EPower.fire || selectedPower2 == EPower.fire)
            {
                GameObject stone = Instantiate(stoneFirePrefab, playerTransform.position, Quaternion.identity, transform.parent);
                stone.GetComponent<StoneFire>().InitializePush(_angle);
            }
            if (selectedPower1 == EPower.forest || selectedPower2 == EPower.forest)
            {
                transform.GetChild(4).rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                transform.GetChild(4).position = playerTransform.position;
                powerAnimators[4].SetTrigger("activated");
            }
        }
        if (selectedPower1 == EPower.ice || selectedPower2 == EPower.ice)
        {
            if (selectedPower1 == EPower.water || selectedPower2 == EPower.water)
            {
                GameObject water = Instantiate(waterIcePrefab, playerTransform.position, Quaternion.identity, transform.parent);
                water.GetComponent<WaterIce>().InitializePush(_facingLeft);
            }
            if (selectedPower1 == EPower.fire || selectedPower2 == EPower.fire)
            {
                transform.GetChild(3).position = playerTransform.position;
                transform.GetChild(3).rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
                powerAnimators[3].SetBool("activated", true);
            }
        }
    }

    public void PowerMixUpdate(EPower selectedPower1, EPower selectedPower2, bool _facingLeft, float _angle)
    {
        if (selectedPower1 == EPower.ice || selectedPower2 == EPower.ice)
        {
            if (selectedPower1 == EPower.fire || selectedPower2 == EPower.fire)
            {
                transform.GetChild(3).position = playerTransform.position;
                transform.GetChild(3).rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
            }
        }
    }
    public void PowerMixDeinvocation(EPower selectedPower1, EPower selectedPower2, bool _facingLeft, float _angle)
    {
        if (selectedPower1 == EPower.ice || selectedPower2 == EPower.ice)
        {
            if (selectedPower1 == EPower.fire || selectedPower2 == EPower.fire)
            {
                powerAnimators[3].SetBool("activated", false);
            }
        }
    }

    void CreateIceBlock(bool _facingLeft)
    {
        AudioIce.Play();
        Vector3 _positition;
        float distance = 2.5f;
        if (_facingLeft)
            _positition = playerTransform.position + new Vector3(-distance, 0, 0);
        else
            _positition = playerTransform.position + new Vector3(distance, 0, 0);

        GameObject ice = Instantiate(iceBlockPrefab, _positition, Quaternion.identity, transform.parent);
    }

    void CreateWaterWave(bool _facingLeft)
    {
        Audiowater.Play();
        GameObject water = Instantiate(waterPrefab, playerTransform.position, Quaternion.identity, transform.parent);
        water.GetComponent<Water>().InitializePush(_facingLeft);

        Vector3 _positition;
        float distance = 2.5f;
        if (_facingLeft)
            _positition = playerTransform.position + new Vector3(-distance, 0, 0);
        else
            _positition = playerTransform.position + new Vector3(distance, 0, 0);

        GameObject charcho = Instantiate(charchoPrefab, _positition - new Vector3(0, 1.64f, 0), Quaternion.identity, transform.parent);
        charcho.GetComponent<SpriteRenderer>().flipX = _facingLeft;
        //charcho.GetComponent<Charco>().InitializePush(_facingLeft);
    }

    void CreateStone(float _angle)
    {
        AudioStone.Play();
        GameObject stone = Instantiate(stonePrefab, playerTransform.position, Quaternion.identity, transform.parent);
        stone.GetComponent<Stone>().InitializePush(_angle);
    }
}
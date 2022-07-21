using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public Color physicalColor;
    public Color natureColor;
    public Color fireColor;
    public Color waterColor;

    public int normalTextSize = 16;
    public int vulnerableTextSize = 22;
    public int resistedTextSize = 12;

    public static DamagePopup Create(Vector3 position, int damageAmount, DamageInfo.DamageType damagetype, DamageInfo.DamageEffectiveness effectiveness)
    {
        Transform damagePopUpTransform = Instantiate(GameAssets.Instance.PFDamagePopUp, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopUpTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, damagetype, effectiveness);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private const float DISAPPEAR_TIMER_MAX = 1.0f;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, DamageInfo.DamageType damagetype, DamageInfo.DamageEffectiveness effectiveness)
    {
        textMesh.SetText(damageAmount.ToString());
        switch(damagetype)
        {
            case (DamageInfo.DamageType.physical):
                textColor = physicalColor;
                break;
            case (DamageInfo.DamageType.fire):
                textColor  = fireColor;
                break;
            case (DamageInfo.DamageType.nature):
                textColor = natureColor;
                break;
            case (DamageInfo.DamageType.water):
                textColor = waterColor;
                break;
        }
        textMesh.color = textColor;
        switch(effectiveness)
        {
            case (DamageInfo.DamageEffectiveness.resisted):
                textMesh.fontSize = resistedTextSize;
                break;
            case (DamageInfo.DamageEffectiveness.normal):
                textMesh.fontSize = normalTextSize;
                break;
            case (DamageInfo.DamageEffectiveness.vulnerable):
                textMesh.fontSize = vulnerableTextSize;
                break;
        }
        disappearTimer = 1.0f;
    }

    private void Update()
    {
        float moveYSpeed = 5.0f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1.0f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1.0f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if(disappearTimer <= 0)
        {
            float disappearSpeed = 3.0f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

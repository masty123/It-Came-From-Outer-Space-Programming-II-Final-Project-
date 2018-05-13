using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * StatusIndicator show player's status in UI graphic.
 * @author Theeruth Borisuth
 */
public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;


    void Awake()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR : No health bar object reference");
        }

        if (healthText == null)
        {
            Debug.LogError("STATUS INDICATOR : No health text object reference");
        }
    }

    /*
     * Change size of healthBar.
     */
    public void SetHealth(int _cur, int _max)
    {
            float _value = (float)_cur / _max;
            if (float.IsNaN(_value)) _value = 0;
            if (_value > 1f) _value = 1f;
            healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
            healthText.text = _cur + "/" + _max + " HP";              
    }

   





}

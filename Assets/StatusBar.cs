using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform HealthBar;

    [SerializeField]
    private Text HealthStatus;



    private void Start() {
        if (HealthBar ==  null) {
            Debug.LogError("No health bar references");
        }
        if (HealthStatus ==  null) {
            Debug.LogError("No health text references");
        }
    }

    public void SetHealth(int _current, int _max) {
        float _value = (float) _current  / _max;
        HealthBar.localScale = new Vector3(_value, HealthBar.localScale.y, HealthBar.localScale.z);
        HealthStatus.text = _current + "/" + _max + " HP";
    }




}

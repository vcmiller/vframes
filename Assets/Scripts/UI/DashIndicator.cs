using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIndicator : MonoBehaviour {
    public Image fill { get; private set; }

	// Use this for initialization
	void Start () {
        fill = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        fill.fillAmount = Ticker.inst.timeSinceTickRatio;
	}
}

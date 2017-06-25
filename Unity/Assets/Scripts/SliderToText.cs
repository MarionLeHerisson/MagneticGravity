using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour {

    [SerializeField]
    private Text textVal;

    [SerializeField]
    private Slider slider;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MajTexte() {

        var val = slider.value;

        if ((int)val == val) textVal.text = val.ToString();

        else textVal.text = val.ToString("F2");

    }
}

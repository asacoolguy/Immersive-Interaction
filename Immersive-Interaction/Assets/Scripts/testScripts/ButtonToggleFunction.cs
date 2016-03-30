using UnityEngine;
using System.Collections;
using LMWidgets;
using Leap;

public class ButtonToggleFunction : DataBinderToggle {
	
	public Color OnColor;
	public Color OffColor;
	private bool toggle;
	
	override public bool GetCurrentData(){
		return toggle;
	}
	
	override protected void setDataModel(bool value){
		toggle = value;
	}
	
	override protected void Start()
	{
		toggle = false;
		gameObject.GetComponent<Renderer>().material.color = OffColor;
	}
	
	protected void Update()
	{
		if (toggle){
			gameObject.GetComponent<Renderer>().material.color = OnColor;
		}
		else{
			gameObject.GetComponent<Renderer>().material.color = OffColor;
		}
	}
}

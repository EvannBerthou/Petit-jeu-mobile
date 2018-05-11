using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Texts {
	public string production;
	public string consumption;

    #region names
    //CITY
    public string inhabitant;
    public string hut;
    public string house;
    public string school;
    public string market;
    public string grocery;
    public string supermarket;
    public string startup;
    public string building;
    public string company;

    //WATER
    public string rainRecuperator;
    public string smallPump;
    public string largePump;
    public string waterTower;
    public string wastewaterTreatment;

    //Energy
    public string coalFactory;
    public string windTurbine;
    public string solarPanel;
    public string barrage;
    public string nuclearPlant;

    #endregion

    #region descriptions
    //CITY
    public string Dinhabitant;
    public string Dhut;
    public string Dhouse;
    public string Dschool;
    public string Dmarket;
    public string Dgrocery;
    public string Dsupermarket;
    public string Dstartup;
    public string Dbuilding;
    public string Dcompany;

    //WATER
    public string DrainRecuperator;
    public string DsmallPump;
    public string DlargePump;
    public string DWaterTower;
    public string DwastewaterTreatment;

    //Energy
    public string DcoalFactory;
    public string DwindTurbine;
    public string DsolarPanel;
    public string Dbarrage;
    public string DnuclearPlant;
    #endregion

    #region Items
    public string Unlock;
    #endregion

    #region Categories
    public string City;
    public string Water;
	public string Energy;
    public string Bonus;
	#endregion

	#region Options

	public string Music;
	public string Sounds;
	public string version;
	#endregion

	#region ReportBug
	public string Title;
	public string Description;
	#endregion

	#region QuitMenu
	public string Sure;
	public string Confirm;
	public string Cancel;
	#endregion
}
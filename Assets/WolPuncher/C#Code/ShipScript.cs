using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class ShipScript : MonoBehaviour 
{
	public int health;
	public Vector2 pos;
	public int damage;
	public string name;

	public TextAsset mXMLDoc;
	XmlDocument xmlDoc = new XmlDocument();

	// Use this for initialization
	void Start () 
	{
		
		xmlDoc.Load("./Assets/WolPuncher/XML/myXML.xml");
		xmlDoc.Load(new StringReader(mXMLDoc.text));

		XmlNode root = xmlDoc.FirstChild;

		XmlNode shipOne = root.FirstChild;

		int hp = int.Parse(shipOne.Attributes["HP"].Value); //gets HP from the ship in the xml file
		float pos = int.Parse(shipOne.Attributes["Pos"].Value);
		int damage = int.Parse(shipOne.Attributes["Damage"].Value);
		string name = (shipOne.Attributes["Name"].Value);
//		name = new string.Contains(shipOne.Attributes["Name"].Value);
	}

	// Update is called once per frame
	void Update () 
	{

		xmlDoc.Load("./Assets/WolPuncher/XML/myXML.xml");
		xmlDoc.Load(new StringReader(mXMLDoc.text));

		XmlNode root = xmlDoc.FirstChild;

		XmlNode shipOne = root.FirstChild;

//		shipOne.Attributes["HP"].Value = health;
//		shipOne.Attributes["Damage"].Value = damage;
//		shipOne.Attributes["X"].Value = pos.x;
//		shipOne.Attributes["Y"].Value = pos.y;
//		shipOne.Attributes["Name"].Value = name;

		if (Input.GetAxis("Jump") > 0)
		{
			xmlDoc.Save("./Assets/WolPuncher/XML/myXML.xml");
		}
		
	}
}

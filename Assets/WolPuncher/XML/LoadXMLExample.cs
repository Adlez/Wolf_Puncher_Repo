using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;


public class LoadXMLExample : MonoBehaviour
{
	public TextAsset mXMLDoc;
	XmlDocument xmlDoc = new XmlDocument();
	public int HP;
	public
	

	// Use this for initialization
	void Start () 
	{
		xmlDoc.Load("./Assets/WolPuncher/XML/myXML.xml");
		xmlDoc.Load(new StringReader(mXMLDoc.text));

		XmlNode root = xmlDoc.FirstChild;

		XmlNode shipOne = root.FirstChild;

		XmlAttributeCollection temp = shipOne.Attributes;
		XmlAttribute temp2 = temp[0];

		int hp = int.Parse(shipOne.Attributes["HP"].Value); //gets HP from the ship in the xml file
		float pos = int.Parse(shipOne.Attributes["Pos"].Value);
		int damage = int.Parse(shipOne.Attributes["Damage"].Value);
		string name = (shipOne.Attributes["Name"].Value);

		Debug.Log(hp);
		foreach(XmlNode node in shipOne.ChildNodes)
		{
			Debug.Log(node.Name);
		}

		string nodePath = "//ShipTypes/Ship1";
		XmlNodeList ship1Nodes = xmlDoc.SelectNodes(nodePath);

		//ship1Nodes[0].anything;

		//SAVING------------------------------------------------------------------------

		shipOne.Attributes["HP"].Value = "20";

		xmlDoc.Save("./Assets/WolPuncher/XML/myXML.xml");
	//	root.InnerText
		//ADD NODE-----------------------------------------------------------------------
		shipOne.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, "Cargo", ""));

	}
	
	// Update is called once per frame
	void Update () 
	{
		xmlDoc.Load("./Assets/WolPuncher/XML/myXML.xml");
		xmlDoc.Load(new StringReader(mXMLDoc.text));

		XmlNode root = xmlDoc.FirstChild;

		XmlNode shipOne = root.FirstChild;

		shipOne.Attributes["HP"].Value = "50000000000";
		shipOne.Attributes["Damage"].Value = "10000";
		shipOne.Attributes["X"].Value = "10.5f";
		shipOne.Attributes["Y"].Value = "3.5f";
		shipOne.Attributes["Name"].Value = "RobIsAwesome";



		//int.Parse(shipOne.Attributes["HP"].Value) = 10;
	}
}

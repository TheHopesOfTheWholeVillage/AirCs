using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class TypeData:BaseXmlData {
	public	string	id;
	public	string	name;

	public override void init(XmlElement data)
	{
		id = data.GetAttribute ("id");
		this.name = data.GetAttribute ("name");
	}
}

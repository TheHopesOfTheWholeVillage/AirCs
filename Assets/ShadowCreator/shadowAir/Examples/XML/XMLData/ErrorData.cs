using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ErrorData:BaseXmlData {
	public	string	id;
	public	string	check;
	public	string	text;

	public override void init(XmlElement data)
	{
		id = data.GetAttribute ("id");
		this.text = data.GetAttribute ("text");
		this.check = data.GetAttribute ("check");
	}
}

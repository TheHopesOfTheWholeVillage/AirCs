using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class FurnitureData:BaseXmlData {
	public	string	id;
	public	string	name;
	public	string	type;
	public	string	series;
	public	string	number;
	public	string	f_color;
	public	string	f_fabric;
	public	string	f_material;
	public	bool		needDwonload;
	public	string	pic{get{return number;}}
	public	string	model{get{return number;}}

	public override void init(XmlElement data)
	{
		this.id = data.GetAttribute ("id");
		this.name = data.GetAttribute ("name");
		this.type = data.GetAttribute ("type");
		this.series = data.GetAttribute ("series");

		this.number = data.GetAttribute ("number");
		this.f_color = data.GetAttribute ("color");
		this.f_fabric = data.GetAttribute ("fabric");
		this.f_material = data.GetAttribute ("material");
		this.needDwonload = int.Parse(id) > 21 ?true:false;
	}
}

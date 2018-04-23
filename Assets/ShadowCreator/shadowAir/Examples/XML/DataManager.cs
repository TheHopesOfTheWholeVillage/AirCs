using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class DataManager : MonoBehaviour {

	public static Dictionary<string, Dictionary<string,BaseXmlData>> xmlDatas;
	public static bool isLoaded = false;

	private List<string> xmlNames;

	void Start () {
		xmlNames = new List<string> ();
		xmlNames.Add("type.xml");
		xmlNames.Add("series.xml");
		xmlNames.Add("furniture.xml");
		xmlNames.Add("errorCode.xml");

		xmlDatas = new Dictionary<string, Dictionary<string, BaseXmlData>> ();
		loadXmls (xmlNames);
		TypeData data = getXmlDataById<TypeData> ("1");
		Debug.Log (data.name);
	}

	private void loadXmls (List<string> xmlNames)
	{
		XmlDocument doc;
		XmlNode node;
		Type t;
		Dictionary<string,BaseXmlData> datas;
		BaseXmlData xml;
		for (int i = 0, l = xmlNames.Count; i < l; i++) {
			doc = ReadAndLoadXml (xmlNames [i]);
			node = doc.SelectSingleNode ("RECORDS");
			string className = node.Attributes["class"].Value;
			t = Type.GetType (className);

			datas = new  Dictionary<string, BaseXmlData> ();
			foreach (XmlElement p in node.ChildNodes) {
				if (p.HasAttribute ("id")) {
					xml = (BaseXmlData)Activator.CreateInstance (t);
					xml.init (p);
					datas.Add (p.GetAttribute ("id"),xml);
				}
			}
			xmlDatas.Add (className, datas);
		}
	}

	/// <summary>  
	/// 加载xml文档  
	/// </summary>  
	/// <returns></returns>  
	public XmlDocument ReadAndLoadXml(string url)  
	{  
		url = Application.streamingAssetsPath + "/XML/" + url;
		XmlDocument doc = new XmlDocument();  
		doc.Load(url);
		return doc;
	}

	public T getXmlDataById<T>(string id) where T:BaseXmlData
	{
		Type t = typeof(T);
		Dictionary<string, BaseXmlData> datas = xmlDatas[t.Name];
		return (T)datas [id];
	}
}

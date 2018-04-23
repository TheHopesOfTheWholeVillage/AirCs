using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo {
	
	//设备sn号
	public static string Serialno =  string.Empty;
	//影创账号
	public static string Account = string.Empty;
	//影创应用号
	public static string AppID = string.Empty;

	/// <summary>
	/// 判断是否为游客账号
	/// </summary>
	/// <returns><c>true</c>, if vistor account was ised, <c>false</c> otherwise.</returns>
	public static bool isVistorAccount()
	{
		return Serialno == Account;
	}
	/// <summary>
	/// 判断用户是否登陆
	/// </summary>
	/// <returns><c>true</c>, if login was ised, <c>false</c> otherwise.</returns>
	public static bool isLogin()
	{
		return Account != string.Empty;
	}
}

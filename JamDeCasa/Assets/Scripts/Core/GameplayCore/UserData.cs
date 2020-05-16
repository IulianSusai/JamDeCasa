using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
	private static UserData instance;
	public static UserData Instance {
		get {
			if(instance == null) {
				instance = new UserData();
			}
			return instance;
		}
	}

	public SavedData savedData { private set; get; }

	private UserData() {
		savedData = new SavedData();
		savedData.Load();
	}
}

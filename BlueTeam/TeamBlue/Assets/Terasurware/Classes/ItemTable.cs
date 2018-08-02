using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemTable : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int Code;
		public string Name;
		public string Type;
		public int HP;
		public int Attack;
		public int Defence;
		public int RecipeWood;
		public int RecipeIron;
		public int RecipeSheep;
		public int RecipeBrick;
		public string Image;
	}
}


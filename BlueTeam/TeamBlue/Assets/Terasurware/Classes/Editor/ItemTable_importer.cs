using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ItemTable_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Terasurware/ItemTable.xlsx";
	private static readonly string exportPath = "Assets/Terasurware/ItemTable.asset";
	private static readonly string[] sheetNames = { "ItemTable", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			ItemTable data = (ItemTable)AssetDatabase.LoadAssetAtPath (exportPath, typeof(ItemTable));
			if (data == null) {
				data = ScriptableObject.CreateInstance<ItemTable> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					ItemTable.Sheet s = new ItemTable.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						ItemTable.Param p = new ItemTable.Param ();
						
					cell = row.GetCell(0); p.Code = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.Type = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.HP = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.Attack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.Defence = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.RecipeWood = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.RecipeIron = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.RecipeSheep = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.RecipeBrick = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.Image = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}

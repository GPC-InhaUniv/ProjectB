using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PresentorEditor
{

}

[CustomEditor(typeof(EquipUIPresenter))]
public class EquipPresentorEditor : Editor
{
    public override void OnInspectorGUI()
    {

    }
}

[CustomEditor(typeof(CombinationUIPresenter))]
public class CombinationPresenterEditor : Editor
{
    public override void OnInspectorGUI()
    {

    }
}

[CustomEditor(typeof(QuestUIPresenter))]
public class QuestPresenter : Editor
{
    public override void OnInspectorGUI()
    {

    }
}

[CustomEditor(typeof(InventoryUIPresenter))]
public class InventoryPresenter : Editor
{
    public override void OnInspectorGUI()
    {

    }
}

[CustomEditor(typeof(WarehouseUIPresenter))]
public class WarehousePresenter : Editor
{
    public override void OnInspectorGUI()
    {

    }
}

[CustomEditor(typeof(WorldMapUIPresenter))]
public class WorldMapUIyPresenter : Editor
{
    public override void OnInspectorGUI()
    {

    }
}
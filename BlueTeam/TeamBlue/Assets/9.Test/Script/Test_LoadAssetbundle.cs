
using UnityEngine;

public class Test_LoadAssetbundle : MonoBehaviour {

	public void LoadWood()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.WoodDungeon);
    }
    public void LoadIron()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.IronDungeon);
    }
    public void LoadSheep()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.SheepDungeon);
    }
    public void LoadBrick()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.BrickDungeon);
    }
    public void LoadTown()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.Town);
       
    }
    public void ChangeScene()
    {
        Test_AssetBundleManager.Instance.LoadScene();
    }
}

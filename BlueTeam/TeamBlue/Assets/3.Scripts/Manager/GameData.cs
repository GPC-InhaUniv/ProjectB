/// <summary>
/// 담당자 : 김정수
/// 
/// 내용 : 게임 자원 관리
/// 
/// Input : 자원 타입, 자원 개수
/// </summary>


public enum GameResources
{
    Brick,
    Wood,
    Iron,
    Sheep,
}

public class GameData : Singleton<GameData>
{

    private int brick;
    private int wood;
    private int iron;
    private int sheep;


    public void ChangeResource(GameResources type, int count)
    {
        switch (type)
        {
            case GameResources.Brick:
                brick += count;
                break;
            case GameResources.Wood:
                wood += count;
                break;
            case GameResources.Iron:
                iron += count;
                break;
            case GameResources.Sheep:
                sheep += count;
                break;
            default:
                break;
        }
    }

}

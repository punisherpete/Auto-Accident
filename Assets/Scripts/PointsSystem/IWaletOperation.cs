public interface IWaletOperation 
{
    public void OperateWithPoints(int amount);
    public int GetPointsAmount();
    public void Reset(int value);
}

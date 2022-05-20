public interface IPointsPoolTransaction
{
    public void OnWithdraw(int amout);
    public void UnlinkWithdrawEvents();
}

public class GoldMine : Unit
{
    protected override void Die(Team team)
    {
        Health.ResetCount();
        SetTeam(team);
    }
}

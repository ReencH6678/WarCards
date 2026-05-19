public class GoldFinder : EnemyFinder
{
    protected override bool IsPriority(Unit unit)
    {
        if(unit.TryGetComponent<GoldMine>(out _))
            return true;

        return false;
    }
}


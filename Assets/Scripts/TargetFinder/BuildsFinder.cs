public class BuildsFinder : EnemyFinder
{
    protected override bool IsPriority(Unit unit)
    {
        if (unit.TryGetComponent<Build>(out _))
                return true;

        return false;
    }
}

using Sources.Domain.Enemies;

namespace Sources.InfrastructureInterfaces.Assessors
{
    public interface IEnemyAssessor
    {
        int Assess(IEnemy enemy); 
    }
}
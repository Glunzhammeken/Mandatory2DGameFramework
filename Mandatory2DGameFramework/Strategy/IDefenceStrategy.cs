using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    // Strategy for forsvar (Basis for Decorator og Composite)
    public interface IDefenceStrategy
    {
        // Reducerer den indkommende skade baseret på forsvarsstrategien
        int ReduceDamage(int incomingDamage);

        // Returnerer en basisværdi for forsvaret (kan bruges af Decorator)
        int GetDefenceValue();
    }
}

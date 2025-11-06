using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    // Decorator Base: Abstrakt klasse, der holder en reference til Strategy-objektet
    public abstract class DefenceDecorator : IDefenceStrategy
    {
        protected IDefenceStrategy _wrappee;

        public DefenceDecorator(IDefenceStrategy defence)
        {
            _wrappee = defence;
        }

        // Delegerer alle kald til det indpakkede objekt
        public virtual int ReduceDamage(int incomingDamage)
        {
            return _wrappee.ReduceDamage(incomingDamage);
        }

        public virtual int GetDefenceValue()
        {
            return _wrappee.GetDefenceValue();
        }
    }
}
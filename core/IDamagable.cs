using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoEngine.core
{
    public interface IDamagable
    {
         float MaxHealth {  get;  set; }
        float CurHealth { get; set; }
        GameObject BaseClass { get; set; }

        void TakeDamage(float dmg) ;
        void Die();
       
    }
}

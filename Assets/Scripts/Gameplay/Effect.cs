using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//一些可以作用在角色身上的效果，立即激活
public interface IEffect
{
    void Activate(Entity entity);
}
public class AddBuffEffect : IEffect
{
    public Buff[] buffs;

    public void Activate(Entity entity)
    {
        foreach(Buff buff in buffs)
        {
            buff.Apply(entity);
        }
    }
}
public class HealEffect : IEffect
{
    public float healAmount;

    public void Activate(Entity entity)
    {
        entity.Heal(healAmount);
    }
}
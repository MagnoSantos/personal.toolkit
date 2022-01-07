using System.Collections.Generic;
using System.Linq;

namespace Personal.Patterns.Strategy
{
    public class StrategyDefiniedProcessor
    {
        public string Process(Type typeToProcess)
        {
            return typeToProcess switch
            {
                Type.StrategyA => new StrategyA().ExecuteAsync(),
                Type.StrategyB => new StrategyB().ExecuteAsync(),
                _ => throw new System.NotImplementedException(),
            };
        }
    }

    public enum Type
    {
        StrategyA, 
        StrategyB
    }
}
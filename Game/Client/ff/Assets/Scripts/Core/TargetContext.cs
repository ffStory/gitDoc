using System.Collections.Generic;

namespace Core
{
    public class TargetContext
    {
        public Dictionary<ObjectType, object> Context;

        public TargetContext()
        {
            Context = new Dictionary<ObjectType, object>();
        }
    }
}

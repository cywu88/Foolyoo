using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class EventManager
    {
        private Dictionary<string, Dictionary<int, Action<object>>> m_handlers = new Dictionary<string, Dictionary<int, Action<object>>>();

        private Dictionary<int, Action<object>> m_handlerAlls = new Dictionary<int, Action<object>>();

        public void Send(string name, string sessionID, object data)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using Furesoft.Rpc.Mmf.InformationApi;
using System.Linq;

namespace Furesoft.Rpc.Mmf
{
    //ToDo: check class modifiers in whole lib
    internal class InterfaceInfoImpl : IInterfaceInfo
    {
        private RpcServer server;

        public InterfaceInfoImpl(RpcServer server)
        {
            this.server = server;
        }

        public InterfaceInfo GetInfo(string name)
        {
            var t = server.GetInterfaceType(name);
            
            return InterfaceInfoBuilder.StartBuild(t);
        }

        public string[] GetInterfaceNames()
        {
            return server._iTypes.Keys.ToArray();
        }
    }
}
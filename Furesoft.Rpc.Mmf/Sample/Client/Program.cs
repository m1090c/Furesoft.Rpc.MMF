﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Furesoft.Rpc.Mmf;
using Furesoft.Rpc.Mmf.Auth;
using Furesoft.Rpc.Mmf.Serializer;
using Interface;

namespace Client
{
    internal class Bootstrapper : RpcBootstrapper
    {
        public override void Boot()
        {
            AuthModule.Enable(this);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new RpcClient("ExampleChannel", new Bootstrapper(), null);
            client.Start();

            var interfaces = client.GetInterfaceNames();
            var info = client.GetInfo<IMath>();

            Thread.Sleep(2000);

            var math = client.Bind<IMath>();

            var p = math.AddPosition(10, 15);
            p = math.TranslatePoint(p);

            p.GetX();

            //math.MethodWithException();
            math.OnIndexChanged += Math_IndexChanged;

            Console.WriteLine("result: " + math.Add(15, 5));

            math[10] = 5;

            Console.WriteLine("Index result: " + math[10]);

            try
            {
                //math.MethodWithException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has thrown: " + ex.Message);
            }

            Console.ReadLine();
        }

        private static void Math_IndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}
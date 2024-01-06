﻿using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Nelibur.ObjectMapper.Reflection
{
    internal class DynamicAssemblyBuilder
    {
        internal const string PublicKey = "4bde840421ed3b53";
        internal static readonly byte[] PublicKeyAsBytes = Encoding.ASCII.GetBytes(PublicKey);
        internal const string AssemblyName = "DynamicTinyMapper";
        internal const string AssemblyNameWithPublicKey = "DynamicTinyMapper, PublicKey=4bde840421ed3b53";
#if !COREFX
        //        private const string AssemblyNameFileName = AssemblyName + ".dll";
        //        private static AssemblyBuilder _assemblyBuilder;
#endif
        private static readonly DynamicAssembly _dynamicAssembly = new DynamicAssembly();

        public static IDynamicAssembly Get()
        {
            return _dynamicAssembly;
        }


        private sealed class DynamicAssembly : IDynamicAssembly
        {
            private readonly ModuleBuilder _moduleBuilder;

            public DynamicAssembly()
            {
                var assemblyName = new AssemblyName(AssemblyName);
                assemblyName.SetPublicKey(PublicKeyAsBytes);

#if COREFX
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

#else
                AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                //                        _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

                _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
                //                        _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name, AssemblyNameFileName, true);
#endif

            }

            public TypeBuilder DefineType(string typeName, Type parentType)
            {
                return _moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Sealed, parentType, null);
            }

            public void Save()
            {
#if !COREFX
                //                _assemblyBuilder.Save(AssemblyNameFileName);
#endif
            }
        }
    }
}

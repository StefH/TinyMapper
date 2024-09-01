using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Nelibur.ObjectMapper.Reflection
{
    internal class DynamicAssemblyBuilder
    {
        private const string PublicKey = "0024000004800000940000000602000000240000525341310004000001000100d91d5600a91c47559a4101fdb29145e6f762d2c3061beecf50d1f15927546a17439f188e0239b17e1fd9eb31c9fbd900f530be120f501bcdc5eb6eae1e091e230d7dbfe19df7157cdef9c1ac16fcdf58ac0ecb824f62c702c27f92fbdc49789d24e2c63c0b9c727a6e0ed7a719a88c2286b1c2623322a4c7ab8ce51ddb4faf97";
        private static readonly byte[] PublicKeyAsBytes = Encoding.ASCII.GetBytes(PublicKey);
        private const string AssemblyName = "DynamicTinyMapper";
        internal const string AssemblyNameWithPublicKey = "DynamicTinyMapper, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d91d5600a91c47559a4101fdb29145e6f762d2c3061beecf50d1f15927546a17439f188e0239b17e1fd9eb31c9fbd900f530be120f501bcdc5eb6eae1e091e230d7dbfe19df7157cdef9c1ac16fcdf58ac0ecb824f62c702c27f92fbdc49789d24e2c63c0b9c727a6e0ed7a719a88c2286b1c2623322a4c7ab8ce51ddb4faf97";
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
                var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
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

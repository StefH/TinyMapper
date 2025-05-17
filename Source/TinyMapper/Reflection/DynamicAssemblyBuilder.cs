using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Nelibur.ObjectMapper.Reflection
{
    internal class DynamicAssemblyBuilder
    {
        private const string AssemblyName = "DynamicTinyMapper";
        internal const string PublicKey = "0024000004800000940000000602000000240000525341310004000001000100d15fcca86c1c96fc30935c05d618660538bd0b69754742c89ac823d097b5b6143fff3718342d3f1df7eb3c68946612a11f163e80a464be115a6884e79c35445c43254dc30441633ddf3ca4c44efe736171ef2d7f36d127d1d82112b02ee717d3b0f881945ecafa949e669c2a4b2bed9692a340c48b623f6de77706284607acd9";
        internal const string AssemblyNameWithPublicKey = "DynamicTinyMapper, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15fcca86c1c96fc30935c05d618660538bd0b69754742c89ac823d097b5b6143fff3718342d3f1df7eb3c68946612a11f163e80a464be115a6884e79c35445c43254dc30441633ddf3ca4c44efe736171ef2d7f36d127d1d82112b02ee717d3b0f881945ecafa949e669c2a4b2bed9692a340c48b623f6de77706284607acd9";

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
                assemblyName.SetPublicKey(StringToByteArray(PublicKey));

#if COREFX
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

#else
                AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                _moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
#endif

            }

            public TypeBuilder DefineType(string typeName, Type parentType)
            {
                return _moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Sealed, parentType, null);
            }

            public void Save()
            {
            }
        }

        private static byte[] StringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}